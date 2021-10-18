using MyLibraryEF.Data.Interfaces;
using MyLibraryEF.Models;
using System.Linq;
using System.Windows.Forms;

namespace MyLibraryEF.Data
{
    class BookRepository : IBookRepo
    {
        private readonly LibraryContext _libContext;

        public BookRepository(LibraryContext libContext)
        {
            _libContext = libContext;
        }

        public void AddBook(Book book)
        {
            _libContext.Books.Add(book);
        }

        public int GetAmountOfBooks(int userID)
        {
            int amountOfBooks = _libContext.Books.Where(book => book.UserId == userID && book.ToBuy == "Nie").Count();

            return amountOfBooks;
        }

        public void UpdateBook(int currentId, string title, string author)
        {
            var book = _libContext.Books.Find(currentId);
            book.Title = title;
            book.Author = author;
        }

        public void UpdateToBuyOfBook(int currentId)
        {
            Book book = _libContext.Books.Find(currentId);
            book.ToBuy = "Nie";
        }

        public void RemoveBook(int currentId)
        {
            var book = _libContext.Books.Find(currentId);
            _libContext.Books.Remove(book);
        }

        public BindingSource BooksToBindingSource(int userId, string toBuy)
        {
            BindingSource bi = new BindingSource();

            var query = _libContext.Books.Where(book => book.UserId == userId && book.ToBuy == toBuy)
                .Select(book => new { book.Id, book.Title, book.Author }).ToList();

            bi.DataSource = query;

            return bi;
        }

        public BindingSource BindingSourceForSearchOfBooks(int userId, string searchText)
        {
            BindingSource bi = new BindingSource();

            var query = _libContext.Books
                .Where(book => book.UserId == userId && book.Title.Contains(searchText))
                .Select(book => new { book.Id, book.Title, book.Author }).ToList();

            bi.DataSource = query;

            return bi;
        }
    }
}