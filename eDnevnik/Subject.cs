using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaIzostanaka
{
    public class Subject
    {
        public string subjectName {  get; set; }   
        public string Kabinet { get; set; }
        public int zaključnaOcjena { get; set; }

        public Subject() { }

        public override string ToString()
        {
            return subjectName;
        }
    }
}
