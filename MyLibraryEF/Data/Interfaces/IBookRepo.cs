using MyLibraryEF.Models;
using System.Windows.Forms;

namespace MyLibraryEF.Data.Interfaces
{
    public interface IBookRepo
    {
        int GetAmountOfBooks(int userId);
        void AddBook(Book book);
        void RemoveBook(int currentId);
        void UpdateBook(int currentId, string title, string author);
        void UpdateToBuyOfBook(int currentId);
        BindingSource BooksToBindingSource(int userId, string toBuy);
        BindingSource BindingSourceForSearchOfBooks(int userId, string searchText);

    }
}
