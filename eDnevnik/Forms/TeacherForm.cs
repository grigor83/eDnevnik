using Google.Protobuf;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvidencijaIzostanaka
{
    public partial class TeacherForm : Form
    {
        private MainForm mainForm;
        private Teacher teacher;

        public TeacherForm(MainForm mainForm, Teacher teacher)
        {
            this.mainForm = mainForm;
            this.teacher = teacher;
            this.teacher.Class = DBeDnevnik.selectTeachersClass(this.teacher.id);
            InitializeComponent();
            this.Text = Program.APP_NAME;
            this.lbTeacherName.Text = "Nastavnik: "+teacher.name;
            lbNaziv.Text = "Učenici razreda: " + teacher.Class;
            cbWeek.DataSource = getWeeks();
            cbWeek.DropDownClosed += (s, args) =>
            {
                // Update the cell value when the ComboBox is closed
                if (cbWeek.SelectedIndex != -1)
                {
                    showSchedule(cbWeek.SelectedItem.ToString());
                }
            };
            showSchedule(cbWeek.SelectedItem.ToString());
            showStudents();
        }

        private void showSchedule(String week)
        {
            foreach (Control control in tabelaRaspored.Controls.Cast<Control>().ToArray())
            {
                for(int rowIndex =1; rowIndex <tabelaRaspored.RowCount;rowIndex++)
                    if (tabelaRaspored.GetRow(control) == rowIndex)
                        {
                            tabelaRaspored.Controls.Remove(control);
                            control.Dispose();
                        }
            }

            teacher.getSchedule(week);
            teacher.schedule.ForEach(lesson =>
            {
               tabelaRaspored.Controls.Add(lesson, (int)lesson.date.DayOfWeek - 1, lesson.redniBroj);
                lesson.form = this;
            });

        }
        public void showStudents()
        {
            teacher.getStudents();

            if (teacher.students == null)
            {
                AddCustomRow(tabelaImenikUčenika, null);
                return;
            }

            foreach (Student s in teacher.students)
                AddCustomRow(tabelaImenikUčenika, s);
        }

        public void updateIzostanci(Student student, int rowIndex)
        {
            //List<LessonSimple> izostanci = DBeDnevnik.LoadAbsencesByStudentID(student.studentId);
            student.izostanci = null;
            student.izostanci = DBeDnevnik.LoadAbsencesByStudentID(student.studentId);
            Label control = tabelaImenikUčenika.GetControlFromPosition(2, rowIndex) as Label;
            int opravdani = 0;
            student.izostanci.ForEach(iz =>
            {
                if (!string.IsNullOrEmpty(iz.tip) && iz.tip.Equals("opravdan"))
                    opravdani++;
            });
            control.Text = "Opravdani: "+ opravdani.ToString();

            control = tabelaImenikUčenika.GetControlFromPosition(3, rowIndex) as Label;
            opravdani = 0;
            student.izostanci.ForEach(iz =>
            {
                if (!string.IsNullOrEmpty(iz.tip) && iz.tip.Equals("neopravdan"))
                    opravdani++;
            });
            control.Text = "Neopravdani: " + opravdani.ToString();

            CustomLabel control2 = tabelaImenikUčenika.GetControlFromPosition(4, rowIndex) as CustomLabel;
            opravdani = 0;
            student.izostanci.ForEach(iz =>
            {
                if (string.IsNullOrEmpty(iz.tip))
                    opravdani++;
            });
            control2.Text = "Nedefinisani: " + opravdani.ToString();
            control2.Student = student;
        }

        private void AddCustomRow(TableLayoutPanel tableLayoutPanel, Student student)
        {
            // Add a new row
            int rowIndex = tableLayoutPanel.RowCount++;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            if (student == null)
            {
                Label warning = new Label();
                warning.Dock = DockStyle.Fill;
                warning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                warning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                warning.Text = "Niste odjeljenjski starješina!";
                tableLayoutPanel.Controls.Add(warning, 1, rowIndex);
                return;
            }
            //List<LessonSimple> izostanci = DBeDnevnik.LoadAbsencesByStudentID(student.studentId);
            student.izostanci = null;
            student.izostanci = DBeDnevnik.LoadAbsencesByStudentID(student.studentId);

            PictureBox pictureBox = new PictureBox();
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, 50, 50);
            pictureBox.Region = new Region(path);
            pictureBox.Image = Properties.Resources.student;
            pictureBox.Size = new System.Drawing.Size(50, 50);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            tableLayoutPanel.Controls.Add(pictureBox, 0, 0);

            Label lbNameOfStudent = new Label();
            lbNameOfStudent.Dock = DockStyle.Fill;
            lbNameOfStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbNameOfStudent.Text = rowIndex + ". " + student.studentName;
            lbNameOfStudent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            Label lbOpravdani = new Label();
            lbOpravdani.Dock = DockStyle.Fill;
            lbOpravdani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbOpravdani.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            int opravdani=0;
            student.izostanci.ForEach(iz =>
            {
                if (!string.IsNullOrEmpty(iz.tip) && iz.tip.Equals("opravdan"))
                    opravdani++;
            });
            lbOpravdani.Text ="Opravdani: "+ opravdani.ToString();

            Label lbNeopravdani = new Label();
            lbNeopravdani.Dock = DockStyle.Fill;
            lbNeopravdani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbNeopravdani.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            int neopravdani = 0;
            student.izostanci.ForEach(iz =>
            {
                if (!string.IsNullOrEmpty(iz.tip) && iz.tip.Equals("neopravdan"))
                    neopravdani++;
            });
            lbNeopravdani.Text = "Neopravdani: " + neopravdani.ToString();

            CustomLabel lbNedefinisani = new CustomLabel();
            lbNedefinisani.Student = student;
            lbNedefinisani.Dock = DockStyle.Fill;
            lbNedefinisani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbNedefinisani.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbNedefinisani.Click += (s, args) => showIzostanke(student, student.izostanci, rowIndex);
            int nedefinisani = 0;
            student.izostanci.ForEach(iz =>
            {
                if (string.IsNullOrEmpty(iz.tip))
                    nedefinisani++;
            });
            lbNedefinisani.Text = "Nedefinisani: " + nedefinisani.ToString();

            // Add controls to the new row
            tableLayoutPanel.Controls.Add(pictureBox, 0, rowIndex);
            tableLayoutPanel.Controls.Add(lbNameOfStudent, 1, rowIndex);
            tableLayoutPanel.Controls.Add(lbOpravdani, 2, rowIndex);
            tableLayoutPanel.Controls.Add(lbNeopravdani, 3, rowIndex);
            tableLayoutPanel.Controls.Add(lbNedefinisani, 4, rowIndex);
        }

        // Event handler for the label Nedefinisani click event
        private void showIzostanke(Student student, List<LessonSimple> izostanci, int rowIndex)
        {
            new OpravdajIzostanakForm(this, student, izostanci, teacher.name, rowIndex).ShowDialog();
        }

        private List<string> getWeeks()
        {
            List<string> weeks = new List<string>();
            DateTime currentDate = DateTime.Now;
            for (int i = 0; i < 3; i++)
            {
                //DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + 1);
                //DateTime endOfWeek = startOfWeek.AddDays(6);
                //weeks.Add(startOfWeek.ToShortDateString() + "-" + endOfWeek.ToShortDateString());
                //currentDate = startOfWeek.AddDays(-(int)currentDate.DayOfWeek - 1);
                DateTime firstDayOfWeek = GetFirstDayOfWeek(currentDate);
                DateTime endOfWeek2 = firstDayOfWeek.AddDays(6);
                Console.WriteLine("First Day of Current Week: " + firstDayOfWeek+ " "+endOfWeek2);
                weeks.Add(firstDayOfWeek.ToShortDateString() + "-" + endOfWeek2.ToShortDateString());
                currentDate = firstDayOfWeek.AddDays(-(int)currentDate.DayOfWeek - 1);
            }
            return weeks;
        }

        static DateTime GetFirstDayOfWeek(DateTime currentDate)
        {
            // Calculate the difference between the current day of the week and the day you consider the start of the week
            int daysUntilStartOfWeek = (int)currentDate.DayOfWeek - (int)DayOfWeek.Monday;

            // Adjust the difference to include the current day
            if (daysUntilStartOfWeek < 0)
            {
                daysUntilStartOfWeek += 7;
            }

            // Subtract the adjusted difference to get the first day of the week
            DateTime firstDayOfWeek = currentDate.AddDays(-daysUntilStartOfWeek);

            return firstDayOfWeek;
        }

        private void TeacherForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mainForm.Show();
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabMain.TabPages["tabImenik"] && tabelaImenikUčenika.RowCount>1)
            {
                int i = 1;
                teacher.students.ForEach(student =>
                {
                    updateIzostanci(student, i++);
                });
            }
        }
    }
}
