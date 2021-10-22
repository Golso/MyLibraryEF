using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryEF.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepo UserRepository { get; }
        IBookRepo BookRepository { get; }
        IBorrowedBookRepo BorrowedBookRepository { get; }
        void SaveChanges();
    }
}
