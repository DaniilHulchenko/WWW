using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.AccessControl;
using WWW.API;
using WWW.DAL.Repositories;
using WWW.Domain.Api;
using WWW.Domain.Entity;
using WWW.Domain.ViewModels.Article;

namespace WWW.Controllers
{
    public class TestController : Controller
    {
        //private readonly IArticleService _articleService;
        //private readonly ICategoryService _categoryService;
        //private readonly IBaseRepository<Tags> _tagsRepository;
        //private readonly RestApiRequest _restApiRequest;
        //private readonly ILogger<HomeController> _logger;

        //DownloadService _downloadService;

        //ArticleApiJob_ParseToDb _articleApiJob_ParseToDb;

        //EntityBaseRepository<Article> baseRepository
        //public HomeController(EntityBaseRepository<Article> baseRepository, ArticleApiJob_ParseToDb articleApiJob_ParseToDb,DownloadService downloadService,RestApiRequest restApiRequest, IArticleService articleService, ICategoryService categoryService, IBaseRepository<Tags>  tagsRepository)

        private readonly EntityBaseRepository<Article> _baseArticleRepository;
        private readonly RestApiRequest _restApiRequest;
        private readonly IMapper _mapper;
        public TestController(RestApiRequest restApiRequest, EntityBaseRepository<Article> baseRepository, IMapper mapper)
        {
            _mapper = mapper;
            _restApiRequest = restApiRequest;
            _baseArticleRepository = baseRepository;
        }

       
        public async Task<IActionResult> Index()
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
            //Article article = _mapper.Map<Article>(a._embedded.events[0]);

            //_restApiRequest.ApiSelector("Events:ticketmaster");
            //var apidata=await _restApiRequest.GetDataAsync<Rootobject>();

            //Article data = _mapper.Map<Article>(apidata._embedded.events[0]);
            //var articles = _baseArticleRepository.GetALL().First().Title;
            return View();
        }
    }
}
