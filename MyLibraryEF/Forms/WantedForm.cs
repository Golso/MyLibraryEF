using MyLibraryEF.Data;
using MyLibraryEF.Models;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyLibraryEF.Forms
{
    public partial class WantedForm : Form
    {
        private int currentId = 0;
        private readonly int userId;
        private readonly LibraryContext libContext;
        private readonly SqlLibraryRepo libCommand;

        public WantedForm(int userId)
        {
            this.userId = userId;
            libContext = new LibraryContext();
            libCommand = new SqlLibraryRepo(libContext);

            InitializeComponent();

            SetMode(libCommand.GetUserState(userId));

            LoadBooksList();
        }

        private void LoadBooksList()
        {
            BindingSource bi = new BindingSource();

            var query = libContext.Books.Where(book => book.UserId==userId && book.ToBuy=="Tak")
                .Select(book => new { book.Id, book.Title, book.Author }).ToList();

            bi.DataSource = query;

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

                libContext.Books.Add(book);
                libContext.SaveChanges();

                titleText.Text = "";
                autorText.Text = "";
            }

            LoadBooksList();
        }

        private void BtnBought_Click(object sender, EventArgs e)
        {
            if (titleText.Text != "" && currentId != 0)
            {
                Book book = libContext.Books.Find(currentId);
                book.ToBuy = "Nie";
                libContext.SaveChanges();

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
                Book book = libContext.Books.Find(currentId);
                book.Title = titleText.Text;
                book.Author = autorText.Text;
                libContext.SaveChanges();

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
                    Book book = libContext.Books.Find(currentId);
                    libContext.Books.Remove(book);
                    libContext.SaveChanges();
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
