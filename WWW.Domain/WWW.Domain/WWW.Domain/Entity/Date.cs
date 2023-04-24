using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWW.Domain.Entity
{
    public class Date
    {
        public int EventID { get; set; }
        [Key, ForeignKey(nameof(EventID))]
        public Event FkEventId { get; set; }

        public DateTime Date_of_Creation { get; set; }
        public DateTime Date_Of_Start { get; set; }
        public DateTime Date_Of_End { get; set; }
        public DateTime Date_Of_Updated { get; set; }

    }
}
