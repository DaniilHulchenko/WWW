using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WWW.Domain.Entity;
using WWW.Service.Interfaces;

namespace WWW.Controllers
{
    public class ArticleController : Controller
    {
        IArticleService _articleService;
        public ArticleController(IArticleService articleService) {
            _articleService = articleService;
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Check(Article collection)
        {
            if (ModelState.IsValid) {
                    await _articleService.Create(collection);
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(collection);
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
