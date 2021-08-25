using MyLibraryEF.Data;
using MyLibraryEF.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MyLibraryEF.Forms
{
    public partial class BorrowedForm : Form
    {
        private int currentId = 0;
        private readonly int userId;
        private readonly LibraryContext libContext;

        public BorrowedForm(int userId)
        {
            this.userId = userId;

            InitializeComponent();

            libContext = new LibraryContext();

            //SetMode(SqlDataAccess.GetUserState(userID));

            LoadBorrowed();
        }

        private void LoadBorrowed()
        {
            //DataSet ds = SqlDataAccess.LoadBooksBorrowed(userID);

            //dataGridViewBorrowed.DataSource = null;
            //dataGridViewBorrowed.DataSource = ds.Tables[0];
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

            //SqlDataAccess.SaveBook(book);
            libContext.Books.Add(book);
            //SqlDataAccess.DeleteBorrowedBook(currentId);


            libContext.SaveChanges();

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
