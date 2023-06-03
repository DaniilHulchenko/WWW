using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWW.Domain.Entity
{
    [NotMapped]
    public class Favorite
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int EventId { get; set; }
        public virtual Article Event { get; set; }
    }
}
