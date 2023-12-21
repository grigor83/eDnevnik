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
    public partial class OpravdajIzostanakForm : Form
    {
        private TeacherForm teacherForm;
        private Student student;
        private List<LessonSimple> izostanci;
        private LessonSimple selectedLesson;
        private int rowIndex;

        public OpravdajIzostanakForm(TeacherForm teacherForm, Student student, List<LessonSimple> izostanci, string teacherName, int rowIndex)
        {
            this.teacherForm = teacherForm;
            this.student = student;
            this.izostanci = izostanci;
            this.rowIndex = rowIndex;
            InitializeComponent();
            this.Text = Program.APP_NAME;
            this.label1.Text = "Definiši izostanke za učenika: " + student.studentName;
            lbTeacherName.Text = "Razrednik: " + teacherName;
            FillDataGridAbsences();
        }

        private void FillDataGridAbsences()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = izostanci;
            dataGridView1.Columns["subjectName"].Visible = false;
            dataGridView1.Columns["Nastavnik"].Visible = false;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["date"].HeaderText = "Datum";
            dataGridView1.Columns["redniBroj"].HeaderText = "Redni broj";
            dataGridView1.Columns["razlog"].HeaderText = "Razlog";
            dataGridView1.Columns["tip"].HeaderText = "Tip";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           if (e.ColumnIndex==4 || e.ColumnIndex == 5)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.BeginEdit(true);
                selectedLesson = dataGridView1.SelectedRows[0].DataBoundItem as LessonSimple;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.BeginEdit(false);

            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                if (DBeDnevnik.UpdateAbsence(selectedLesson))
                {
                    MessageBox.Show("Čas je definisan!","",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void OpravdajIzostanakForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            teacherForm.updateIzostanci(student, rowIndex);
        }

    }
}
