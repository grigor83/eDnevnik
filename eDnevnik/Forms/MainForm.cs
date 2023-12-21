using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvidencijaIzostanaka
{
    public partial class MainForm : Form
    {
        List<User> users;
        public List<string> allUsernames;

        public MainForm()
        {
            users = DBeDnevnik.LoadAllUsers();
            loadAllUsernames();
            InitializeComponent();
        }

        private void loadAllUsernames()
        {
            allUsernames = new List<string>();
            foreach (User user in users)
                allUsernames.Add(user.username);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUserName.Text.Trim();
            string password = tbPassword.Text.Trim();
            User korisnik = null;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Niste unijeli sve potrebne podatke!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (users == null)
                    return;

                foreach (User k in users)
                    if (k.username.Equals(username) && k.password.Equals(password))
                    {
                        if (k.accountType.Equals("administrator"))
                            new Administrator2(this, k).Show();
                        else
                            new TeacherForm(this, (Teacher) k).Show();
                        tbPassword.Text = "";
                        tbUserName.Text = "";
                        tbUserName.Focus();
                        this.Hide();
                        korisnik = k;
                        break;
                    }
                if (korisnik == null)
                    MessageBox.Show("Uneseni podaci nisu tačni!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin.PerformClick();
            }
        }
    }
}
