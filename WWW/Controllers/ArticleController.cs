using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WWW.Domain.Entity;
using WWW.Domain.ViewModels.Article;
using WWW.Service.Interfaces;

namespace WWW.Controllers
{
    public class ArticleController : Controller
    {
        IArticleService _articleService;
        ICategoryService _categoryService;
        public ArticleController(IArticleService articleService, ICategoryService categoryService) {
            _articleService = articleService;
            _categoryService = categoryService;
        }
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        // GET: Article/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Check(ArticleViewModal collection)
        {
            if (ModelState.IsValid)
            {
                Article data = new Article()
                {
                    Title = collection.Title,
                    ShortDescription = collection.ShortDescription,
                    Description = collection.Description,
                    Published = collection.Published,
                    CategoryID=collection.Category
                };
                await _articleService.Create(data);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Create", collection);
            }
        }

        // GET: Article/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Article/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Article/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Article/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
