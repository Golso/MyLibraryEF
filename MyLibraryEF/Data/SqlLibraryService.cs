using MyLibraryEF.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyLibraryEF.Data
{
    class SqlLibraryService : ILibraryService
    {
        private readonly LibraryContext _libContext;

        public SqlLibraryService(LibraryContext libraryContext)
        {
            _libContext = libraryContext;
        }

        public void AddBook(Book book)
        {
            _libContext.Books.Add(book);
        }

        public void AddBorrowedBook(BorrowedBook borrowedBook)
        {
            _libContext.BorrowedBooks.Add(borrowedBook);
        }

        public void AddUser(User user)
        {
            _libContext.Users.Add(user);
        }

        public BindingSource BindingSourceForSearch(int userId, string searchText)
        {
            BindingSource bi = new BindingSource();

            var query = _libContext.Books
                .Where(book => book.UserId == userId && book.Title.Contains(searchText))
                .Select(book => new { book.Id, book.Title, book.Author }).ToList();

            bi.DataSource = query;

            return bi;
        }

        public BindingSource BooksToBindingSource(int userId, string toBuy)
        {
            BindingSource bi = new BindingSource();

            var query = _libContext.Books.Where(book => book.UserId == userId && book.ToBuy == toBuy)
                .Select(book => new { book.Id, book.Title, book.Author }).ToList();

            bi.DataSource = query;

            return bi;
        }

        public BindingSource BorowedBooksToBindingSource(int userId)
        {
            BindingSource bi = new BindingSource();

            var query = _libContext.BorrowedBooks.Where(book => book.UserId == userId)
                .Select(book => new { book.Id, book.Title, book.Author, book.ToWhom, book.BorrowedTime }).ToList();

            bi.DataSource = query;

            return bi;
        }

        public void ChangeStateOfUser(int userId, int state)
        {
            User user = _libContext.Users.Find(userId);
            user.State = state;
        }

        public int GetAmountOfBooks(int userID)
        {
            int resultBooks = _libContext.Books.Where(book => book.UserId==userID && book.ToBuy=="Nie").Count();
            int resultBorrowed = _libContext.BorrowedBooks.Where(book => book.UserId==userID).Count();

            return resultBooks + resultBorrowed;
        }

        public int GetUserId(string userName, string password)
        {
            int result = _libContext.Users.Where(user => (user.Login.Contains(userName) && user.Password.Contains(password)))
                .Select(x => x.UserId).FirstOrDefault();

            return result;
        }

        public string GetUserName(int userId)
        {
            string result = _libContext.Users.Where(user => user.UserId == userId)
                .Select(x => x.Login).FirstOrDefault();

            return result;
        }

        public int GetUserState(int userId)
        {
            int result = _libContext.Users.Where(user => user.UserId==userId)
                .Select(x => x.State).FirstOrDefault();

            return result;
        }

        public bool LoginPasswordExists(string userName, string password)
        {
            var sth = _libContext.Users.Where(user => (user.Login==userName && user.Password==password)).Count();
            if (sth == 0)
                return false;
            return true;
        }

        public void RemoveBook(int currentId)
        {
            var book = _libContext.Books.Find(currentId);
            _libContext.Books.Remove(book);
        }

        public void RemoveBorrowedBook(int currentId)
        {
            BorrowedBook returnedBook = _libContext.BorrowedBooks.Find(currentId);
            _libContext.BorrowedBooks.Remove(returnedBook);
        }

        public void RemoveUser(int userId)
        {
            User user = _libContext.Users.Find(userId);
            _libContext.Users.Remove(user);
        }

        public void SaveChanges()
        {
            _libContext.SaveChanges();
        }

        public void UpdateBook(int currentId, string title, string author)
        {
            var book = _libContext.Books.Find(currentId);
            book.Title = title;
            book.Author = author;
        }

        public void UpdateToBuy(int currentId)
        {
            Book book = _libContext.Books.Find(currentId);
            book.ToBuy = "Nie";
        }
    }
}
