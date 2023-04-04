using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WWW.Domain.ViewModels.Category
{
    public class CategoryViewModal
    {
        [Display(Name = "Name of Category")]
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
    }
}
