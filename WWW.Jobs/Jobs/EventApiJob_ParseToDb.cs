using GoogleApi.Entities.Search.Video.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.API;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Service.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace WWW.Jobs.Workers
{
    public class ArticleApiJob_ParseToDb : IBackgroundJob
    {
        ILogger<ArticleApiJob_ParseToDb> _logger;
        private readonly RestApiRequest _restapiRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly DownloadService _downloadService;

        private readonly ILocationRepository _locationRepository;
        private readonly IDateRepository _dateRepository;
        private readonly IPictureRepository _pictureRepository;

        public ArticleApiJob_ParseToDb(RestApiRequest restapiRepository, ILogger<ArticleApiJob_ParseToDb> logger, IArticleRepository articleRepository, IAccountRepository accountRepository, ICategoryRepository categoryRepository, DownloadService downloadService, ILocationRepository locationRepository, IDateRepository dateRepository, IPictureRepository pictureRepository)
        {
            _restapiRepository = restapiRepository;
            _logger = logger;
            _articleRepository = articleRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _downloadService = downloadService;

            _locationRepository = locationRepository;
            _dateRepository = dateRepository;
            _pictureRepository = pictureRepository;
        }
        public async Task ExecuteAsync()
        {
            _restapiRepository.ApiSelector("Events:ticketmaster");
            Dictionary<string, string> keyValuePairs = new() {
                    { "city", "London" } };

            keyValuePairs.Add("page", "0");
            dynamic apiData = (await _restapiRepository.GetDataAsync(keyValuePairs));
            int totalPages = (int)apiData.page.totalPages;
            for (int p = 0; p < (totalPages <=10 ? totalPages : 9); p++)
            {
                keyValuePairs["page"] = $"{p}";

                apiData = (await _restapiRepository.GetDataAsync(keyValuePairs));
                for (int i = 0; i < (int)apiData.page.size; i++)
                {
                    dynamic ApiData = apiData._embedded.events[i];
                    string name=(string)ApiData.name;
                    if (_articleRepository.GetALL().FirstOrDefault(a => a.Title == name) != null) continue; 

                    //int articleId = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
                    string loc = (string)ApiData._embedded.venues[0].name;
                    Location Loc =await _locationRepository.GetALL().FirstOrDefaultAsync(l => l.location == loc);
                    if (Loc == null)
                    {
                        Loc = new()
                        {
                            location = ApiData._embedded.venues[0].name,
                            City = ApiData._embedded.venues[0].city.name,
                            Building = ApiData._embedded.venues[0].address.line1,
                            CountryCode = ApiData._embedded.venues[0].country.countryCode,
                            PostalCode = ApiData._embedded.venues[0].postalCode,
                            Timezone = ApiData._embedded.venues[0].timezone
                        };
                        await _locationRepository.Create(Loc);
                    }



                    Article newArticle = new()
                    {
                        Title = ApiData.name,
                        ShortDescription = $"{ApiData.name} event show",
                        Description = "-",
                        Status = ApiData.dates.status.code,
                        Autor = await _accountRepository.GetALL().FirstAsync(a => a.NickName == "ticketmaster"),
                        Category = _categoryRepository.GetALL().First(c => c.Name == "Ticketmaster Events"),
                        Location = Loc,
                        Tags = null,
                        Published = true,
                        IsFavorite = false,
                    };
                    newArticle.slug = ApiData.name.ToString().ToLower().Replace(' ', '-') + "-" + (_articleRepository.GetALL().Any() ? _articleRepository.GetALL().OrderBy(o => o.Id).Last().Id + 1 : 0);
                    await _articleRepository.Create(newArticle);

                    var dat = new Date()
                    {
                        Article = newArticle,
                        Date_of_Creation = DateTime.Now,
                        Date_Of_Start = ApiData.dates.start.localTime,
                        Date_Of_End = ApiData.dates.start.dateTime,
                        Date_Of_Updated = DateTime.Now,
                    };
                    await _dateRepository.Create(dat);


                    var Pic = await _downloadService.DownloadJpgAsync((string)ApiData.images[0].url);
                    Pic.Article = newArticle;
                    await _pictureRepository.Create(Pic);
                }
            }
            _logger.LogInformation($"!!! : ArticleApiJob done");



        }
    }
}
