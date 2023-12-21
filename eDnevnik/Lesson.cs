using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EvidencijaIzostanaka
{
    public class Lesson: Label
    {
        public DateTime date { get; set; }
        public int redniBroj { get; set; }
        public int razred { get; set; }    
        public string department { get; set; }
        public string schoolyear { get; set; }
        public string subjectName { get; set; }
        public List<Student> students { get; set; }
        public TeacherForm form { get; set; }

        public Lesson(int classID, string department, string schoolyear, string subjectName)
        {
            this.razred = classID;
            this.department = department;
            this.schoolyear = schoolyear;
            this.subjectName = subjectName;
            Dock = DockStyle.Fill;
            TextAlign = ContentAlignment.MiddleCenter;
            Font = new Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            MouseDown += Label_MouseDown;
        }

        public Lesson(DateTime dateTime, int rednibroj, int razred, string odjeljenje, string škg, string predmet): this (razred,odjeljenje,škg, predmet)
        {
            this.date = dateTime;
            this.redniBroj = rednibroj;
            this.Text = this.ToString();
        }

        private void Label_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 2)
                {
                    students = DBeDnevnik.selectStudentsByClassID(razred,department,schoolyear);
                    DBeDnevnik.selectAbsentStudentsByLessonID(students, date, redniBroj);
                    // Ovdje za svakog studenta učitaj ocjene iz ovog predmeta, ali samo za taj čas, dakle jednu ocjenu
                    DBeDnevnik.LoadGradesByDateAndSubjectName(students, this);
                    new EvidentirajIzostanakForm(this).ShowDialog();
                }
            }
        }

        public override string ToString()
        {
            //return date.ToShortDateString()+" "+redniBroj+".čas "+razred+" "+department+" "+ schoolyear+" "+subjectName;
            return subjectName + "\n(" + razred + " " + department + ")";
            //return razred+" "+department+" "+schoolyear+" "+subjectName;
        }
    }
}
