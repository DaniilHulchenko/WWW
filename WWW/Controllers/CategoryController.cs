using Microsoft.AspNetCore.Mvc;

using WWW.DAL;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Service.Implementations;
using WWW.Service.Interfaces;
using static Grpc.Core.Metadata;

namespace WWW.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;

        public CategoryController( IArticleRepository articleRepository, ICategoryRepository categoryRepository, ICategoryService categoryService)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
        }
        public async Task<IActionResult > Index()
        {
            return View(await _categoryRepository.GetAll());
        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Check(Category category) {
            if (ModelState.IsValid)
            {
                await _categoryService.Create(category);
                //await _categoryRepository.Create(category);
                return Redirect("/Category");
            }
            else
            {
                return View("Create", category);
            }
        }


        public async Task<IActionResult> List(string category = "")
        {
            return View(await _articleRepository.GetByCategoryName(category));
        }

        public ActionResult Delete(int Id) {
            _categoryService.DeleteById(Id);
            return Redirect("/Category");

        }
    }
}
