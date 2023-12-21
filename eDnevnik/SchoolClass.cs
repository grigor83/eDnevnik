using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EvidencijaIzostanaka
{
    public class SchoolClass
    {
        public int Razred { get; set; }
        public string Odjeljenje { get; set; }
        public string ŠkolskaGodina { get; set; }  
        public int RazrednikID { get; set; }
        public Teacher Razrednik { get; set; }
        public string rasporedČasova { get; set; }

        public override bool Equals(Object o)
        {
            if (o == null || GetType() != o.GetType()) 
            { return false; }
            SchoolClass other = o as SchoolClass;
            if (this.Razred == other.Razred && this.Odjeljenje.Equals(other.Odjeljenje) && this.ŠkolskaGodina.Equals(other.ŠkolskaGodina))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Razred.GetHashCode() ^
                Odjeljenje.GetHashCode() ^
                ŠkolskaGodina.GetHashCode();
        }

        public override string ToString()
        {
            return Razred+" "+Odjeljenje + " "+ŠkolskaGodina+" "+ Razrednik;
        }
    }
}
