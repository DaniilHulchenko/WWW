using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWW.Domain.Entity
{
    public class Location:DbBase
    {
        [Key]
        public int LocationID { get; set; }
        public string location { get; set; }
        public string City { get; set; }
        public string Building { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string Timezone { get; set; }

    }
}
