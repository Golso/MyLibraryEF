using MyLibraryEF.Data;
using MyLibraryEF.Data.Interfaces;
using MyLibraryEF.Models;
using System;
using System.Windows.Forms;

namespace MyLibraryEF.Forms
{
    public partial class WhoBorrowsForm : Form
    {
        private readonly BorrowedBook bookBorrowed;
        private readonly int currentId;
        private readonly MyBooksForm form;
        private readonly IUnitOfWork _unitOfWork;

        public WhoBorrowsForm(BorrowedBook bookBorrowed, int currentId, MyBooksForm form)
        {
            this.bookBorrowed = bookBorrowed;
            this.currentId = currentId;
            this.form = form;
            _unitOfWork = new UnitOfWork(new LibraryContext());

            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (txtBoxWho.Text != string.Empty)
            {
                bookBorrowed.ToWhom = txtBoxWho.Text;
                bookBorrowed.BorrowedTime = borrowedDatePicker.Value;

                _unitOfWork.BorrowedBookRepository.AddBorrowedBook(bookBorrowed);
                _unitOfWork.BookRepository.RemoveBook(currentId);
                _unitOfWork.SaveChanges();

                form.LoadBooksList();

                Hide();

                //Not sure if its closing by itself
                //libContext.Dispose();
            }
            else
            {
                MessageBox.Show("Providing the name of the borrower is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
