
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
        private readonly ILogger<CategoryViewComponent> _logger;
        public CategoryViewComponent(ICategoryRepository categoryRepository, ILogger<CategoryViewComponent> logger)
        {
            _categoryRepository=categoryRepository; 
            _logger = logger;
        }
        public IViewComponentResult InvokeAsync()
        {
            return View("Index", _categoryRepository.GetNotEmptyCategory());
        }
    }
}

//https://learn.microsoft.com/en-us/answers/questions/735223/how-do-you-add-data-from-the-database-to-the-share