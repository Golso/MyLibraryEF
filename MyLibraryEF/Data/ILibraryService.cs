using MyLibraryEF.Models;
using System.Windows.Forms;

namespace MyLibraryEF.Data
{
    interface ILibraryService
    {
        bool LoginPasswordExists(string userName, string password);
        int GetUserId(string userName, string password);
        int GetUserState(int userId);
        string GetUserName(int userId);
        int GetAmountOfBooks(int userId);
        void SaveChanges();
        void AddUser(User user);
        BindingSource BorowedBooksToBindingSource(int userId);
        void AddBook(Book book);
        void RemoveBorrowedBook(int currentId);
        void RemoveUser(int userId);
        void ChangeStateOfUser(int userId, int state);
        BindingSource BooksToBindingSource(int userId, string toBuy);
        void RemoveBook(int currentId);
        void UpdateBook(int currentId, string title, string author);
        void AddBorrowedBook(BorrowedBook borrowedBook);
        BindingSource BindingSourceForSearch(int userId, string searchText);
        void UpdateToBuy(int currentId);
    }
}
