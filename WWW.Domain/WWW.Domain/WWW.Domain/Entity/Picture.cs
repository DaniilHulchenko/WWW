using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWW.Domain.Entity
{
    public class Picture:DbBase
    {
        //[Key]
        [Key,ForeignKey(nameof(Article))]
        public int PictureID { get; set; }
        public virtual Article Article { get; set; }

        public byte[] picture { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
