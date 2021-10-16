using MyLibraryEF.Data;
using MyLibraryEF.Forms;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyLibraryEF
{
    public partial class MainForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
         (
              int nLeftRect,
              int nTopRect,
              int nRightRect,
              int nBottomRect,
              int nWidthEllipse,
              int nHeightEllipse
          );

        private readonly ILibraryService libCommands;

        private readonly int userId;
        private readonly int userState;

        public MainForm(int userId)
        {
            libCommands = new SqlLibraryService(new LibraryContext());

            this.userId = userId;
            userState = libCommands.GetUserState(userId);

            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            pnlFormLoader.Controls.Clear();
            MyBooksForm myBookForm = new MyBooksForm(userId) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            myBookForm.FormBorderStyle = FormBorderStyle.None;
            pnlFormLoader.Controls.Add(myBookForm);
            userNameLabel.Text = libCommands.GetUserName(this.userId);
            myBookForm.Show();

            ChangeMode(userState);
        }

        private void BtnMain_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Moje książki";
            pnlFormLoader.Controls.Clear();
            MyBooksForm myBookForm = new MyBooksForm(userId) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            myBookForm.FormBorderStyle = FormBorderStyle.None;
            pnlFormLoader.Controls.Add(myBookForm);
            myBookForm.Show();
        }

        private void BtnBorrowed_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Pożyczone";
            pnlFormLoader.Controls.Clear();
            BorrowedForm borrowedForm = new BorrowedForm(userId) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            borrowedForm.FormBorderStyle = FormBorderStyle.None;
            pnlFormLoader.Controls.Add(borrowedForm);
            borrowedForm.Show();
        }

        private void BtnWanted_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Do kupienia";
            pnlFormLoader.Controls.Clear();
            WantedForm wantedForm = new WantedForm(userId) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            wantedForm.FormBorderStyle = FormBorderStyle.None;
            pnlFormLoader.Controls.Add(wantedForm);
            wantedForm.Show();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Ustawienia";
            pnlFormLoader.Controls.Clear();
            SettingsForm settingsForm = new SettingsForm(userId, this) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            settingsForm.FormBorderStyle = FormBorderStyle.None;
            pnlFormLoader.Controls.Add(settingsForm);
            settingsForm.Show();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogoutLabel_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            Hide();
        }

        private void ChangeToNormalMode()
        {
            BackColor = Color.Navy;
            pnlNavigation.BackColor = Color.Navy;
            userPanel.BackColor = Color.MidnightBlue;
            pnlFormLoader.BackColor = Color.MediumBlue;

            lblTitle.ForeColor = Color.Black;
            userNameLabel.ForeColor = Color.Black;

            btnBorrowed.BackColor = Color.Navy;
            btnClose.BackColor = Color.Navy;
            btnMain.BackColor = Color.Navy;
            btnSettings.BackColor = Color.Navy;

            btnMain.ForeColor = Color.Black;
            btnBorrowed.ForeColor = Color.Black;
            btnSettings.ForeColor = Color.Black;
            btnWanted.ForeColor = Color.Black;
        }

        private void ChangeToDarkMode()
        {
            BackColor = Color.FromArgb(51, 51, 76);
            pnlNavigation.BackColor = Color.FromArgb(51, 51, 76);
            userPanel.BackColor = Color.FromArgb(39, 39, 58);
            pnlFormLoader.BackColor = Color.FromArgb(100, 100, 100);

            lblTitle.ForeColor = Color.Black;
            userNameLabel.ForeColor = Color.Black;

            btnBorrowed.BackColor = Color.FromArgb(51, 51, 76);
            btnClose.BackColor = Color.FromArgb(51, 51, 76);
            btnMain.BackColor = Color.FromArgb(51, 51, 76);
            btnSettings.BackColor = Color.FromArgb(51, 51, 76);

            btnMain.ForeColor = Color.DarkRed;
            btnBorrowed.ForeColor = Color.DarkRed;
            btnSettings.ForeColor = Color.DarkRed;
            btnWanted.ForeColor = Color.DarkRed;
        }

        public void ChangeMode(int state)
        {
            if (state == 1)
            {
                ChangeToDarkMode();
            }
            else
            {
                ChangeToNormalMode();
            }
        }

        private void Minimaliz_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
