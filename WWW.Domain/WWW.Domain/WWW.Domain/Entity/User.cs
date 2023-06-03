using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using WWW.Domain.Enum;

namespace WWW.Domain.Entity
{
    public class User:DbBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        [AllowNull]
        public byte[]? Avatar { get; set; }
        public UserRole Role { get; set; }

        public virtual User_Details Details { get; set; }
        public virtual List<Article>? Event { get; set; }

    }
}
