using EvidencijaIzostanaka;
using EvidencijaIzostanaka.Forms;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace EvidencijaIzostanaka
{
    public partial class Administrator2 : Form
    {
        private MainForm mainForm { get; set; }
        private List<User> Korisnici { get; set; }
        private List<SchoolClass> classes { get; set; }

        private User selectedUser;
        private SchoolClass selectedClass, oldClass;
        private Subject oldSubject, selectedSubject;
        public Student oldStudent, selectedStudent;
        private string oldCellValue;
        private System.Windows.Forms.ComboBox problem, comboboxRazredi;

        public Administrator2(MainForm mainForm, User k)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Text = Program.APP_NAME;
            lbAdministratorName.Text = "Administrator: " + k.name;
            this.label7.Text = "Administrator: " + k.name;
            this.label12.Text = "Administrator: " + k.name;
            this.label20.Text = "Administrator: " + k.name;
            FillDataGridUsers();
            PopulateComboBoxSubjects(cbSelectSubject);
            PopulateComboBoxCabinets(cbIzaberiKabinet);
            LoadChildWhitoutParents();
            cbTopŠkolskaGodina.DropDownClosed += (s, args) =>
            {
                loadTabClasses(false);
            };
            FillDataGridSubjects();

            foreach (String s in Program.SCHOOL_YEARS)
                cbŠkolskaGodina.Items.Add(s);
            cbŠkolskaGodina.Items.Add("Svi učenici");
            cbŠkolskaGodina.SelectedIndex = Program.SCHOOL_YEARS.Count - 1;
            cbŠkolskaGodina.DropDownClosed += (s, args) =>
            {
                FillDataGridStudents();
            };
            FillDataGridStudents();
        }

        private void FillDataGridUsers()
        {
            dataGridView1.DataSource = null;
            Korisnici = DBeDnevnik.LoadAllUsers();
            dataGridView1.DataSource = Korisnici;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["username"].Visible = false;
            dataGridView1.Columns["password"].Visible = false;
            dataGridView1.Columns["name"].HeaderText = "Ime i prezime";
            dataGridView1.Columns["accountType"].HeaderText = "Tip naloga";
            dataGridView1.Columns["education"].HeaderText = "Obrazovanje";
            dataGridView1.Columns["children"].HeaderText = "Djeca";
        }

        public static void PopulateComboBoxSubjects(System.Windows.Forms.ComboBox cmb)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=e_dnevnik; uid=root; password=mojabaza;");
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM predmet";
                MySqlDataReader reader = cmd.ExecuteReader();
                cmb.Items.Clear();
                while (reader.Read())
                    cmb.Items.Add(reader.GetString(0));
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nije uspjelo učitavanje predmeta!");
            }
        }

        public static void PopulateComboBoxCabinets(System.Windows.Forms.ComboBox cmb)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=e_dnevnik; uid=root; password=mojabaza;");
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM kabinet";
                MySqlDataReader reader = cmd.ExecuteReader();
                cmb.Items.Clear();
                while (reader.Read())
                    cmb.Items.Add(reader.GetString(0));
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nije uspjelo učitavanje kabineta!");
            }
        }

        private void LoadChildWhitoutParents()
        {
            dgvIzaberiDijete.DataSource = null;
            dgvIzaberiDijete.DataSource = DBeDnevnik.LoadStudentsWhithoutParents();
            dgvIzaberiDijete.Columns["studentId"].Visible = false;
            dgvIzaberiDijete.Columns["studentClass"].Visible = false;
            dgvIzaberiDijete.Columns["Prisustvo"].Visible = false;
            dgvIzaberiDijete.Columns["Ocjena"].Visible = false;
            dgvIzaberiDijete.Columns["OpštiUspjeh"].Visible = false;
            dgvIzaberiDijete.Columns["Vladanje"].Visible = false;
        }

        private void FillDataGridStudents()
        {
            dgvSpisakUčenika.DataSource = null;
            dgvSpisakUčenika.DataSource = DBeDnevnik.LoadStudentsBySchoolyear(cbŠkolskaGodina.SelectedItem.ToString());
            dgvSpisakUčenika.Columns["studentID"].Visible = false;
            dgvSpisakUčenika.Columns["prisustvo"].Visible = false;
            dgvSpisakUčenika.Columns["Ocjena"].Visible = false;
            dgvSpisakUčenika.Columns["Vladanje"].Visible = false;
            dgvSpisakUčenika.Columns["studentName"].HeaderText = "Ime i prezime";
            dgvSpisakUčenika.Columns["studentClass"].HeaderText = "Razred i odjeljenje";
            dgvSpisakUčenika.Columns["OpštiUspjeh"].HeaderText = "Opšti uspjeh";

            cbIzaberiRazredUčenika.DataSource = DBeDnevnik.LoadClasses();
            cbIzaberiRazredUčenika.SelectedIndex = -1;
        }

        private void FillDataGridSubjects()
        {
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = DBeDnevnik.LoadSubjects();
            dataGridView3.Columns["zaključnaOcjena"].Visible = false;
            dataGridView3.Columns[0].HeaderText = "Naziv predmeta";
        }

        private void FillDataGridClasses()
        {
            dataGridView2.DataSource = null;
            classes = DBeDnevnik.LoadClassesBySchoolyear(cbTopŠkolskaGodina.SelectedItem.ToString());
            dataGridView2.DataSource = classes;
            dataGridView2.Columns["RazrednikID"].Visible = false;
            dataGridView2.Columns["ŠkolskaGodina"].Visible = false;
            dataGridView2.Columns["rasporedČasova"].HeaderText = "Raspored časova";
        }

        private void PopulateComboBoxTeachers(System.Windows.Forms.ComboBox cmb)
        {
            classes = DBeDnevnik.LoadClassesBySchoolyear(cbTopŠkolskaGodina.SelectedItem.ToString());
            cmb.Items.Clear();
            List<int> starješineID = new List<int>();
            foreach (SchoolClass schoolClass in classes)
                starješineID.Add(schoolClass.RazrednikID);
            foreach (User user in Korisnici)
            {
                if (user.accountType.Equals("nastavnik") && !starješineID.Contains(user.id))
                    cmb.Items.Add(user);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedUser = dataGridView1.SelectedRows[0].DataBoundItem as User;
            oldCellValue = null;
            if (selectedUser.accountType.Equals("administrator") || e.ColumnIndex == 6)
                return;
            if (e.ColumnIndex == 5 && !selectedUser.accountType.Equals("nastavnik"))
                return;
            dataGridView1.ReadOnly = false;
            dataGridView1.BeginEdit(true);
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                oldCellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Rectangle cellBounds = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            if (e.ColumnIndex == 5 && selectedUser.accountType.Equals("nastavnik"))
            {
                if (problem == null)
                {
                    problem = new System.Windows.Forms.ComboBox();
                    problem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    problem.Bounds = cellBounds;
                    PopulateComboBoxSubjects(problem);
                    dataGridView1.Controls.Add(problem);
                    // Show the ComboBox
                    problem.DroppedDown = true;
                    problem.DropDownClosed += (s, args) =>
                    {
                        // Update the cell value when the ComboBox is closed
                        if (problem.SelectedIndex != -1)
                        {
                            problem.DroppedDown = false;
                            dataGridView1.Controls.Remove(problem);
                            if (string.IsNullOrEmpty(oldCellValue))
                                cell.Value = problem.SelectedItem;
                            else
                                cell.Value = oldCellValue + ", " + problem.SelectedItem;
                            dataGridView1.ReadOnly = true;
                        }
                    };
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.BeginEdit(false);
            if (problem != null)
            {
                problem.DroppedDown = false;
                dataGridView1.Controls.Remove(problem);
                problem = null;
            }

            if (!DBeDnevnik.updateUserByID(selectedUser.id, selectedUser.name, selectedUser.accountType, selectedUser.education))
            {
                switch (e.ColumnIndex)
                {
                    case 1:
                        selectedUser.changeName(oldCellValue); dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = oldCellValue;
                        break;
                    case 4:
                        selectedUser.changeAccountType(oldCellValue); dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = oldCellValue;
                        break;
                    case 5:
                        selectedUser.changeEducation(oldCellValue); dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = oldCellValue;
                        break;
                }
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var result = new DeleteForm().ShowDialog();
                if (result == DialogResult.Yes)
                {
                    User selectedUser = dataGridView1.SelectedRows[0].DataBoundItem as User;
                    if (selectedUser.accountType.Equals("administrator"))
                    {
                        MessageBox.Show("Ne možete obrisati nalog administratora!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (DBeDnevnik.DeleteUserByID(selectedUser.id) == 1)
                    {
                        FillDataGridUsers();
                        LoadChildWhitoutParents();
                        MessageBox.Show("Obrisali ste nalog");
                    }
                    else
                        MessageBox.Show("Nalog nije obrisan!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFirstName.Text) || string.IsNullOrEmpty(tbLastName.Text) ||
                string.IsNullOrEmpty(tbUserName.Text) || string.IsNullOrEmpty(tbPassword.Text) || string.IsNullOrEmpty(cbSelectSubject.Text))
            {
                MessageBox.Show("Niste unijeli potpune podatke!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (mainForm.allUsernames.Contains(tbUserName.Text))
            {
                MessageBox.Show("Uneseno korisničko ime je već zauzeto!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var korisnik = new Teacher()
            {
                name = tbFirstName.Text + " " + tbLastName.Text,
                username = tbUserName.Text,
                password = tbPassword.Text,
                accountType = "nastavnik",
            };
            korisnik.education = cbSelectSubject.Text;
            tbFirstName.Clear(); tbLastName.Clear(); tbUserName.Clear(); tbPassword.Clear(); cbSelectSubject.SelectedIndex = -1;

            if (DBeDnevnik.InsertUser(korisnik))
            {
                FillDataGridUsers();
                MessageBox.Show("Dodali ste novi nalog");
            }
            else
                MessageBox.Show("Novi nalog nije dodan!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Administrator2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mainForm.Show();
        }

        private void btnAddNewClass_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbRazred.Text) || string.IsNullOrEmpty(cbOdjeljenje.Text) ||
                string.IsNullOrEmpty(cbŠkolska.Text))
            {
                MessageBox.Show("Niste unijeli potpune podatke!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newClass = new SchoolClass()
            {
                Razred = int.Parse(cbRazred.Text), 
                Odjeljenje = cbOdjeljenje.Text,
                ŠkolskaGodina = cbŠkolska.Text,
            };
            if (cbChooseRazrednik.SelectedItem != null)
            {
                newClass.Razrednik = (Teacher)cbChooseRazrednik.SelectedItem;
                newClass.RazrednikID = newClass.Razrednik.id;
            }

            if (DBeDnevnik.InsertNewClass(newClass))
            {
                FillDataGridClasses();
                MessageBox.Show("Dodali ste novi razred");
                cbRazred.ResetText(); cbOdjeljenje.ResetText(); cbŠkolska.ResetText(); cbChooseRazrednik.SelectedIndex = -1;
            }
            else
                MessageBox.Show("Dodavanje novog razreda nije uspjelo", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                selectedClass = dataGridView2.SelectedRows[0].DataBoundItem as SchoolClass;
                new RasporedČasova(selectedClass).ShowDialog();
                dataGridView2.EndEdit();
                return;
            }

            selectedClass = dataGridView2.SelectedRows[0].DataBoundItem as SchoolClass;
            oldClass = new SchoolClass()
            {
                Razred = selectedClass.Razred,
                Odjeljenje = selectedClass.Odjeljenje,
                ŠkolskaGodina = selectedClass.ŠkolskaGodina,
            };
            if (oldClass.Razrednik != null)
            {
                oldClass.Razrednik = selectedClass.Razrednik;
                oldClass.RazrednikID = selectedClass.Razrednik.id;
            }

            dataGridView2.ReadOnly = false;
            dataGridView2.BeginEdit(true);

            DataGridViewCell cell = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Rectangle cellBounds = dataGridView2.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            if (problem == null)
            {
                problem = new System.Windows.Forms.ComboBox();
                problem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                problem.Bounds = cellBounds;
                problem.Items.Clear();
                if (e.ColumnIndex == 1)
                {
                    List<string> list = new List<string>() { "a", "b", "c", "d" };
                    list.ForEach(l => problem.Items.Add(l));
                }
                else if (e.ColumnIndex == 0)
                {
                    List<int> list = new List<int>() { 1, 2, 3, 4 };
                    list.ForEach(l => problem.Items.Add(l));
                }
                else if (e.ColumnIndex == 4)
                {
                    PopulateComboBoxTeachers(problem);
                }
                dataGridView2.Controls.Add(problem);
                // Show the ComboBox
                problem.DropDownStyle = ComboBoxStyle.DropDownList;
                problem.DroppedDown = true;
                problem.SelectedIndex = -1;
                problem.DropDownClosed += (s, args) =>
                {
                    // Update the cell value when the ComboBox is closed
                    if (problem.SelectedIndex != -1)
                    {
                        problem.DroppedDown = false;
                        dataGridView2.Controls.Remove(problem);
                        cell.Value = problem.SelectedItem;
                        dataGridView2.ReadOnly = true;
                    }
                };
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.ReadOnly = true;
            dataGridView2.BeginEdit(false);
            if (problem != null)
            {
                problem.DroppedDown = false;
                dataGridView2.Controls.Remove(problem);
                problem = null;
            }

            switch (e.ColumnIndex)
            {
                case 4: DBeDnevnik.updateClass(selectedClass);  //jedino kad mijenjam SAMO razrednika radi se o update; i u slučaju da smo izabrali istog razrednika, izvršće apdejt
                        PopulateComboBoxTeachers(cbChooseRazrednik); // ali neće duplirati razred; ne treba mi if
                        break;
                default: foreach (SchoolClass schoolClass in classes)
                             if (schoolClass.Equals(selectedClass))
                             {
                                dataGridView2.Rows[e.RowIndex].Cells[0].Value = oldClass.Razred;
                                dataGridView2.Rows[e.RowIndex].Cells[1].Value = oldClass.Odjeljenje;
                                dataGridView2.Rows[e.RowIndex].Cells[2].Value = oldClass.ŠkolskaGodina;
                                return;
                              }
                         DBeDnevnik.DeleteClass(oldClass);
                         DBeDnevnik.InsertNewClass(selectedClass);
                         classes = DBeDnevnik.LoadClassesBySchoolyear(selectedClass.ŠkolskaGodina);
                         break;

            }
        }

        private void dataGridView2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var result = new DeleteForm().ShowDialog();
                if (result == DialogResult.Yes)
                {
                    SchoolClass selectedClass = dataGridView2.SelectedRows[0].DataBoundItem as SchoolClass;
                    if (DBeDnevnik.DeleteClass(selectedClass))
                    {
                        FillDataGridClasses();
                        MessageBox.Show("Obrisali ste razred");
                    }
                    else
                        MessageBox.Show("Razred nije obrisan!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDodajPredmet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbNazivPredmeta.Text))
            {
                MessageBox.Show("Niste unijeli potpune podatke!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Subject newSubject = new Subject();
            newSubject.subjectName = tbNazivPredmeta.Text;
            if (!string.IsNullOrEmpty(cbIzaberiKabinet.Text))
                newSubject.Kabinet = cbIzaberiKabinet.Text;    

            if (DBeDnevnik.InsertSubject(newSubject))
            {
                FillDataGridSubjects();
                MessageBox.Show("Dodali ste novi predmet");
            }
            tbNazivPredmeta.Clear(); cbIzaberiKabinet.SelectedIndex = -1;
            PopulateComboBoxSubjects(cbSelectSubject);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabClasses"])
            {
                cbRazred.Items.Clear();
                foreach (int s in Program.CLASSES)
                    cbRazred.Items.Add(s);
                cbOdjeljenje.Items.Clear();
                foreach (String s in Program.DEPARTMENTS)
                    cbOdjeljenje.Items.Add(s);
                cbŠkolska.Items.Clear();
                foreach (String s in Program.SCHOOL_YEARS)
                    cbŠkolska.Items.Add(s);

                loadTabClasses(true);
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.ReadOnly = false;
            dataGridView3.BeginEdit(true);
            selectedSubject = dataGridView3.SelectedRows[0].DataBoundItem as Subject;
            oldSubject = new Subject()
            {
                subjectName = selectedSubject.subjectName,
                Kabinet = selectedSubject.Kabinet,
            };

            DataGridViewCell cell = dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Rectangle cellBounds = dataGridView3.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            if (e.ColumnIndex == 1)
            {
                if (problem == null)
                {
                    problem = new System.Windows.Forms.ComboBox();
                    problem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    problem.Bounds = cellBounds;
                    PopulateComboBoxCabinets(problem);
                    dataGridView3.Controls.Add(problem);
                    // Show the ComboBox
                    problem.DroppedDown = true;
                    problem.DropDownClosed += (s, args) =>
                    {
                        // Update the cell value when the ComboBox is closed
                        if (problem.SelectedIndex != -1)
                        {
                            problem.DroppedDown = false;
                            dataGridView3.Controls.Remove(problem);
                            selectedSubject.Kabinet = problem.SelectedItem.ToString();
                            dataGridView3.ReadOnly = true;
                        }
                    };
                }
            }
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.ReadOnly = true;
            dataGridView3.BeginEdit(false);
            if (problem != null)
            {
                problem.DroppedDown = false;
                dataGridView3.Controls.Remove(problem);
                problem = null;
            }

            if (string.IsNullOrEmpty(selectedSubject.subjectName))
            {
                dataGridView3.Rows[e.RowIndex].Cells[0].Value = oldSubject.subjectName;
                dataGridView3.Rows[e.RowIndex].Cells[1].Value = oldSubject.Kabinet;
                return;
            }

            if (oldSubject != null)
            {
                if (e.ColumnIndex == 0)
                {
                    if (DBeDnevnik.deleteSubject(oldSubject.subjectName))
                    {
                        if (!DBeDnevnik.InsertSubject(selectedSubject))
                        {
                            dataGridView3.Rows[e.RowIndex].Cells[0].Value = oldSubject.subjectName;
                            dataGridView3.Rows[e.RowIndex].Cells[1].Value = oldSubject.Kabinet;
                        }
                        PopulateComboBoxSubjects(cbSelectSubject);
                        FillDataGridUsers();
                    }
                }
                else
                    DBeDnevnik.UpdateSubjectCabinet(selectedSubject);
            }
        }

        private void dataGridView3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var result = new DeleteForm().ShowDialog();
                if (result == DialogResult.Yes)
                {
                    Subject subject = dataGridView3.SelectedRows[0].DataBoundItem as Subject;
                    if (DBeDnevnik.deleteSubject(subject.subjectName))
                    {
                        FillDataGridSubjects();
                        FillDataGridUsers();
                        PopulateComboBoxSubjects(cbSelectSubject);
                        MessageBox.Show("Obrisali ste predmet");
                    }
                }
            }
        }

        private void btnDodajučenika_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbImeUčenika.Text) || string.IsNullOrEmpty(tbPrezimeUčenika.Text) || string.IsNullOrEmpty(cbIzaberiRazredUčenika.Text))
            {
                MessageBox.Show("Niste unijeli potpune podatke!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Student student = new Student()
            {
                studentName = tbImeUčenika.Text.Trim()+" "+tbPrezimeUčenika.Text.Trim(),
                studentClass = cbIzaberiRazredUčenika.SelectedItem as SchoolClass,
            };

            tbImeUčenika.Clear(); tbPrezimeUčenika.Clear(); cbIzaberiRazredUčenika.SelectedIndex = -1;

            if (DBeDnevnik.InsertStudent(student))
            {
                FillDataGridStudents();
                MessageBox.Show("Dodali ste novog učenika");
            }
        }

        private void btnDodajRoditelja_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbImeRoditelja.Text) || string.IsNullOrEmpty(tbPrezimeRoditelja.Text) ||
               string.IsNullOrEmpty(tbKorisničkoRoditelja.Text) || string.IsNullOrEmpty(tbLozinkaRoditelja.Text) 
               || dgvIzaberiDijete.SelectedRows.Count == 0)
            {
                MessageBox.Show("Niste unijeli potpune podatke!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (mainForm.allUsernames.Contains(tbKorisničkoRoditelja.Text))
            {
                MessageBox.Show("Uneseno korisničko ime je već zauzeto!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var korisnik = new Parent()
            {
                name = tbImeRoditelja.Text + " " + tbPrezimeRoditelja.Text,
                username = tbKorisničkoRoditelja.Text,
                password = tbLozinkaRoditelja.Text,
                accountType = "roditelj",
            };
            korisnik.childrens = new List<Student>(); 
            foreach (DataGridViewRow row in dgvIzaberiDijete.SelectedRows)
            {
                korisnik.childrens.Add(row.DataBoundItem as Student);
            }
            tbImeRoditelja.Clear(); tbPrezimeRoditelja.Clear(); tbKorisničkoRoditelja.Clear(); tbLozinkaRoditelja.Clear(); dgvIzaberiDijete.ClearSelection();

            if (DBeDnevnik.InsertUser(korisnik))
            {
                FillDataGridUsers();
                LoadChildWhitoutParents();
                MessageBox.Show("Dodali ste novi nalog");
            }
            else
                MessageBox.Show("Novi nalog nije dodan!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnDodajKabinet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbNoviKabinet.Text))
            {
                MessageBox.Show("Niste unijeli potpune podatke!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DBeDnevnik.InsertCabinet(int.Parse(tbNoviKabinet.Text)))
            {
                PopulateComboBoxCabinets(cbIzaberiKabinet);
                MessageBox.Show("Dodali ste novi kabinet");
            }
            tbNoviKabinet.Clear(); 
        }

        private void dgvIzaberiDijete_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvIzaberiDijete.ClearSelection();
        }

        private void dgvSpisakUčenika_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedStudent = dgvSpisakUčenika.SelectedRows[0].DataBoundItem as Student;
            oldStudent = new Student()
            {
                studentId = selectedStudent.studentId,
                studentName = selectedStudent.studentName,
            };
            if (selectedStudent.studentClass != null)
            {
                oldStudent.studentClass = new SchoolClass()
                {
                    Razred = selectedStudent.studentClass.Razred,
                    Odjeljenje = selectedStudent.studentClass.Odjeljenje,
                    ŠkolskaGodina = selectedStudent.studentClass.ŠkolskaGodina
                };
            }
            if (e.ColumnIndex == 5)
            {
                new OpštiUspjehForm(selectedStudent).ShowDialog();
                return;
            }

            dgvSpisakUčenika.ReadOnly = false;
            dgvSpisakUčenika.BeginEdit(true);
            if (e.ColumnIndex != 1)
            {
                DataGridViewCell cell = dgvSpisakUčenika.Rows[e.RowIndex].Cells[e.ColumnIndex];
                Rectangle cellBounds = dgvSpisakUčenika.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                if (comboboxRazredi == null)
                {
                    comboboxRazredi = new System.Windows.Forms.ComboBox();
                    comboboxRazredi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    comboboxRazredi.Bounds = cellBounds;
                    List<SchoolClass> list = DBeDnevnik.LoadClasses();
                    list.ForEach(l => comboboxRazredi.Items.Add(l));
                    dgvSpisakUčenika.Controls.Add(comboboxRazredi);
                    // Show the ComboBox
                    comboboxRazredi.DroppedDown = true;
                    comboboxRazredi.DropDownClosed += (s, args) =>
                    {
                        // Update the cell value when the ComboBox is closed
                        if (comboboxRazredi.SelectedIndex != -1)
                        {
                            comboboxRazredi.DroppedDown = false;
                            dgvSpisakUčenika.Controls.Remove(comboboxRazredi);
                            cell.Value = comboboxRazredi.SelectedItem;
                            dgvSpisakUčenika.ReadOnly = true;
                        }
                    };
                }
            }
        }

        private void dgvSpisakUčenika_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvSpisakUčenika.ReadOnly = true;
            dgvSpisakUčenika.BeginEdit(false);
            if (comboboxRazredi != null)
            {
                comboboxRazredi.DroppedDown = false;
                dgvSpisakUčenika.Controls.Remove(problem);
                comboboxRazredi.Dispose();
                comboboxRazredi = null;
            }

            if (string.IsNullOrEmpty(selectedStudent.studentName))
            {
                dgvSpisakUčenika.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = oldStudent;
                return;
            }

            switch (e.ColumnIndex)
            {
                case 1:
                    DBeDnevnik.updateStudent(selectedStudent);
                    break;
                default:

                    if (DBeDnevnik.DeleteStudent(oldStudent))
                    {
                        DBeDnevnik.InsertStudent(selectedStudent);
                    }
                    break;
            }
        }

        private void loadTabClasses(bool first)
        {
           if (first)
            {
                cbTopŠkolskaGodina.Items.Clear();
                foreach (String s in Program.SCHOOL_YEARS)
                    cbTopŠkolskaGodina.Items.Add(s);
                cbTopŠkolskaGodina.SelectedIndex = Program.SCHOOL_YEARS.Count - 1;
            }
            FillDataGridClasses();
            PopulateComboBoxTeachers(cbChooseRazrednik);
        }

    }
}
