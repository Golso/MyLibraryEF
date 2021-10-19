using MyLibraryEF.Data;
using System;
using System.Windows.Forms;

namespace MyLibraryEF.Forms
{
    public partial class LoginForm : Form
    {
        private readonly UnitOfWork _unitOfWork;

        public LoginForm()
        {
            InitializeComponent();

            _unitOfWork = new UnitOfWork(new LibraryContext());
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUsername.Text;
            string password = txtPassword.Text;

            if (_unitOfWork.UserRepository.LoginPasswordExists(userName, password))
            {
                int userID = _unitOfWork.UserRepository.GetUserId(userName, password);
                new MainForm(userID).Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Incorrect login or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            checkBoxShowPass.Checked = false;
        }

        private void LabelCreate_Click(object sender, EventArgs e)
        {
            new RegisterForm().Show();
            Hide();
        }

        private void CheckBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPass.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLogin_Click(this, new EventArgs());
            }
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
