using EvidencijaIzostanaka.Forms;
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
    public partial class RasporedČasova : Form
    {
        private SchoolClass schoolClass;

        public RasporedČasova(SchoolClass selectedClass)
        {
            InitializeComponent();
            this.schoolClass = selectedClass;
            lbRazred.Text = "Razred: " + selectedClass;
            this.Text = Program.APP_NAME;
            Administrator2.PopulateComboBoxSubjects(cbIzaberiPredmet);
            List<User> users = DBeDnevnik.LoadAllUsers();
            users.ForEach(user => {
                if (user.accountType.Equals("nastavnik"))
                    cbIzaberiNastavnika.Items.Add(user);
            });
            for (int i = 1; i < 7; i++)
                cbRedniBroj.Items.Add(i);
            FillDataGridLessons();
        }

        private void FillDataGridLessons()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DBeDnevnik.LoadLessons(schoolClass);
            dataGridView1.Columns["subjectName"].HeaderText = "Naziv predmeta";
            dataGridView1.Columns["date"].HeaderText = "Datum časa";
            dataGridView1.Columns["redniBroj"].HeaderText = "Redni broj časa";
            dataGridView1.Columns["razlog"].Visible = false;
            dataGridView1.Columns["tip"].Visible = false;
            dataGridView1.Columns["id"].Visible = false;
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbIzaberiPredmet.Text) || string.IsNullOrEmpty(cbIzaberiNastavnika.Text) ||
                string.IsNullOrEmpty(dateTimePicker1.Text) || string.IsNullOrEmpty(cbRedniBroj.Text))
            {
                MessageBox.Show("Niste unijeli potpune podatke!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String subjectName = cbIzaberiPredmet.SelectedItem.ToString();
            User teacher = cbIzaberiNastavnika.SelectedItem as User;
            DateTime date = dateTimePicker1.Value.Date;
            int n = int.Parse(cbRedniBroj.SelectedItem.ToString());
            cbIzaberiPredmet.SelectedIndex = -1;
            cbIzaberiNastavnika.SelectedIndex = -1; dateTimePicker1.Value = DateTime.Now;
            cbRedniBroj.SelectedIndex = -1;

            if (DBeDnevnik.InsertČas(schoolClass, subjectName, teacher, date, n))
            {
                MessageBox.Show("Dodijelili ste novi predmet ovom razredu");
                FillDataGridLessons();
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var result = new DeleteForm().ShowDialog();
                if (result == DialogResult.Yes)
                {
                    LessonSimple selectedLesson = dataGridView1.SelectedRows[0].DataBoundItem as LessonSimple;
                    if (DBeDnevnik.DeleteČas(schoolClass, selectedLesson))
                    {
                        FillDataGridLessons();
                        MessageBox.Show("Obrisali ste čas");
                    }
                }
            }
        }
    }
}
