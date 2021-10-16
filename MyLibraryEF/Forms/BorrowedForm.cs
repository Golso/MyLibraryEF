using MyLibraryEF.Data;
using MyLibraryEF.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyLibraryEF.Forms
{
    public partial class BorrowedForm : Form
    {
        private int currentId = 0;
        private readonly int userId;
        private readonly ILibraryService libCommand;

        public BorrowedForm(int userId)
        {
            this.userId = userId;

            InitializeComponent();

            libCommand = new SqlLibraryService(new LibraryContext());

            SetMode(libCommand.GetUserState(userId));

            LoadBorrowed();
        }

        private void LoadBorrowed()
        {
            BindingSource bi = libCommand.BorowedBooksToBindingSource(userId);

            dataGridViewBorrowed.DataSource = null;
            dataGridViewBorrowed.DataSource = bi;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Book book = new Book
            {
                Title = titleText.Text,
                Author = autorText.Text,
                ToBuy = "Nie",
                UserId = userId
            };

            libCommand.AddBook(book);
            libCommand.RemoveBorrowedBook(currentId);
            libCommand.SaveChanges();

            currentId = 0;
            titleText.Text = "";
            autorText.Text = "";
            whoText.Text = "";

            LoadBorrowed();
        }

        private void DataGridViewBorrowed_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewBorrowed.Rows[e.RowIndex];

                currentId = Convert.ToInt32(row.Cells[0].Value);
                titleText.Text = row.Cells[1].Value.ToString();
                autorText.Text = row.Cells[2].Value.ToString();
                whoText.Text = row.Cells[3].Value.ToString();
            }
        }

        private void SetNormalMode()
        {
            BackColor = Color.MediumBlue;
        }

        private void SetDarkMode()
        {
            BackColor = Color.FromArgb(100, 100, 100);
        }

        private void SetMode(int state)
        {
            if (state == 1)
            {
                SetDarkMode();
            }
            else SetNormalMode();
        }
    }
}
