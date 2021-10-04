using MyLibraryEF.Data;
using MyLibraryEF.Models;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyLibraryEF.Forms
{
    public partial class MyBooksForm : Form
    {
        private int currentId = 0;
        private readonly int userId;
        private readonly ILibraryService libCommands;

        public MyBooksForm(int userId)
        {
            this.userId = userId;
            libCommands = new SqlLibraryService(new LibraryContext());

            InitializeComponent();

            SetMode(libCommands.GetUserState(userId));

            LoadBooksList();
        }

        public void LoadBooksList()
        {
            txtBoxTitleSearch.Text = "Szukaj po tytule...";

            BindingSource bi = libCommands.BooksToBindingSource(userId, "Nie");
            
            dataGridViewMain.DataSource = null;
            dataGridViewMain.DataSource = bi;


            lblBooksAmount.Text = "Ilość posiadanych książek: " + libCommands.GetAmountOfBooks(userId);
        }

        private void BtnAddBook_Click(object sender, EventArgs e)
        {
            if (titleText.Text != "" && autorText.Text != "")
            {
                Book book = new Book
                {
                    Title = titleText.Text,
                    Author = autorText.Text,
                    ToBuy = "Nie",
                    UserId = userId
                };

                libCommands.AddBook(book);
                libCommands.SaveChanges();

                titleText.Text = "";
                autorText.Text = "";
            }

            LoadBooksList();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentId != 0)
                {
                    libCommands.RemoveBook(currentId);
                    libCommands.SaveChanges();
                }

                currentId = 0;
                titleText.Text = "";
                autorText.Text = "";
            }
            catch (Exception exemp)
            {
                MessageBox.Show(exemp.Message);
            }

            LoadBooksList();
        }

        private void DataGridViewMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewMain.Rows[e.RowIndex];

                currentId = Convert.ToInt32(row.Cells[0].Value);
                titleText.Text = row.Cells[1].Value.ToString();
                autorText.Text = row.Cells[2].Value.ToString();
            }
        }

        private void BtnUpdateBook_Click(object sender, EventArgs e)
        {
            if (titleText.Text != "" && currentId != 0)
            {
                libCommands.UpdateBook(currentId, titleText.Text, autorText.Text);
                libCommands.SaveChanges();

                currentId = 0;
                titleText.Text = "";
                autorText.Text = "";
            }

            LoadBooksList();
        }

        private void BtnBorrow_Click(object sender, EventArgs e)
        {
            if (titleText.Text != "" && currentId != 0)
            {
                BorrowedBook book = new BorrowedBook
                {
                    Title = titleText.Text,
                    Author = autorText.Text,
                    UserId = userId
                };

                new WhoBorrowsForm(book, currentId, this).Show();

                currentId = 0;
                titleText.Text = "";
                autorText.Text = "";
            }

            LoadBooksList();
        }

        private void TxtBoxTitleSearch_TextChanged(object sender, EventArgs e)
        {
            BindingSource bi = libCommands.BindingSourceForSearch(userId, txtBoxTitleSearch.Text);

            dataGridViewMain.DataSource = null;
            dataGridViewMain.DataSource = bi;
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
