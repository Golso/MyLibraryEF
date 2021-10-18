using MyLibraryEF.Data.Interfaces;
using MyLibraryEF.Models;
using System.Linq;
using System.Windows.Forms;

namespace MyLibraryEF.Data
{
    class BorrowedBookRepository : IBorrowedBookRepo
    {
        private readonly LibraryContext _libContext;

        public BorrowedBookRepository(LibraryContext libContext)
        {
            _libContext = libContext;
        }

        public int GetAmountOfBorrowedBooks(int userID)
        {
            int amountOfBorrowedBooks = _libContext.BorrowedBooks.Where(book => book.UserId == userID).Count();

            return amountOfBorrowedBooks;
        }

        public void AddBorrowedBook(BorrowedBook borrowedBook)
        {
            _libContext.BorrowedBooks.Add(borrowedBook);
        }

        public void RemoveBorrowedBook(int currentId)
        {
            BorrowedBook returnedBook = _libContext.BorrowedBooks.Find(currentId);
            _libContext.BorrowedBooks.Remove(returnedBook);
        }

        public BindingSource BorowedBooksToBindingSource(int userId)
        {
            BindingSource bi = new BindingSource();

            var query = _libContext.BorrowedBooks.Where(book => book.UserId == userId)
                .Select(book => new { book.Id, book.Title, book.Author, book.ToWhom, book.BorrowedTime }).ToList();

            bi.DataSource = query;

            return bi;
        }
    }
}
