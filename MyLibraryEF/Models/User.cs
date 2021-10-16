using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLibraryEF.Models
{
    [Table("tblUsers")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int State { get; set; }
    }
}
