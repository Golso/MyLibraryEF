using MyLibraryEF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryEF.Data
{
    class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
