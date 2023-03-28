using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Models;
using WWW.Service.Interfaces;

namespace WWW.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IBaseRepository<Tags> _tagsRepository;

        //private readonly ILogger<HomeController> _logger;

        //public HomeController( )
        public HomeController(IArticleService articleService, ICategoryService categoryService, IBaseRepository<Tags>  tagsRepository)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _tagsRepository = tagsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            //BaseResponse< IEnumerable<Article>> article = await _articleService.GetAll();
            //Console.WriteLine("!!!"+article.ErrorDescription);
            //var article1= article.Data.First
            //await _articleService.AddTag(article1, _tagsRepository.GetALL().First());
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}