using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Models;
using WWW.Service.Interfaces;
using WWW.API;
using Microsoft.Extensions.Primitives;
using GoogleApi.Entities.Interfaces;

namespace WWW.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IBaseRepository<Tags> _tagsRepository;
        private readonly IApiRepository<RestApiRequest> _restApiRequest;
        //private readonly ILogger<HomeController> _logger;


        public HomeController(IApiRepository<RestApiRequest> restApiRequest, IArticleService articleService, ICategoryService categoryService, IBaseRepository<Tags>  tagsRepository)
        {
            _restApiRequest = restApiRequest;
            _articleService = articleService;
            _categoryService = categoryService;
            _tagsRepository = tagsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PageForTests()
        {
            //_apiRequest.SetApiName("Events_predicthq");
            //dynamic data = await _apiRequest.GetData(new Dictionary<string, string>{
            //    { "country", "CA" },
            //});
            _restApiRequest.ApiSelector("Events:ticketmaster");
            dynamic data = await _restApiRequest.GetDataAsync(new Dictionary<string, string>{
                { "city", "Ottawa" },
            });

            return View(data._embedded.events[0].name);

            //return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}