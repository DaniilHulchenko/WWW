using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Models;
using WWW.Service.Interfaces;
using WWW.API;
using Microsoft.Extensions.Primitives;
using GoogleApi.Entities.Interfaces;
using WWW.Jobs.Workers;
using WWW.Domain.Api;
using WWW.DAL.Repositories;

namespace WWW.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IArticleService _articleService;
        //private readonly ICategoryService _categoryService;
        //private readonly IBaseRepository<Tags> _tagsRepository;
        //private readonly RestApiRequest _restApiRequest;
        private readonly EntityBaseRepository<Article> _baseArticleRepository;
        //private readonly ILogger<HomeController> _logger;

        //DownloadService _downloadService;

        //ArticleApiJob_ParseToDb _articleApiJob_ParseToDb;

        //EntityBaseRepository<Article> baseRepository
        //public HomeController(EntityBaseRepository<Article> baseRepository, ArticleApiJob_ParseToDb articleApiJob_ParseToDb,DownloadService downloadService,RestApiRequest restApiRequest, IArticleService articleService, ICategoryService categoryService, IBaseRepository<Tags>  tagsRepository)
        public HomeController(EntityBaseRepository<Article> baseRepository)
        {
            _baseArticleRepository = baseRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PageForTests()
        {
            //_apiRequest.SetApiName("Articles_predicthq");
            //dynamic data = await _apiRequest.GetData(new Dictionary<string, string>{
            //    { "country", "CA" },
            //});
            //_restApiRequest.ApiSelector("Events:ticketmaster");
            //dynamic data = await _restApiRequest.GetDataAsync(new Dictionary<string, string>{
            //{ "city", "Ottawa" },
            //});
            //await _articleApiJob_ParseToDb.ExecuteAsync();

            ////_downloadService.DownloadJpgAsync("https://static.nachasi.com/wp-content/uploads/2022/06/watermelon-2-1.gif-1.gif");



            //_restApiRequest.ApiSelector("Events:ticketmaster");
            //Rootobject a = await _restApiRequest.GetDataAsync<Rootobject>();
            //string b = a._embedded.events[0].name;
            var articles = _baseArticleRepository.GetALL().First().Title;
            return View();

            //return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}