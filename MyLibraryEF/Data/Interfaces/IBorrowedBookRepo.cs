using MyLibraryEF.Models;
using System.Windows.Forms;

namespace MyLibraryEF.Data.Interfaces
{
    public interface IBorrowedBookRepo
    {
        int GetAmountOfBorrowedBooks(int userId);
        void AddBorrowedBook(BorrowedBook borrowedBook);
        void RemoveBorrowedBook(int currentId);
        BindingSource BorowedBooksToBindingSource(int userId);

    }
}
