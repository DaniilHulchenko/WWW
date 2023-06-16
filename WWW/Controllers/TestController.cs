using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WWW.DAL.Interfaces;

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

        //EventApiJob_ParseToDb EventApiJob_ParseToDb;

        //EntityBaseRepository<Article> baseRepository
        //public HomeController(EntityBaseRepository<Article> baseRepository, ArticleApiJob_ParseToDb articleApiJob_ParseToDb,DownloadService downloadService,RestApiRequest restApiRequest, IArticleService articleService, ICategoryService categoryService, IBaseRepository<Tags>  tagsRepository)

        //private readonly EntityBaseRepository<Article> _baseArticleRepository;
        //private readonly RestApiRequest _restApiRequest;
        //private readonly IMapper _mapper;
        //public TestController(RestApiRequest restApiRequest, EntityBaseRepository<Article> baseRepository, IMapper mapper)
        //{
        //    _mapper = mapper;
        //    _restApiRequest = restApiRequest;
        //    _baseArticleRepository = baseRepository;
        //    //EventApiJob_ParseToDb = eventApiJob_ParseToDb;

        //}

        //private readonly IElasticClient _elasticClient;

        IArticleRepository _articleRepository;
        public TestController(IArticleRepository articleRepository)
        {
            //_elasticClient = elasticClient;
            _articleRepository = articleRepository;
        }



        public async Task<IActionResult> Index(string searchTerm)
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
            //var searchResponse = _elasticClient.Search<Article>(s => s
            //      .Query(q => q
            //          .Match(m => m
            //              .Field(f => f.Title)
            //              .Query("WAILERS WITH")
            //          )
            //      )
            //  );

            var a = _articleRepository.SearchByTitle("NK: TRUSTF");


            return View("Index", a);
        }
        [Authorize]
        public async Task<IActionResult> Chat()
        {
            return View();
        }
        public async Task<IActionResult> DataGetter()
        {
            return View();
        }
    }


}
