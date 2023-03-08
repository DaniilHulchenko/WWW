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
        private readonly ICategoryService _categoryService;
        //private readonly ILogger<HomeController> _logger;

        //public HomeController( )
        public HomeController(ICategoryService categoryRepository)
        {
            _categoryService = categoryRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var data = await _categoryService.GetAll();
            return View(data.Data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}