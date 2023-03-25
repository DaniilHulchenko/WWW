using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Models;
using WWW.Service.Implementations;
using WWW.Service.Interfaces;

namespace WWW.Controllers
{
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
            //foreach (Category category in data)
            //{
            //    _logger.LogInformation($"!!! : {category}");
            //}
            //IEnumerable<Category> items = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //PageViewModel PageViewModel = new PageViewModel(data.Count(), page, pageSize);
            //PageIndexViewModel<Category> viewModel = new PageIndexViewModel<Category>
            //{
            //    PageViewModel = PageViewModel,
            //    Data = items
            //};

            int pageSize = 3;
            BaseResponse<IEnumerable<Category>> data = await _categoryService.GetAll();
            PageIndexViewModel<Category> viewModel = new PageIndexViewModel<Category>(data.Data, pageSize, page);
            return View(viewModel);
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


        public async Task<IActionResult> List(string category = "", int page=0)
        {
            int pageSize = 5;
            var data = await _articleService.GetByCategoryName(category);
            _logger.LogInformation(data.StatusCode.ToString());
            PageIndexViewModel<Article> paginator = new PageIndexViewModel<Article>(data.Data, pageSize, page);
            if (data.StatusCode != Domain.Enum.StatusCode.OK){
                return RedirectToAction("Error");
            }
            return View(paginator);
        }

        public ActionResult Delete(int Id) {
            _categoryService.DeleteById(Id);
            
            return Redirect("/Category");

        }
    }
}
