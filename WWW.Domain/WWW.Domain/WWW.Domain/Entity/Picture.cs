using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWW.Domain.Entity
{
    public class Picture
    {
        public int idPicture { get; set; }
        [Key, ForeignKey(nameof(idPicture))]
        public Event EventID { get; set; }

        public byte[] picture { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
