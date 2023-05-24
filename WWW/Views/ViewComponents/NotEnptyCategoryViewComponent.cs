
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
    public class NotEnptyCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<NotEnptyCategoryViewComponent> _logger;
        public NotEnptyCategoryViewComponent(ICategoryRepository categoryRepository, ILogger<NotEnptyCategoryViewComponent> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _categoryRepository.GetNotEmptyCategory();
            //Console.WriteLine($"!!!{data}");
            return View("Index", data);
        }
        //public async Task<IViewComponentHelper> Allcat()
        //{
        //    return _categoryRepository.GetALL();
        //}
    }
}

//https://learn.microsoft.com/en-us/answers/questions/735223/how-do-you-add-data-from-the-database-to-the-share