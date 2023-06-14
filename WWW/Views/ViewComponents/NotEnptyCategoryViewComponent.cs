
using Microsoft.AspNetCore.Mvc;

//using WebApplication1.Data.Interfaces;
using WWW.Domain.Entity;
using WWW.DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using WWW.DAL.Interfaces;
using System.Linq;

namespace WWW.ViewComponents
{
    //[ViewComponent(Name = "NECategory")]
    public class NotEnptyCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<NotEnptyCategoryViewComponent> _logger;
        private readonly IArticleRepository articleRepository;
        public NotEnptyCategoryViewComponent(ICategoryRepository categoryRepository, ILogger<NotEnptyCategoryViewComponent> logger, IArticleRepository articleRepository)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            this.articleRepository = articleRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? city=null)
        {
            try
            {
                IQueryable<Category> cat = await _categoryRepository.GetNotEmptyCategory();
                if (false)
                {
                    IQueryable<Article> articles = articleRepository.GetALL().Where(a => a.Location.City == city);


                    cat = cat.Where(c => articles.Any(a => a.Category.Name == c.Name));
                    var a = cat.FirstOrDefault();
                }
                            
                return View("Index", cat.ToList());
            }
            catch (Exception)
            {
                string err="!!! ";
                _logger.LogError(err); 
                throw;
            }
            
        }
        //public async Task<IViewComponentHelper> Allcat()
        //{
        //    return _categoryRepository.GetALL();
        //}
    }
}

//https://learn.microsoft.com/en-us/answers/questions/735223/how-do-you-add-cat-from-the-database-to-the-share