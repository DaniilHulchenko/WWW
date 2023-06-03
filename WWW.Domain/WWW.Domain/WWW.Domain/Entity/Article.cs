using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using WWW.Domain.Enum;
using WWW.Domain.ViewModels.Article;
using static Grpc.Core.Metadata;

namespace WWW.Domain.Entity
{
    [Table("Articles")]
    public class Article: DbBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public ArticleStatus Status { get; set; }

        public virtual Location Location { get; set; }
        [NotMapped]
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


        public virtual List<User>? User{ get; set;}

        public Article() { }
        public Article(ArticleCreateViewModal entity) {
            Title = entity.Title;
            ShortDescription = entity.ShortDescription;
            Description = entity.Description;
            Published = entity.Published;
        }
    }
}
