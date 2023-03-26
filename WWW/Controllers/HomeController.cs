using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Models;
using WWW.Service.Interfaces;

namespace WWW.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        //private readonly ILogger<HomeController> _logger;

        //public HomeController( )
        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy(int i)
        {
            //var data = await _articleService.foo(i);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}