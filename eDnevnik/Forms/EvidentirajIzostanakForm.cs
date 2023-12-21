using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EvidencijaIzostanaka
{
    public partial class EvidentirajIzostanakForm : Form
    {
        private Lesson lesson;
        private Student selectedStudent;
        private string oldOcjena;
        private ComboBox cmbOcjena;

        public EvidentirajIzostanakForm()
        {
            InitializeComponent();
        }

        public EvidentirajIzostanakForm(Lesson lesson)
        {            
            InitializeComponent();
            dataGridView1.DataSource = lesson.students;
            dataGridView1.Columns["studentID"].Visible = false;
            dataGridView1.Columns["studentClass"].Visible = false;
            dataGridView1.Columns["OpštiUspjeh"].Visible = false;
            dataGridView1.Columns["Vladanje"].Visible = false;
            dataGridView1.Columns["studentName"].HeaderText = "Ime i prezime učenika";
            lbTop.Text = "Evidencija prisustva";
            lbSubjectName.Text = "Predmet: " + lesson.subjectName;
            lbLesson.Text = lesson.date.ToShortDateString() + " " + lesson.redniBroj + ". čas";
            this.lesson = lesson;
            this.Text = Program.APP_NAME;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedStudent = dataGridView1.SelectedRows[0].DataBoundItem as Student;
            if (e.ColumnIndex == 3)
            {
                if (selectedStudent.Prisustvo.Equals("prisutan"))
                {
                    if (DBeDnevnik.insertAbsentStudent(selectedStudent.studentId, lesson.date, lesson.redniBroj))
                        selectedStudent.Prisustvo = "odsutan";
                }
                else
                {
                    if (DBeDnevnik.deleteAbsentStudent(selectedStudent.studentId, lesson.date, lesson.redniBroj))
                        selectedStudent.Prisustvo = "prisutan";
                }
            }
            else if (e.ColumnIndex == 4)
            {
                oldOcjena = selectedStudent.Ocjena;
                dataGridView1.ReadOnly = false;
                dataGridView1.BeginEdit(true);

                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                Rectangle cellBounds = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                if (cmbOcjena == null)
                {
                    cmbOcjena = new System.Windows.Forms.ComboBox();
                    cmbOcjena.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    cmbOcjena.Bounds = cellBounds;
                    PopulateComboBoxGrades(cmbOcjena);
                    dataGridView1.Controls.Add(cmbOcjena);
                    cmbOcjena.DroppedDown = true;
                    cmbOcjena.DropDownClosed += (s, args) =>
                    {
                        if (cmbOcjena.SelectedIndex != -1)
                        {
                            cmbOcjena.DroppedDown = false;
                            dataGridView1.Controls.Remove(cmbOcjena);
                            selectedStudent.Ocjena = cmbOcjena.SelectedItem.ToString();
                            dataGridView1.ReadOnly = true;
                        }
                    };
                }
            }
        }

        private void PopulateComboBoxGrades(ComboBox cmbOcjena)
        {
            Program.GRADES.ForEach(g => { cmbOcjena.Items.Add(g); });   
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.BeginEdit(false);
            if (cmbOcjena != null)
            {
                cmbOcjena.DroppedDown = false;
                dataGridView1.Controls.Remove(cmbOcjena);
                cmbOcjena = null;
            }
            if (selectedStudent.Prisustvo.Equals("odsutan"))
            {
                MessageBox.Show("Ne možete ocijeniti učenika koji nije prisutan na času!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                selectedStudent.Ocjena = oldOcjena;
                return;
            }

            if (!DBeDnevnik.InsertGrade(selectedStudent, lesson))
                if (!DBeDnevnik.UpdateGrade(selectedStudent, lesson))
                    selectedStudent.Ocjena = oldOcjena;
            oldOcjena = null;
        }
    }
}
