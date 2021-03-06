using MyLibraryEF.Data;
using MyLibraryEF.Data.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyLibraryEF.Forms
{
    public partial class SettingsForm : Form
    {
        private readonly int userId;
        private readonly MainForm main;
        private readonly IUnitOfWork _unitOfWork;
        public SettingsForm(int userId, MainForm main)
        {
            this.userId = userId;
            this.main = main;

            InitializeComponent();

            _unitOfWork = new UnitOfWork(new LibraryContext());

            SetMode(_unitOfWork.UserRepository.GetUserState(this.userId));
        }

        private void BtnDeleteAccount_Click(object sender, EventArgs e)
        {
            DialogResult delete = MessageBox.Show("Are you sure you want to delete your account?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (delete == DialogResult.Yes)
            {
                _unitOfWork.UserRepository.RemoveUser(userId);
                _unitOfWork.SaveChanges();

                new LoginForm().Show();
                main.Hide();
            }
        }

        private void BtnNormalMode_Click(object sender, EventArgs e)
        {
            _unitOfWork.UserRepository.ChangeStateOfUser(userId, 0);
            _unitOfWork.SaveChanges();

            main.ChangeMode(0);
            SetMode(0);
        }

        private void BtnBlackMode_Click(object sender, EventArgs e)
        {
            _unitOfWork.UserRepository.ChangeStateOfUser(userId, 1);
            _unitOfWork.SaveChanges();

            main.ChangeMode(1);
            SetMode(1);
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
                SetDarkMode();
            else SetNormalMode();
        }
    }
}
