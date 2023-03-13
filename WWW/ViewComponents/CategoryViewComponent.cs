
using Microsoft.AspNetCore.Mvc;

//using WebApplication1.Data.Interfaces;
using WWW.Domain.Entity;
using WWW.DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using WWW.DAL.Interfaces;

namespace WWW.ViewComponents
{
    [ViewComponent(Name = "Category")]
    public class CategoryViewComponent: ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        //private readonly ApplicationDbContext db;
        private readonly ILogger<CategoryViewComponent> _logger;
        public CategoryViewComponent(ApplicationDbContext db, ICategoryRepository categoryRepository, ILogger<CategoryViewComponent> logger)
        {
            _categoryRepository=categoryRepository; 
            //this.db = db;
            _logger = logger;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        //public IViewComponentResult Invoke()
        {
            //string query = "select * from Categories " +
            //                    "where exists (" +
            //                    "select * from Articles where Articles.CategoryID = Categories.Id " +
            //                    "); ";
            //IEnumerable<Category> cat = db.Categories.FromSqlRaw(query);



            //try
            //{
            //    //IEnumerable<Category> data = await Task.Run(()=> _categoryRepository.GetNotEmptyCategory()) ;
            //    IEnumerable<Category> data = _categoryRepository.GetNotEmptyCategory();
            //    return View("Index", data);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError("!!! DB connection error:" + ex.Message);
            //    return View("Error", ex);
            //}
                return View("Index", _categoryRepository.GetNotEmptyCategory());
        }
    }
}

//https://learn.microsoft.com/en-us/answers/questions/735223/how-do-you-add-data-from-the-database-to-the-share