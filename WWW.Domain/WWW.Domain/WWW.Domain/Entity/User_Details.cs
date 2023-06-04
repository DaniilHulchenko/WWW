using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace WWW.Domain.Entity
{
    public class User_Details:DbBase
    {
        [Key, ForeignKey(nameof(User))]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        [AllowNull]
        public string? Password { get; set; }

        [AllowNull]
        public string? Introdaction { get; set; }

        //[AllowNull]
        //public string? RealName { get; set; }
        //[AllowNull, Phone]
        //public string? PhoneAttribute { get; set; }
    }
}
