using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using WWW.Domain.Entity;

namespace WWW.Domain.ViewModels.Article
{
    public class ArticleCreateViewModal
    {

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Enter Title")]
        [MinLength(2, ErrorMessage = "Minimum lenght: 2")]
        public string Title { get; set; }

        [Display(Name = "ShortDescription")]
        [Required(ErrorMessage = "Enter ShortDescription")]
        [MinLength(2, ErrorMessage = "Minimum lenght: 2")]
        public string ShortDescription { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Enter Description")]
        [MinLength(2, ErrorMessage = "Minimum lenght: 2")]
        public string Description { get; set; }

        [Display(Name = "Picture")]
        [AllowNull]
        public IFormFile Picture { get; set; }

        public bool Published { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Enter Category ID")]
        public int Category { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Enter Location")]
        public string Location { get; set; }

        [Display(Name = "Date Of Article")]
        [Required(ErrorMessage = "Enter Date Of Article")]
        public DateTime DateOfArticle { get; set; }

        public string User { get; set; }

    }
}
