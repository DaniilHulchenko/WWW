using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WWW.Domain.Entity
{
    public class Article
    {
        [Key]
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
        public int CategoryID { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfModification { get; set; }
        public User Author { get; set; }
        public Category Category { get; set; }
        public List<Tags>? Tags { get; set; }
        public string slug { get; set; }



    }
}
