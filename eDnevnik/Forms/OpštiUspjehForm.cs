using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvidencijaIzostanaka.Forms
{
    public partial class OpštiUspjehForm : Form
    {
        private Student student;
        public OpštiUspjehForm(Student selectedStudent)
        {
            InitializeComponent();
            this.Text = Program.APP_NAME;
            this.student = selectedStudent;
            lbUčenik.Text = "Učenik " + student.studentName;
            lbŠkola.Text = "Škola: Sveti Sava";
            PopulateDGV();
        }

        private void PopulateDGV()
        {
            dataGridView1.DataSource = DBeDnevnik.LoadŠkolskiUspjeh(student);
            dataGridView1.Columns["studentID"].Visible = false;
            dataGridView1.Columns["studentName"].Visible = false;
            dataGridView1.Columns["prisustvo"].Visible = false;
            dataGridView1.Columns["Ocjena"].Visible = false;
            dataGridView1.Columns["studentClass"].HeaderText = "Razred i odjeljenje";
            dataGridView1.Columns["OpštiUspjeh"].HeaderText = "Opšti uspjeh";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Student selectedStudent = dataGridView1.SelectedRows[0].DataBoundItem as Student;
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = DBeDnevnik.LoadFinalGradesByYear(selectedStudent);
                dataGridView2.Columns["Kabinet"].Visible = false;
                dataGridView2.Columns["subjectName"].HeaderText = "Naziv predmeta";
                dataGridView2.Columns["zaključnaOcjena"].HeaderText = "Zaključna ocjena";
            }
        }
    }
}
