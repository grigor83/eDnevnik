using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaIzostanaka
{
    public class Student
    {
        public int studentId { get; set; }
        public string studentName { get; set; }
        public SchoolClass studentClass { get; set; }
        public string Prisustvo { get; set; }
        public List<LessonSimple> izostanci { get; set; }
        public string Ocjena { get; set; }
        public string OpštiUspjeh { get; set; }
        public string Vladanje { get; set; }

        public Student()
        {
            Prisustvo = "prisutan";
        }

        public override string ToString()
        {
            return studentName;
        }
    }
}
