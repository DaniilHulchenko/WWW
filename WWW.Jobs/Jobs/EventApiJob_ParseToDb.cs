using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Service.Helpers;
using WWW.Service.Interfaces;

namespace WWW.Jobs.Jobs
{
    public class EventApiJob_ParseToDb : IBackgroundJob
    {
        ILogger<EventApiJob_ParseToDb> _logger;
        private readonly RestApiRequest _restapiRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly DownloadService _downloadService;

        private readonly EntityBaseRepository<Location> _locationRepository;
        private readonly EntityBaseRepository<Date> _dateRepository;
        private readonly EntityBaseRepository<Picture> _pictureRepository;

        public EventApiJob_ParseToDb(RestApiRequest restapiRepository, ILogger<EventApiJob_ParseToDb> logger, IArticleRepository articleRepository, IAccountRepository accountRepository, ICategoryRepository categoryRepository, DownloadService downloadService, EntityBaseRepository<Location> locationRepository, EntityBaseRepository<Date> dateRepository, EntityBaseRepository<Picture> pictureRepository)
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

        Dictionary<string, string> keyValuePairs = new(){

                    { "city", "Toronto" }
            };


        public async Task ExecuteAsync()// автомапер 
        {
            try
            {             
            _restapiRepository.ApiSelector("Events:ticketmaster");

            keyValuePairs.Add("page", "0");
            dynamic apiData = (await _restapiRepository.GetDataAsync(keyValuePairs));
            int totalPages = (int)apiData.page.totalPages;


                for (int p = 0; p < Math.Min(totalPages, 10); p++)
                {

                    keyValuePairs["page"] = $"{p}";
                    for (int i = 0; i < (int)apiData.page.size; i++)
                    {
                        try
                        {

                            dynamic ApiData = apiData._embedded.events[i];

                            string name = (string)ApiData.name;
                            string location = ApiData._embedded.venues[0].name;
                            if (_articleRepository.GetALL().FirstOrDefault(a => a.Title == name & a.Location.location == location) != null) continue;

                            Location Loc = await GetOrCreateLocation(ApiData);

                            Article newArticle = await CreateArticle(ApiData, Loc);

                            await CreateDate(ApiData, newArticle);

                            await DownloadAndCreatePicture(ApiData, newArticle);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("!!! " + ex.Message);
                            continue;
                        }

                    }
                }
            _logger.LogInformation($"!!! : ArticleApiJob done");
            }
            catch (Exception ex)
            {
                _logger.LogError("!!!!!" + ex.Message);
                throw new Exception(ex.Message );
            }

        }

        private async Task<Article> CreateArticle(dynamic ApiData, Location Loc) {
            Article newArticle = new()
            {
                Title = ApiData.name,
                ShortDescription = $"{ApiData.name} event show",
                Description = "-",
                Status = ApiData.dates.status.code,
                Autor = await _accountRepository.GetALL().FirstAsync(a => a.NickName == "Ticketmaster"),
                Category = _categoryRepository.GetALL().First(c => c.Name == "Ticketmaster Events"),
                Location = Loc,
                Tags = null,
                Published = true,
                IsFavorite = false,
            };
            //newArticle.slug = ApiData.name.ToString().ToLower().Replace(' ', '-') + "-" + (_articleRepository.GetALL().Any() ? _articleRepository.GetALL().OrderBy(o => o.Id).Last().Id + 1 : 0);
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

        private async Task DownloadAndCreatePicture(dynamic ApiData, Article newArticle)
        {
            var Pic = await _downloadService.DownloadJpgPictAsync((string)ApiData.images[0].url);
            Pic.Article = newArticle;
            await _pictureRepository.Create(Pic);
        }

        private async Task CreateDate(dynamic ApiData, Article newArticle) {
            var dat = new Date()
            {
                Article = newArticle,
                Date_of_Creation = DateTime.Now,
                Date_Of_Start = ApiData.dates.start.localTime,
                Date_Of_Updated = DateTime.Now,
            };
            await _dateRepository.Create(dat);
        }



        private string GenerateArticleSlug(Article article)
        {
            string name =  article.Title.ToString().ToLower().Replace(' ', '-');
            int newArticleId = _articleRepository.GetALL().OrderByDescending(a => a.Id).Select(a => a.Id).FirstOrDefault() + 1;

            return $"{name}-{newArticleId}";
        }
    }
}



