using MyLibraryEF.Data.Interfaces;

namespace MyLibraryEF.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepo _userRepository;
        private IBookRepo _bookRepository;
        private IBorrowedBookRepo _borrowedBookRepository;
        private readonly LibraryContext _libContext;

        public UnitOfWork(LibraryContext libContext)
        {
            _libContext = libContext;
        }

        public IUserRepo UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_libContext);
                }
                return _userRepository;
            }
        }

        public IBookRepo BookRepository
        {
            get
            {
                if (_bookRepository == null)
                {
                    _bookRepository = new BookRepository(_libContext);
                }
                return _bookRepository;
            }
        }

        public IBorrowedBookRepo BorrowedBookRepository
        {
            get
            {
                if (_borrowedBookRepository == null)
                {
                    _borrowedBookRepository = new BorrowedBookRepository(_libContext);
                }
                return _borrowedBookRepository;
            }
        }

        public void SaveChanges()
        {
            _libContext.SaveChanges();
        }
    }
}
