using MyLibraryEF.Models;
using System.Data.Entity;

namespace MyLibraryEF.Data
{
    class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
