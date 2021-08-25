using MyLibraryEF.Data;
using MyLibraryEF.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyLibraryEF.Forms
{
    public partial class SettingsForm : Form
    {
        private readonly int userId;
        private readonly MainForm main;
        private readonly LibraryContext libContext;
        private readonly SqlLibraryRepo libCommand;
        public SettingsForm(int userId, MainForm main)
        {
            this.userId = userId;
            this.main = main;

            InitializeComponent();

            libContext = new LibraryContext();
            libCommand = new SqlLibraryRepo(libContext);

            SetMode(libCommand.GetUserState(this.userId));
        }

        private void BtnDeleteAccount_Click(object sender, EventArgs e)
        {
            DialogResult delete = MessageBox.Show("Czy na pewno chcesz usunąć konto?", "Na pewno?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (delete == DialogResult.Yes)
            {
                User user = libContext.Users.Find(userId);
                libContext.Users.Remove(user);
                libContext.SaveChanges();

                new LoginForm().Show();
                main.Hide();
            }
        }

        private void BtnNormalMode_Click(object sender, EventArgs e)
        {
            User user = libContext.Users.Find(userId);
            user.State = 0;
            libContext.SaveChanges();

            main.ChangeMode(0);
            SetMode(0);
        }

        private void BtnBlackMode_Click(object sender, EventArgs e)
        {
            User user = libContext.Users.Find(userId);
            user.State = 1;
            libContext.SaveChanges();

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
