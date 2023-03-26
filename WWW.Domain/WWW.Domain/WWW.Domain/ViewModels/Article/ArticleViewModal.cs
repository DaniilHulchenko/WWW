using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WWW.Domain.Entity;

namespace WWW.Domain.ViewModels.Article
{
    public class ArticleViewModal
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
        [Required(ErrorMessage = "Upload Picture")]
        [MinLength(2, ErrorMessage = "Minimum lenght: 2")]
        public IFormFile Picture { get; set; }
        //IFormFile
        public bool Published { get; set; }
        public Category Category { get; set; }

    }
}
