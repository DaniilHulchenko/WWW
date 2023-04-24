using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WWW.Domain.Enum;

namespace WWW.Domain.Entity
{
    [Table("Articles")]
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public int LocationId { get; set; }
        [ForeignKey(nameof(LocationId))]
        public Location FkLocationId { get; set; }

        public EventStatus Status { get; set; }

        public int AutorID { get; set; }
        [ForeignKey(nameof(AutorID))]
        public User FkAutorID { get; set; }

        public int CategoryID { get; set; }
        [ForeignKey(nameof(CategoryID))]
        public Category FkCategoryID { get; set; }

        public List<Tags>? Tags { get; set; }

        //public Picture Picture { get; set; }
        [DefaultValue(1)]
        public bool Published { get; set; }
        [DefaultValue(1)]
        public bool IsFavorite { get; set; }

        public string slug { get; set; }



    }
}
