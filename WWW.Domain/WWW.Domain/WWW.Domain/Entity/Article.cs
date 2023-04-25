using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WWW.Domain.Enum;

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
        public ArticleStatus Status { get; set; }

        public virtual Location Location { get; set; }
        public virtual User Autor { get; set; }
        public virtual Category Category { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual Date Date { get; set; }
        public virtual List<Tags>? Tags { get; set; }

        [DefaultValue(1)]
        public bool Published { get; set; }
        [DefaultValue(1)]
        public bool IsFavorite { get; set; }

        public string slug { get; set; }



    }
}
