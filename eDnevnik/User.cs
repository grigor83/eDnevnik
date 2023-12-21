using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaIzostanaka
{
    public abstract class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string accountType { get; set; }
        public string education { get; set; }
        public string children { get; set; }
        public List<Student> childrens { get; set; }

        public User() { }

        protected User(int id, string name, string userName, string password, string account)
        {
            this.id = id;
            this.name = name;
            this.username = userName;
            this.password = password;
            this.accountType = account;
        }

        public override string ToString()
        {
            return id + " " + name + " " + username + " " + password + " " + accountType +" "+education;
        }

        public void changeName(string newName)
        {
            name = newName;
        }

        public void changeAccountType(string newAccount)
        {
            accountType = newAccount;
        }

        internal void changeEducation(string v)
        {
            education = v;
        }
    }
}
