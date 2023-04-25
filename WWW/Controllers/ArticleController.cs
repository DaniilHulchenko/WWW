using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using WWW.Domain.Entity;
using WWW.Domain.ViewModels.Article;
using WWW.Models;
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
        public async Task<IActionResult> Index(string category = "", int page = 0)
        {
            int pageSize = 5;
            var data = await _articleService.GetByCategoryName(category);
            //_logger.LogInformation(data.StatusCode.ToString());
            PageIndexViewModel<Article> paginator = new PageIndexViewModel<Article>(data.Data, pageSize, page);
            if (data.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Error");
            }
            return View(paginator);
        }

        // GET: Article/Details/5
        public ActionResult Details(string stug)
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
                    //Category= collection.Category,

                    Location = new Location() { location= collection.Location },
                    Date = new Date() { Date_of_Creation= collection.DateOfArticle },

                };
                if (collection.Picture != null)
                {
                    using(var memoryStream = new MemoryStream())
                    {
                        await collection.Picture.CopyToAsync(memoryStream);
                        data.Picture= new Picture() { picture = memoryStream.ToArray() };
                    }
                }
                await _articleService.Create(data);
                return Redirect("/");
            }
            else
            {
                //ModelState.AddModelError("");
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
