
using Microsoft.AspNetCore.Mvc;

//using WebApplication1.Data.Interfaces;
using WWW.Domain.Entity;
using WWW.DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using WWW.DAL.Interfaces;

namespace WWW.ViewComponents
{
    //[ViewComponent(Name = "NECategory")]
    public class SelectCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<SelectCategoryViewComponent> _logger;
        public SelectCategoryViewComponent(ICategoryRepository categoryRepository, ILogger<SelectCategoryViewComponent> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _categoryRepository.GetAll();
            
            return View(data);
        }
    }
}

//https://learn.microsoft.com/en-us/answers/questions/735223/how-do-you-add-data-from-the-database-to-the-share