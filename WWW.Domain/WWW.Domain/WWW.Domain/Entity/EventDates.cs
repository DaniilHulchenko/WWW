using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WWW.Domain.Entity
{
    public class EventDates:DbBase
    {
        //[Key]
        [Key,ForeignKey(nameof(Article))]
        public int ArticleID { get; set; }
        public virtual Article Article { get; set; }

        public DateTime Date_of_Creation { get; set; }
        public DateTime Date_Of_Start { get; set; }
        //public DateTime Date_Of_End { get; set; }
        public DateTime Date_Of_Updated { get; set; }

    }
}
