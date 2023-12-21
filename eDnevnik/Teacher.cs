using System;
using System.Collections.Generic;

namespace EvidencijaIzostanaka
{
    public class Teacher : User
    {
        public List<Lesson> schedule {  get; set; }
        public SchoolClass Class { get; set; }    // odjeljenje kojem je ovaj nastavnik razredni starješina
        public List<Student> students { get; set; } // učenici kojim je ovaj nastavnik razredni starješina

        public Teacher()
        {
            //Class = DBeDnevnik.selectTeachersClass(this.id);
        }

        public void getSchedule(String week)
        {
            DateTime startdate = DateTime.Parse(week.Split('-')[0]);
            DateTime enddate = DateTime.Parse(week.Split('-')[1]);
            schedule = DBeDnevnik.selectLessonForTeacher2(this.id, startdate, enddate);
        }

        public void getStudents()
        {
            if (students != null)
            {
                students = null;
            }
            Class = DBeDnevnik.selectTeachersClass(this.id);
            if (Class == null)
                return;
            students = DBeDnevnik.selectStudentsByClassID(Class.Razred, Class.Odjeljenje, Class.ŠkolskaGodina);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
