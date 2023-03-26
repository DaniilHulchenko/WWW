using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WWW.Domain.Entity
{
    [Table("Articles")]
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public byte[]? Picture { get; set; }
        [DefaultValue(1)]
        public bool Published { get; set; }
        [DefaultValue(1)]
        public bool IsFavorite { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfEvent { get; set; }

        [ForeignKey("Author")]
        public int AutorID { get; set; }
        public User Author { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public List<Tags>? Tags { get; set; }
        public string slug { get; set; }



    }
}
