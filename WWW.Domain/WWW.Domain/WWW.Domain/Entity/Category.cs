using System.ComponentModel.DataAnnotations;

namespace WWW.Domain.Entity
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        //[Display(Name = "Category Name")]
        //[Required(ErrorMessage = "You need to enter category name")]
        public string Name { get; set; }
        public string slug { get; set; }


        public virtual ICollection<Article> Articles { get; set; } // Навигационное свойство
        public override string ToString()
        {
            return $"Id={Id}, Name={Name}";
        }
    }
}
