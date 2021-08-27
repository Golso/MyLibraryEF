using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLibraryEF.Models
{
    [Table("tblBorrowedBooks")]
    public class BorrowedBook
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ToWhom { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime? BorrowedTime { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
