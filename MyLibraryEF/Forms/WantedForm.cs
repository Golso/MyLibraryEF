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
        private readonly UnitOfWork _unitOfWork;

        public WantedForm(int userId)
        {
            this.userId = userId;
            _unitOfWork = new UnitOfWork(new LibraryContext());

            InitializeComponent();

            SetMode(_unitOfWork.UserRepository.GetUserState(userId));

            LoadBooksList();
        }

        private void LoadBooksList()
        {
            BindingSource bi = _unitOfWork.BookRepository.BooksToBindingSource(userId, "Tak");

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

                _unitOfWork.BookRepository.AddBook(book);
                _unitOfWork.Save();

                titleText.Text = "";
                autorText.Text = "";
            }

            LoadBooksList();
        }

        private void BtnBought_Click(object sender, EventArgs e)
        {
            if (titleText.Text != "" && currentId != 0)
            {
                _unitOfWork.BookRepository.UpdateToBuyOfBook(currentId);
                _unitOfWork.Save();

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
                _unitOfWork.BookRepository.UpdateBook(currentId, titleText.Text, autorText.Text);
                _unitOfWork.Save();

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
                    _unitOfWork.BookRepository.RemoveBook(currentId);
                    _unitOfWork.Save();
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
