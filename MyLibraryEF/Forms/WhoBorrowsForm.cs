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
        private readonly ILibraryService libCommand;

        public WhoBorrowsForm(BorrowedBook bookBorrowed, int currentId, MyBooksForm form)
        {
            this.bookBorrowed = bookBorrowed;
            this.currentId = currentId;
            this.form = form;
            libCommand = new SqlLibraryService(new LibraryContext());

            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (txtBoxWho.Text != string.Empty)
            {
                bookBorrowed.ToWhom = txtBoxWho.Text;
                bookBorrowed.BorrowedTime = borrowedDatePicker.Value;

                libCommand.AddBorrowedBook(bookBorrowed);
                libCommand.RemoveBook(currentId);
                libCommand.SaveChanges();

                form.LoadBooksList();

                Hide();

                //Not sure if its closing by itself
                //libContext.Dispose();
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
