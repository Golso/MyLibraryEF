using MyLibraryEF.Data;
using MyLibraryEF.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyLibraryEF.Forms
{
    public partial class WantedForm : Form
    {
        private int currentId = 0;
        private readonly int userId;
        private readonly ILibraryService libCommand;

        public WantedForm(int userId)
        {
            this.userId = userId;
            libCommand = new SqlLibraryService(new LibraryContext());

            InitializeComponent();

            SetMode(libCommand.GetUserState(userId));

            LoadBooksList();
        }

        private void LoadBooksList()
        {
            BindingSource bi = libCommand.BooksToBindingSource(userId, "Tak");

            dataGridViewMain.DataSource = null;
            dataGridViewMain.DataSource = bi;
        }

        private void BtnAddBook_Click(object sender, EventArgs e)
        {
            if (titleText.Text != "" && autorText.Text != "")
            {
                Book book = new Book
                {
                    Title = titleText.Text,
                    Author = autorText.Text,
                    ToBuy = "Tak",
                    UserId = userId
                };

                libCommand.AddBook(book);
                libCommand.SaveChanges();

                titleText.Text = "";
                autorText.Text = "";
            }

            LoadBooksList();
        }

        private void BtnBought_Click(object sender, EventArgs e)
        {
            if (titleText.Text != "" && currentId != 0)
            {
                libCommand.UpdateToBuy(currentId);
                libCommand.SaveChanges();

                currentId = 0;
                titleText.Text = "";
                autorText.Text = "";
            }

            LoadBooksList();
        }

        private void BtnUpdateBook_Click(object sender, EventArgs e)
        {
            if (titleText.Text != "" && currentId != 0)
            {
                libCommand.UpdateBook(currentId, titleText.Text, autorText.Text);
                libCommand.SaveChanges();

                currentId = 0;
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
                    libCommand.RemoveBook(currentId);
                    libCommand.SaveChanges();
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
