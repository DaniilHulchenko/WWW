using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WWW.Domain.Entity;
using WWW.Domain.Enum;
using WWW.Domain.Enum.Articles;
using WWW.Domain.ViewModels.Article;
using WWW.Models;
using WWW.Service.Interfaces;
using WWW.Domain.Enum.Articles;
using WWW.DAL.Repositories;
using Hangfire.MemoryStorage.Database;
using System.Drawing.Printing;
using WWW.Domain.Api;
using Newtonsoft.Json.Linq;
using WWW.Domain.Response;

namespace WWW.Controllers
{
    public class ArticleController : Controller
    {
        IArticleService _articleService;
        ICategoryService _categoryService;
        IUserService _userService;
        EntityBaseRepository<User> _userRepository;   
        public ArticleController(IArticleService articleService, ICategoryService categoryService, Service.Interfaces.IUserService accountService, EntityBaseRepository<User> userRepository)
        {
            _userRepository=userRepository; 
            _articleService = articleService;
            _categoryService = categoryService;
            _userService = accountService;


        }

        // GET: Article
        public async Task<IActionResult> Index(string? searchTerm = null,string ? category = null, int page = 0, ArticleSortOption SortOption=ArticleSortOption.None, string Filters=null)
        {
            int pageSize = 6;

            var data = (await _articleService.GetByCity(HttpContext.Session.GetString("City")));
            if(searchTerm !=null )
                 data.Data = (await _articleService.SearchByTitle(data.Data, searchTerm));

            //var data = await _articleService.GetByCategoryName(category);


            data = await _articleService.GetByCategoryNameFilter(data.Data, category);
            data = await _articleService.OrderBy(data.Data,SortOption);

            if (Filters != null)
            {
                Dictionary<string, string> filtersDict = Filters
                                    .Split(',')
                                    .Select(kv => kv.Split('-'))
                                    .ToDictionary(kv => kv[0], kv => kv[1]);
                data = await _articleService.Filter(data.Data, filtersDict);
            }
            


            //_logger.LogInformation(data.StatusCode.ToString());
            PageIndexViewModel<Article> paginator = new PageIndexViewModel<Article>(data.Data, pageSize, page);
            if (data.StatusCode != Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Error");
            }
            return View(paginator);
        }

        [Authorize]
        public async Task<IActionResult> FavoriteEvents() {
            var UserId = int.Parse(User.FindFirst("UserId").Value);
            var user = await _userRepository.GetValueByID(UserId);

            PageIndexViewModel<Article> paginator = new PageIndexViewModel<Article>(user.FavEvent.OrderByDescending(e=>e.Date.Date_Of_Start), 6, 0);


            return View(paginator);
        }

        // GET: Article/Details/5
        public ActionResult Details(string slug)
        {
            var data = _articleService.GetBySlug(slug);
            if (data.StatusCode== WWW.Domain.Enum.StatusCode.OK)
                return View(data.Data);
            else
            {
                //_logger.LogError(data.ErrorDescription);
                return BadRequest();
            }
        }

        // GET: Article/Create
        //[Authorize(Roles = "Admin")
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        // POST: Article/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Check(ArticleCreateViewModal collection)
        {
            if (ModelState.IsValid)
            {
                await _articleService.Create(collection);
                return Redirect("/Article");
            }
           return View("Create", collection);
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

        [Authorize]
        public async Task<IActionResult> AddOrDeleteFavoriteEvent(int ArticleId)
        {
            var UserId = int.Parse(User.FindFirst("UserId").Value);
            await _userService.AddOrDeleteFavoriteEvent(UserId, ArticleId);
            return Ok();
        }

        public async Task<IActionResult> Autor (int id)
        {
            var data = await _userRepository.GetValueByID(id);
            return View(data ) ;
        }
    }
}
