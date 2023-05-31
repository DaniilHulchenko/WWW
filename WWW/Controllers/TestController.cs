using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Policy;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Domain.GoogleOAuth;
using WWW.Jobs.Jobs;
using WWW.Service.Helpers;
using WWW.Service.Implementations;

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

        EventApiJob_ParseToDb EventApiJob_ParseToDb;

        //EntityBaseRepository<Article> baseRepository
        //public HomeController(EntityBaseRepository<Article> baseRepository, ArticleApiJob_ParseToDb articleApiJob_ParseToDb,DownloadService downloadService,RestApiRequest restApiRequest, IArticleService articleService, ICategoryService categoryService, IBaseRepository<Tags>  tagsRepository)

        private readonly EntityBaseRepository<Article> _baseArticleRepository;
        private readonly RestApiRequest _restApiRequest;
        private readonly IMapper _mapper;
        public TestController(EventApiJob_ParseToDb eventApiJob_ParseToDb ,RestApiRequest restApiRequest, EntityBaseRepository<Article> baseRepository, IMapper mapper)
        {
            _mapper = mapper;
            _restApiRequest = restApiRequest;
            _baseArticleRepository = baseRepository;
            EventApiJob_ParseToDb = eventApiJob_ParseToDb;
            
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

            ////_downloadService.DownloadJpgPictAsync("https://static.nachasi.com/wp-content/uploads/2022/06/watermelon-2-1.gif-1.gif");



            //_restApiRequest.ApiSelector("Events:ticketmaster");
            //Rootobject a = await _restApiRequest.GetDataAsync<Rootobject>();
            //Article article = _mapper.Map<Article>(a._embedded.events[0]);

            //_restApiRequest.ApiSelector("Events:ticketmaster");
            //var apidata = await _restApiRequest.GetDataAsync<Rootobject>();

            //Article data = _mapper.Map<Article>(apidata._embedded.events[0]);
            //var articles = _baseArticleRepository.GetALL().First().Title;

            //string token = "ya29.a0AWY7Ckngotaz_2PFpf25haTMQSNJp8zqC2inYC12M6gpYg-i9rh0H6G9jw9_gAydXKY8FrlQJRWRmMfKY3pRIhMOVWS8JuDFlhHrXGrjQrnvou1zAD7OfHLnlrneqGJJNO_XLoX1wTELyH5v9wDmY8a-UQc9aCgYKAdcSARISFQG1tDrpwmFSdcgAXjpNTH-4ARxSsA0163";
            //var data = await _googleApiService.GetUserInfoAsync(token);
            //string fileway = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "base-avatar.jpg");
            //byte[] pict = System.IO.File.ReadAllBytes(fileway);
            //await EventApiJob_ParseToDb.ExecuteAsync();
            return View("Index");
        }
        [Authorize]
        public async Task<IActionResult> Chat()
        {
            return View();
        }
    }


}
