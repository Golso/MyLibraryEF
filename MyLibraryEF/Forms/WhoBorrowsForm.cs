using MyLibraryEF.Data;
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
        private readonly LibraryContext libContext;

        public WhoBorrowsForm(BorrowedBook bookBorrowed, int currentId, MyBooksForm form)
        {
            this.bookBorrowed = bookBorrowed;
            this.currentId = currentId;
            this.form = form;
            libContext = new LibraryContext();

            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (txtBoxWho.Text != string.Empty)
            {
                bookBorrowed.ToWhom = txtBoxWho.Text;
                bookBorrowed.BorrowedTime = borrowedDatePicker.Value;

                libContext.BorrowedBooks.Add(bookBorrowed);
                var book = libContext.Books.Find(currentId);
                libContext.Books.Remove(book);
                libContext.SaveChanges();

                form.LoadBooksList();

                Hide();
            }
            else
            {
                MessageBox.Show("Podanie nazwy pożyczającego jest wymagane.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
