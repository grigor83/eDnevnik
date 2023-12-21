using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaIzostanaka
{
    public class LessonSimple
    {
        public string subjectName { get; set; }
        public DateTime date { get; set; }
        public int redniBroj { get; set; }
        public string Nastavnik { get; set; }
        public string razlog { get; set; }
        public string tip { get; set; }
        public int id { get; set; }

        public override string ToString()
        {
            return date.ToShortDateString()+" "+redniBroj;
        }
    }

}
