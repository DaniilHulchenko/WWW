using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Domain.ViewModels.Category;
using WWW.Models;
using WWW.Service.Implementations;
using WWW.Service.Interfaces;

namespace WWW.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        //private readonly IArticleRepository _articleRepository;
        private readonly IArticleService _articleService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController( IArticleService articleService, ICategoryRepository categoryRepository, ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            //_articleRepository = articleRepository;
            _articleService = articleService;
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _logger = logger;   
        }

        //[HttpGet("Hello/World")]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 3;
            BaseResponse<IEnumerable<Category>> data = await _categoryService.GetAll();
            PageIndexViewModel<Category> viewModel;
            if (data.Data!=null)
                viewModel = new PageIndexViewModel<Category>(data.Data, pageSize, page);
            else
                viewModel= new PageIndexViewModel<Category>(null, pageSize, page);
            return View(viewModel);
        }

        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Check(CategoryViewModal category) {
            if (ModelState.IsValid)
            {
                var data = new Category { Name = category.Name };
                await _categoryService.Create(data);

                return Redirect("/Category");
            }
            else
            {
                return View("Create", category);
            }
        }
        public async Task<ActionResult> DeleteAsync(int Id) {
            await _categoryService.Delete(Id);
            return Redirect("/Category");

        }
    }
}
