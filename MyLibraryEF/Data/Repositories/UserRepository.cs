using MyLibraryEF.Data.Interfaces;
using MyLibraryEF.Models;
using System.Linq;

namespace MyLibraryEF.Data
{
    class UserRepository : IUserRepo
    {
        private readonly LibraryContext _libContext;
        private readonly Encryption _encryption;

        public UserRepository(LibraryContext libContext)
        {
            _libContext = libContext;
            _encryption = new Encryption();
        }

        public void AddUser(User user)
        {
            var hashedPassword = _encryption.HashPassword(user.Password);
            user.Password = hashedPassword;

            _libContext.Users.Add(user);
        }

        public void ChangeStateOfUser(int userId, int state)
        {
            User user = _libContext.Users.Find(userId);
            user.State = state;
        }

        public void RemoveUser(int userId)
        {
            User user = _libContext.Users.Find(userId);
            _libContext.Users.Remove(user);
        }

        public int GetUserId(string userName, string password)
        {
            var hashedPassword = _encryption.HashPassword(password);
            int result = _libContext.Users.Where(user => (user.Login.Contains(userName) && user.Password.Contains(hashedPassword)))
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
            int result = _libContext.Users.Where(user => user.UserId == userId)
                .Select(x => x.State).FirstOrDefault();

            return result;
        }

        public bool LoginPasswordExists(string userName, string password)
        {
            var hashedPassword = _encryption.HashPassword(password);
            var sth = _libContext.Users.Where(user => (user.Login == userName && user.Password == hashedPassword)).Count();
            if (sth == 0)
                return false;
            return true;
        }
    }
}
