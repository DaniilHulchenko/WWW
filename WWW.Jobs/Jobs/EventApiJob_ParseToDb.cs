using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using WWW.DAL.Interfaces;
using WWW.DAL.Migrations;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Domain.Enum;
using WWW.Domain.Models.Api;
using WWW.Service.Helpers;
using WWW.Service.Helpers.Api;
using WWW.Service.Interfaces;
using static WWW.Domain.Models.Api.TicketApi;
using Location = WWW.Domain.Entity.Location;

namespace WWW.Jobs.Jobs
{
    public class EventApiJob_ParseToDb : IBackgroundJob
    {
        ILogger<EventApiJob_ParseToDb> _logger;
        private readonly IRestApiRequest _restapiRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly DownloadService _downloadService;

        private readonly EntityBaseRepository<Location> _locationRepository;
        private readonly EntityBaseRepository<EventDates> _dateRepository;
        private readonly EntityBaseRepository<Picture> _pictureRepository;
        Dictionary<string, string> keyValuePairs=new Dictionary<string, string>();
          

        private readonly IHttpContextAccessor _httpContextAccessor;
        public EventApiJob_ParseToDb(IRestApiRequest restapiRepository, ILogger<EventApiJob_ParseToDb> logger, IArticleRepository articleRepository, IUserRepository accountRepository, ICategoryRepository categoryRepository, DownloadService downloadService, EntityBaseRepository<Location> locationRepository, EntityBaseRepository<EventDates> dateRepository, EntityBaseRepository<Picture> pictureRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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

        public void SetCity()
        {
            try
            {
                keyValuePairs.Add("city", _httpContextAccessor.HttpContext.Session.GetString("City"));

            }
            catch (Exception)
            {
            }
        }



        public async Task ExecuteAsync()// автомапер 
        {
            //try
            //{
                SetCity();

            _restapiRepository.ApiSelector("Events:ticketmaster");

            keyValuePairs.Add("page", "0");
            Rootobject apiData = (await _restapiRepository.GetDataAsync<Rootobject>(keyValuePairs));
            int totalPages = (int)apiData.page.totalPages;


                for (int p = 0; p < Math.Min(totalPages, 20); p++)
                {

                    keyValuePairs["page"] = $"{p}";
                    for (int i = 0; i < (int)apiData.page.size; i++)
                    {
                    try
                    {

                        var ApiData = apiData._embedded.events[i];

                            string name = (string)ApiData.name;
                            string location = ApiData._embedded.venues[0].name;
                            if (_articleRepository.GetALL().FirstOrDefault(a => a.Title == name && a.Location.location == location) != null) continue;

                            Location Loc = await GetOrCreateLocation(ApiData);

                            Category category = await CreateOrTakeCategory(ApiData.classifications[0].segment.name);

                            Article newArticle = await CreateArticle(ApiData, Loc, category);

                            await CreateDate(ApiData, newArticle);

                            await DownloadAndCreatePicture(ApiData, newArticle);
                    }
                    catch (Exception ex)
                    {
                        //_logger.LogError("!!! " + ex.Message);
                        //continue;
                        //throw new Exception(ex.Message);
                    }

                }
                }
            _logger.LogInformation($"!!! : ArticleApiJob done");
            //}
            //catch (Exception ex)
            //{
            //    //_logger.LogError("!!!!!" + ex.Message);
            //    throw new Exception(ex.Message );
            //}

        }

        private async Task<Article> CreateArticle(dynamic ApiData, Location Loc, Category category) {
            Article newArticle = new()
            {
                Title = ApiData.name,
                ShortDescription = $"{ApiData.name} event show",
                Description = "-",
                Status = (ArticleStatus)Enum.Parse(typeof(ArticleStatus), ApiData.dates.status.code, ignoreCase: true),
                Autor = await _accountRepository.GetALL().FirstAsync(a => a.NickName == "Ticketmaster"),
                Category = category,
                Location = Loc,
                Tags = null,
                Published = true,
                IsFavorite = false,
            };
            newArticle.slug = GenerateArticleSlug(newArticle);
            await _articleRepository.Create(newArticle);
            return newArticle;
        }


        private async Task<Location> GetOrCreateLocation(dynamic ApiData)
        {
            string loc = (string)ApiData._embedded.venues[0].name;

            var Loc = await _locationRepository.GetALL().FirstOrDefaultAsync(l => l.location == loc);
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

            return Loc;
        }

        private async Task<Category> CreateOrTakeCategory(string CatName)
        {
            var Cat = await _categoryRepository.GetALL().FirstOrDefaultAsync(c => c.Name == CatName);
            if (Cat == null) {
                Cat = new Category()
                {
                    Name = CatName,
                    slug = CatName.ToLower(),
                };
                await _categoryRepository.Create(Cat);

            }
            return Cat;
        }


        private async Task DownloadAndCreatePicture(dynamic ApiData, Article newArticle)
        {
            var Pic = await _downloadService.DownloadJpgPictAsync((string)ApiData.images[0].url);
            Pic.Article = newArticle;
            await _pictureRepository.Create(Pic);
        }

        private async Task CreateDate(dynamic ApiData, Article newArticle) {
            var dat = new EventDates()
            {
                Article = newArticle,
                Date_of_Creation = DateTime.Now,
                Date_Of_Start = DateTime.Parse(ApiData.dates.start.localTime),
                Date_Of_Updated = DateTime.Now,
            };
            await _dateRepository.Create(dat);
        }



        private string GenerateArticleSlug(Article article)
        {
            string slug = Regex.Replace(article.Title, @"[^a-zA-Zа-яА-Я\s]", "");

            string name = slug.ToString().ToLower().Replace(' ', '-');
            name = Regex.Replace(name, "-+", "-");

            int newArticleId = _articleRepository.GetALL().OrderByDescending(a => a.Id).Select(a => a.Id).FirstOrDefault() + 1;
            
            return $"{name}-{newArticleId}";
        }
    }
}



