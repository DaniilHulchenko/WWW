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
        private readonly IApiRequrst _apiRequest;

        //private readonly ILogger<HomeController> _logger;

        //public HomeController( )
        public HomeController(IApiRequrst apiRequest, IArticleService articleService, ICategoryService categoryService, IBaseRepository<Tags>  tagsRepository)
        {
            _apiRequest = apiRequest;
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
            _apiRequest.SetApiName("Events");
            dynamic data = await _apiRequest.GetData(new Dictionary<string, string>{
                { "country", "CA" },
            });

            return View(data.results[1].title);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}