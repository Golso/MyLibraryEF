using System;
using Xunit;
using Moq;
using MyLibraryEF;
using MyLibraryEF.Data;
using MyLibraryEF.Models;

namespace MyLibraryEFXUnitTests
{
    public class BorrowedBookRepositoryTest
    {
        [Fact]
        public void GetAmountOfBorrowedBooks()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<LibraryContext>();

        }
    }
}
