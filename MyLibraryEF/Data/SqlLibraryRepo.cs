using System.Linq;

namespace MyLibraryEF.Data
{
    class SqlLibraryRepo : ILibraryRepo
    {
        private readonly LibraryContext _libContext;

        public SqlLibraryRepo(LibraryContext libraryContext)
        {
            _libContext = libraryContext;
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
                .Select(x => x.UserId).FirstOrDefault();

            return result;
        }

        public bool LoginPasswordExists(string userName, string password)
        {
            var sth = _libContext.Users.Where(user => (user.Login==userName && user.Password==password)).Count();
            if (sth == 0)
                return false;
            return true;
        }
    }
}
