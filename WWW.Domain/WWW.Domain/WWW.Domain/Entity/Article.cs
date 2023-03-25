using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace WWW.Domain.Entity
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        //public Author Author { get; set; }
        public byte[]? Picture { get; set; }

        public bool Published { get; set; }
        public bool IsFavorite { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }



    }
}
