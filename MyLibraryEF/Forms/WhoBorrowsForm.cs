using MyLibraryEF.Models;
using System;
using System.Windows.Forms;

namespace MyLibraryEF.Forms
{
    public partial class WhoBorrowsForm : Form
    {
        private readonly BorrowedBook book;
        private readonly int currentID;
        private readonly MyBooksForm form;

        public WhoBorrowsForm(BorrowedBook book, int currentID, MyBooksForm form)
        {
            this.book = book;
            this.currentID = currentID;
            this.form = form;

            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            //book.Komu = txtBoxWho.Text;
            //SqlDataAccess.SaveBorrowedBook(book);
            //SqlDataAccess.DeleteBook(currentID);

            form.LoadBooksList();

            Hide();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
