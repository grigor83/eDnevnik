using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using System.Runtime.Remoting.Messaging;
using MySql.Data.Types;
using System.Runtime.InteropServices.ComTypes;
using Org.BouncyCastle.Utilities;
using MySqlX.XDevAPI;
using System.Configuration;
using System.Security.Policy;

namespace EvidencijaIzostanaka
{
    internal static class DBeDnevnik
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySqleDnevnik"].ConnectionString;

        public static List<User> LoadAllUsers()
        {
            List<User> users = new List<User>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM korisnik";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(4).Equals("administrator"))
                    {
                        users.Add(new Administrator()
                        {
                            id = reader.GetInt32(0),
                            name = reader.GetString(1),
                            username = reader.GetString(2),
                            password = reader.GetString(3),
                            accountType = reader.GetString(4),
                        });
                    }
                    else if (reader.GetString(4).Equals("roditelj"))
                    {
                        users.Add(new Parent()
                        {
                            id = reader.GetInt32(0),
                            name = reader.GetString(1),
                            username = reader.GetString(2),
                            password = reader.GetString(3),
                            accountType = reader.GetString(4),
                        });
                    }
                    else
                    {
                        users.Add(new Teacher()
                        {
                            id = reader.GetInt32(0),
                            name = reader.GetString(1),
                            username = reader.GetString(2),
                            password = reader.GetString(3),
                            accountType = reader.GetString(4),
                        });
                            //new Teacher(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                    }
                }
                reader.Close();

                cmd.CommandText = "SELECT * FROM stručan";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string subject = reader.GetString(1);   
                    foreach (User user in users)
                        if (user.id == id)
                        {
                            if (user.education == null)
                                user.education = subject;
                            else
                                user.education += ", " + subject;
                            break;
                        }
                }
                reader.Close();

                cmd.CommandText = "select roditelj_učenik.UčenikID, RoditeljID, Ime from roditelj_učenik, učenik where roditelj_učenik.UčenikID = učenik.UčenikID";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(1);
                    string ime = reader.GetString(2);
                    foreach(User user in users)
                        if (user.id == id)
                        {
                            if (user.children == null)
                                user.children = ime;
                            else
                                user.children += ", " + ime;
                            break;
                        }
                }
                reader.Close();
                conn.Close();
                return users;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Učitavanje svih korisnika nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        internal static Boolean updateUserByID(int userID, string newName, string newAccountType, string education)
        {
            if (newAccountType.Equals("nastavnik") && education == null)
                return false;
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE korisnik SET Ime=@name, TipNaloga=@accountType WHERE KorisnikID=@userID";
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@accountType", newAccountType);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.ExecuteNonQuery();
                if (newAccountType.Equals("nastavnik"))
                {
                    cmd.CommandText = "DELETE FROM stručan WHERE NastavnikID=@userID";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO stručan(NastavnikID,NazivPredmeta) VALUES (@userID,@subject)";
                    foreach (string s in education.Split(','))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@userID", userID);
                        cmd.Parameters.AddWithValue("@subject", s.Trim());
                        cmd.ExecuteNonQuery();
                    }
                    cmd.CommandText = "SELECT * FROM predaje_sadrži WHERE PredavačID=" + userID;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    List<String> subjects = new List<string>();
                    while (reader.Read())
                        subjects.Add(reader.GetString(3));
                    reader.Close();
                    foreach (string subject in subjects)
                    {
                        if (!education.Split(',').ToList().Contains(subject.Trim()))
                        {
                            cmd.CommandText = "UPDATE predaje_sadrži SET PredavačID=null WHERE PredavačID=@userID AND NazivPredmeta=@subject";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@userID", userID);
                            cmd.Parameters.AddWithValue("@subject", subject.Trim());
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uneseni podaci nisu validni!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static int DeleteUserByID(int userId)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM korisnik WHERE KorisnikID=@NastavnikId";
            cmd.Parameters.AddWithValue("@NastavnikId", userId);
            int n = cmd.ExecuteNonQuery();
            conn.Close();
            return n;
        }

        public static Boolean InsertUser(User korisnik)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText =
                    @"INSERT INTO korisnik(Ime, KorisničkoIme, Lozinka, TipNaloga)
                VALUES (@FirstName, @KorisničkoIme, @Lozinka, @TipNaloga); SELECT LAST_INSERT_ID();";

                cmd.Parameters.AddWithValue("@FirstName", korisnik.name);
                cmd.Parameters.AddWithValue("@KorisničkoIme", korisnik.username);
                cmd.Parameters.AddWithValue("@Lozinka", korisnik.password);
                cmd.Parameters.AddWithValue("@TipNaloga", korisnik.accountType);
                int generatedID = Convert.ToInt32(cmd.ExecuteScalar());
                if (korisnik.accountType.Equals("nastavnik"))
                {
                    cmd.CommandText = "INSERT INTO stručan(NastavnikID,NazivPredmeta) VALUES (@userID,@education)";
                    cmd.Parameters.AddWithValue("@userID", generatedID);
                    cmd.Parameters.AddWithValue("@education", korisnik.education);
                    cmd.ExecuteNonQuery();
                }
                else if (korisnik.accountType.Equals("roditelj"))
                {
                    korisnik.childrens.ForEach(child =>
                    {
                        cmd.CommandText = "insert into roditelj_učenik values (@parentID,@childID)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@childID", child.studentId);
                        cmd.Parameters.AddWithValue("@parentID", generatedID);
                        cmd.ExecuteNonQuery();
                    });
                }
                conn.Close();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static List<Student> selectStudentsByClassID(int classID, string department, string schoolyear) 
        {
            List<int> ids = new List<int>();
            List<Student> students = new List<Student>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM pohađa WHERE Razred=@classID AND Odjeljenje=@department AND ŠkolskaGodina=@schoolyear";
                cmd.Parameters.AddWithValue("@classID", classID);
                cmd.Parameters.AddWithValue("@department", department);
                cmd.Parameters.AddWithValue("@schoolyear", schoolyear);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ids.Add(reader.GetInt32(0));
                }
                reader.Close();

                foreach (int id in ids) {
                    cmd.CommandText = "SELECT * FROM učenik WHERE UčenikID="+id;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    students.Add(new Student()
                    {
                        studentId = reader.GetInt32(0),
                        studentName = reader.GetString(1),
                    });
                    reader.Close();
                }
                conn.Close();
                return students;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Učitavanje učeničkih ID nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        internal static void selectAbsentStudentsByLessonID(List<Student> students, DateTime date, int lessonID)
        {
            List<int> absenceID = new List<int>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT UčenikID FROM izostao WHERE Datum=@date AND RedniBroj=@lessonID";
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@lessonID", lessonID);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    absenceID.Add(reader.GetInt32(0));
                }
                reader.Close();
                conn.Close();

                foreach (Student student in students)
                {
                    if (absenceID.Contains(student.studentId))
                        student.Prisustvo = "odsutan";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Učitavanje izostalih učenika nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static Boolean insertAbsentStudent(int studentId, DateTime date, int lessonID)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText ="INSERT INTO izostao(UčenikID, Datum, RedniBroj) VALUES (@studentId, @date, @lessonID)";
            cmd.Parameters.AddWithValue("@studentId", studentId);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@lessonID", lessonID);
            int n = cmd.ExecuteNonQuery();
            conn.Close();
            if (n == 1)
                return true;
            else return false;
        }

        internal static bool deleteAbsentStudent(int studentId, DateTime date, int lessonID)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM izostao WHERE UčenikID=@studentId AND Datum=@date AND RedniBroj=@lessonID";
            cmd.Parameters.AddWithValue("@studentId", studentId);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@lessonID", lessonID);
            int n = cmd.ExecuteNonQuery();
            conn.Close();
            if (n == 1)
                return true;
            else return false;
        }

        internal static SchoolClass selectTeachersClass(int teacherID)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                SchoolClass c = null;
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM razred WHERE RazrednikID=" + teacherID;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    c = new SchoolClass() { 
                        Razred = reader.GetInt32(0),
                        Odjeljenje = reader.GetString(1),
                        ŠkolskaGodina = reader.GetString(2),
                        };
                }
                reader.Close();
                conn.Close();
                return c;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Učitavanje razreda za odjeljenskog starješinu nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        internal static List<SchoolClass> LoadClassesBySchoolyear(string schoolYear)
        {
            List<SchoolClass> classes = new List<SchoolClass>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM razred WHERE ŠkolskaGodina LIKE '%" + schoolYear.Split('/')[1]+"'";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    classes.Add(new SchoolClass()
                    {
                        Razred = reader.GetInt32(0),
                        Odjeljenje = reader.GetString(1),
                        ŠkolskaGodina = reader.GetString(2),
                        RazrednikID = reader.IsDBNull(3) ? -1 : reader.GetInt32(3),
                        rasporedČasova = "pregled",
                });
                }
                reader.Close();
                foreach (SchoolClass cls in classes)
                {
                    if (cls.RazrednikID != -1)
                    {
                        cmd.CommandText = "SELECT * FROM korisnik WHERE KorisnikID=" + cls.RazrednikID;
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        cls.Razrednik = new Teacher()
                        {
                            id = reader.GetInt32(0),
                            name = reader.GetString(1),
                            username = reader.GetString(2),
                            password = reader.GetString(3),
                            accountType = reader.GetString(4),
                        };
                        reader.Close();
                    }
                }
                conn.Close();
                return classes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Učitavanje podataka o razredu nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        internal static bool InsertNewClass(SchoolClass newClass)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText =
                    @"INSERT INTO razred(Razred, Odjeljenje, ŠkolskaGodina, RazrednikID)
                VALUES (@razred, @odjeljenje, @škgodina, @razrednikID); SELECT LAST_INSERT_ID();";

                cmd.Parameters.AddWithValue("@razred", newClass.Razred);
                cmd.Parameters.AddWithValue("@odjeljenje", newClass.Odjeljenje);
                cmd.Parameters.AddWithValue("@škgodina", newClass.ŠkolskaGodina);
                if (newClass.Razrednik != null) 
                    cmd.Parameters.AddWithValue("@razrednikID", newClass.RazrednikID);
                else
                    cmd.Parameters.AddWithValue("@razrednikID", null);
                int generatedID = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kreiranje novog razreda nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static bool updateClass(SchoolClass selectedClass)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE razred SET Razred=@razred, Odjeljenje=@odjeljenje, ŠkolskaGodina=@škgodina, RazrednikID=@razrednikID" +
                    " WHERE Razred=@razred AND Odjeljenje=@odjeljenje AND ŠkolskaGodina=@škgodina";
                cmd.Parameters.AddWithValue("@razred", selectedClass.Razred);
                cmd.Parameters.AddWithValue("@odjeljenje", selectedClass.Odjeljenje);
                cmd.Parameters.AddWithValue("@škgodina", selectedClass.ŠkolskaGodina);
                if (selectedClass.Razrednik != null)
                    cmd.Parameters.AddWithValue("@razrednikID", selectedClass.Razrednik.id);
                else
                    cmd.Parameters.AddWithValue("@razrednikID", null);
                int n = cmd.ExecuteNonQuery();
                conn.Close();
                if (n == 1)
                    return true;                
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uneseni podaci nisu validni!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static bool DeleteClass(SchoolClass selectedClass)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM razred WHERE Razred=@razred AND Odjeljenje=@odjeljenje AND ŠkolskaGodina=@škgodina";
                cmd.Parameters.AddWithValue("@razred", selectedClass.Razred);
                cmd.Parameters.AddWithValue("@odjeljenje", selectedClass.Odjeljenje);
                cmd.Parameters.AddWithValue("@škgodina", selectedClass.ŠkolskaGodina); 
                int n = cmd.ExecuteNonQuery();                
                conn.Close();
                if (n == 1)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Razred nije obrisan!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static List<Lesson> selectLessonForTeacher2(int teacherID, DateTime startdate, DateTime enddate)
        {
            List<Lesson> list = new List<Lesson>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select Datum, RedniBroj,čas.Razred,čas.Odjeljenje,čas.ŠkolskaGodina,čas.NazivPredmeta from čas, predaje_sadrži" +
                    " where čas.Razred = predaje_sadrži.Razred and čas.Odjeljenje = predaje_sadrži.Odjeljenje" +
                    " and čas.ŠkolskaGodina = predaje_sadrži.ŠkolskaGodina " +
                    " and čas.NazivPredmeta = predaje_sadrži.NazivPredmeta" +
                    " and Datum BETWEEN @startdate AND @enddate" +
                    " and PredavačID=" + teacherID;
                cmd.Parameters.AddWithValue("@startdate", startdate);
                cmd.Parameters.AddWithValue("@enddate", enddate);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Lesson(
                        reader.GetDateTime(0), reader.GetInt32(1), reader.GetInt32(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                }
                reader.Close();
                conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nije uspjelo učitavanje časova!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        internal static bool InsertSubject(Subject subject)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO predmet(NazivPredmeta) VALUES (@subjectName);";
                cmd.Parameters.AddWithValue("@subjectName", subject.subjectName.Trim());
                cmd.ExecuteNonQuery();
                if (!string.IsNullOrEmpty(subject.Kabinet))
                {
                    cmd.CommandText = "INSERT INTO predmet_kabinet VALUES (@subjectName, @cabinet);";
                    cmd.Parameters.AddWithValue("@cabinet", int.Parse(subject.Kabinet));
                    cmd.ExecuteNonQuery();
                }
                
                conn.Close();
                return true;
            }
             catch (Exception ex)
            {
                MessageBox.Show("Kreiranje predmeta nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static List<Subject> LoadSubjects()
        {
            List<Subject> subjects = new List<Subject>();
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=e_dnevnik; uid=root; password=mojabaza;");
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select predmet.NazivPredmeta, BrojKabineta from predmet " +
                    "left join predmet_kabinet " +
                    "on predmet.NazivPredmeta = predmet_kabinet.NazivPredmeta";
                //cmd.CommandText = "SELECT * FROM predmet";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    subjects.Add(new Subject()
                    {
                        subjectName = reader.GetString(0),
                    });

                    if (!reader.IsDBNull(1))
                    {
                        subjects.Last().Kabinet = reader.GetInt32(1).ToString();
                    }
                }
                reader.Close();
                conn.Close();
                return subjects;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nije uspjelo učitavanje predmeta!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        internal static bool deleteSubject(string subjectName)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM predmet WHERE NazivPredmeta=@subjectName";
                cmd.Parameters.AddWithValue("@subjectName", subjectName);
                int n = cmd.ExecuteNonQuery();
                conn.Close();
                if (n == 1)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Predmet nije obrisan!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static void UpdateSubjectCabinet(Subject selectedSubject)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM predmet_kabinet WHERE NazivPredmeta=@subjectName";
                cmd.Parameters.AddWithValue("@subjectName", selectedSubject.subjectName);
                cmd.ExecuteNonQuery();

                if (!string.IsNullOrEmpty(selectedSubject.Kabinet))
                {
                    cmd.CommandText = "INSERT INTO predmet_kabinet VALUES (@subjectName, @kabinet);";
                    cmd.Parameters.AddWithValue("@kabinet", int.Parse(selectedSubject.Kabinet));
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Broj kabineta je ažuriran!", "Greška");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Broj kabineta nije ažuriran!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static bool InsertČas(SchoolClass schoolClass, string subjectName, User teacher, DateTime date, int redniBrojČasa)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO predaje_sadrži(Razred, Odjeljenje, ŠkolskaGodina, NazivPredmeta, PredavačID) VALUES " +
                    "(@razred, @odjeljenje, @škgod, @nazivPredmeta, @predavačID)";
                cmd.Parameters.AddWithValue("@razred", schoolClass.Razred);
                cmd.Parameters.AddWithValue("@odjeljenje", schoolClass.Odjeljenje);
                cmd.Parameters.AddWithValue("@škgod", schoolClass.ŠkolskaGodina);
                cmd.Parameters.AddWithValue("@nazivPredmeta", subjectName);
                cmd.Parameters.AddWithValue("@predavačID", teacher.id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex1)
            {

            }

            try 
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO čas(Datum, RedniBroj, Razred, Odjeljenje, ŠkolskaGodina, NazivPredmeta) VALUES " +
                   "(@date, @redniBroj, @razred, @odjeljenje, @škgod, @nazivPredmeta)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@redniBroj", redniBrojČasa);
                cmd.Parameters.AddWithValue("@razred", schoolClass.Razred);
                cmd.Parameters.AddWithValue("@odjeljenje", schoolClass.Odjeljenje);
                cmd.Parameters.AddWithValue("@škgod", schoolClass.ŠkolskaGodina);
                cmd.Parameters.AddWithValue("@nazivPredmeta", subjectName);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Dodjeljivanje predmeta razredu nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static List<Student> LoadStudentsBySchoolyear(string schoolyear)
        {
            List<Student> students = new List<Student>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                if (schoolyear.Equals("Svi učenici"))
                    cmd.CommandText = "select učenik.UčenikID, Ime, Razred, Odjeljenje, ŠkolskaGodina" +
                        " from učenik" +
                        " left join pohađa" +
                        " on učenik.učenikID = pohađa.UčenikID;";
                else cmd.CommandText = "select učenik.UčenikID, Ime, Razred, Odjeljenje, ŠkolskaGodina from učenik, pohađa" +
                    " where učenik.UčenikID = pohađa.UčenikID" +
                    " and ŠkolskaGodina LIKE '%" + schoolyear.Split('/')[1] + "'";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student()
                    {
                        studentId = reader.GetInt32(0),
                        studentName = reader.GetString(1),
                        OpštiUspjeh = "pregled",
                    });
                    if (!reader.IsDBNull(2))
                    {
                        students.Last().studentClass = new SchoolClass()
                        {
                            Razred = reader.GetInt32(2),
                            Odjeljenje = reader.GetString(3),
                            ŠkolskaGodina = reader.GetString(4),
                        };
                    }
                }
                reader.Close();
                conn.Close();
                return students;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Učitavanje učenika nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        internal static List<SchoolClass> LoadClasses()
        {
            List<SchoolClass> classes = new List<SchoolClass>();
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=e_dnevnik; uid=root; password=mojabaza;");
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM razred";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    classes.Add(new SchoolClass()
                    {
                        Razred = reader.GetInt32(0),
                        Odjeljenje = reader.GetString(1),
                        ŠkolskaGodina = reader.GetString(2),
                    });
                reader.Close();
                conn.Close();
                return classes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nije uspjelo učitavanje razreda!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        internal static bool InsertStudent(Student student)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText =
                    @"INSERT INTO učenik(Ime)
                VALUES (@FirstName); SELECT LAST_INSERT_ID();";
                cmd.Parameters.AddWithValue("@FirstName", student.studentName);
                int generatedID = Convert.ToInt32(cmd.ExecuteScalar());

                if (student.studentClass != null)
                {
                    cmd.CommandText = "INSERT INTO pohađa(UčenikID,Razred,Odjeljenje,ŠkolskaGodina) " +
                    "VALUES (@studentID,@razred,@odjeljenje,@škgod)";
                    cmd.Parameters.AddWithValue("@studentID", generatedID);
                    cmd.Parameters.AddWithValue("@razred", student.studentClass.Razred);
                    cmd.Parameters.AddWithValue("@odjeljenje", student.studentClass.Odjeljenje);
                    cmd.Parameters.AddWithValue("@škgod", student.studentClass.ŠkolskaGodina);
                    cmd.ExecuteNonQuery();
                }
                
                conn.Close();
                if (generatedID > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex) {
                MessageBox.Show("Unošenje podataka o učeniku u bazu nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; 
            }
        }

        internal static bool DeleteStudent(Student oldStudent)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM učenik WHERE UčenikID=@id";
                cmd.Parameters.AddWithValue("@id", oldStudent.studentId);
                int n = cmd.ExecuteNonQuery();
                conn.Close();
                if (n == 1)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Učenik nije obrisan!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static void updateStudent(Student selectedStudent)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE učenik SET Ime=@name WHERE UčenikID=@id";
                cmd.Parameters.AddWithValue("@name", selectedStudent.studentName);
                cmd.Parameters.AddWithValue("@id", selectedStudent.studentId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Promjena imena učenika nije uspjela!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static List<LessonSimple> LoadLessons(SchoolClass schoolClass)
        {
            List<LessonSimple> list = new List<LessonSimple>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select čas.Datum, čas.RedniBroj, čas.NazivPredmeta, Ime " +
                    " from čas " +
                    " left join predaje_sadrži" +
                    " on čas.Razred = predaje_sadrži.Razred AND čas.Odjeljenje = predaje_sadrži.Odjeljenje and čas.ŠkolskaGodina = predaje_sadrži.ŠkolskaGodina and čas.NazivPredmeta = predaje_sadrži.NazivPredmeta" +
                    " left join korisnik" +
                    " on predaje_sadrži.PredavačID = korisnik.KorisnikID" +
                    " where čas.Razred=@razred and čas.Odjeljenje=@odjeljenje and čas.ŠkolskaGodina=@schoolyear" +
                    " order by čas.Datum desc, čas.RedniBroj";
                cmd.Parameters.AddWithValue("@razred", schoolClass.Razred);
                cmd.Parameters.AddWithValue("@odjeljenje", schoolClass.Odjeljenje);
                cmd.Parameters.AddWithValue("@schoolyear", schoolClass.ŠkolskaGodina);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new LessonSimple()
                    {
                        date = reader.GetDateTime(0),
                        redniBroj = reader.GetInt32(1),
                        subjectName = reader.GetString(2),
                    });
                    if (!reader.IsDBNull(3))
                        list.Last().Nastavnik = reader.GetString(3);
                }
                reader.Close();
                conn.Close();

                list.ForEach(x => Console.WriteLine(x));
                return list;

        }
            catch (Exception ex)
            {
                MessageBox.Show("Nije uspjelo učitavanje časova!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        internal static bool DeleteČas(SchoolClass schoolClass, LessonSimple selectedLesson)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM čas WHERE Datum=@datum AND RedniBroj=@broj AND Razred=@razred AND Odjeljenje=@odjeljenje AND ŠkolskaGodina=@škgod";
                cmd.Parameters.AddWithValue("@datum", selectedLesson.date);
                cmd.Parameters.AddWithValue("@broj", selectedLesson.redniBroj);
                cmd.Parameters.AddWithValue("@razred", schoolClass.Razred);
                cmd.Parameters.AddWithValue("@odjeljenje", schoolClass.Odjeljenje);
                cmd.Parameters.AddWithValue("@škgod", schoolClass.ŠkolskaGodina);cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Čas nije obrisan!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static List<LessonSimple> LoadAbsencesByStudentID(int studentId)
        {
            List<LessonSimple> lessons = new List<LessonSimple>();
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=e_dnevnik; uid=root; password=mojabaza;");
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM izostao where UčenikID=@studentId";
                cmd.Parameters.AddWithValue("studentId", studentId);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lessons.Add(new LessonSimple()
                    {
                        id = reader.GetInt32(0),
                        date = reader.GetDateTime(1),
                        redniBroj = reader.GetInt32(2)
                    });
                    if (!reader.IsDBNull(3))
                        lessons.Last().razlog = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                        lessons.Last().tip = reader.GetString(4);
                }
                conn.Close();
                return lessons;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nije uspjelo učitavanje izostanaka za pojedinačnog učenika!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return null;
            }
        }

        internal static bool UpdateAbsence(LessonSimple selectedLesson)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE izostao SET Razlog=@razlog, Tip=@tip" +
                    " WHERE UčenikID=@id AND Datum=@datum AND RedniBroj=@broj";
                cmd.Parameters.AddWithValue("@id", selectedLesson.id);
                cmd.Parameters.AddWithValue("@datum", selectedLesson.date);
                cmd.Parameters.AddWithValue("@broj", selectedLesson.redniBroj);
                if (selectedLesson.razlog != null)
                    cmd.Parameters.AddWithValue("@razlog", selectedLesson.razlog);
                else
                    cmd.Parameters.AddWithValue("@razlog", null);
                if (selectedLesson.tip != null)
                    cmd.Parameters.AddWithValue("@tip", selectedLesson.tip);
                else
                    cmd.Parameters.AddWithValue("@tip", null);

                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Izostanak nije definisan!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static List<Student> LoadStudentsWhithoutParents()
        {
            List<Student> students = new List<Student>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from učenik where UčenikID not in (select UčenikID from roditelj_učenik)";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student()
                    {
                        studentId = reader.GetInt32(0),
                        studentName = reader.GetString(1),
                    });
                }
                reader.Close();
                conn.Close();
                return students;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Učitavanje djece nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        internal static bool InsertCabinet(int brojKabineta)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=e_dnevnik; uid=root; password=mojabaza;");
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO kabinet VALUES(@brojKabineta)";
                cmd.Parameters.AddWithValue("@brojKabineta", brojKabineta);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nije uspjelo kreiranje kabineta!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static bool InsertGrade(Student selectedStudent, Lesson lesson)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=e_dnevnik; uid=root; password=mojabaza;");
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO ocjena VALUES(@učenikID, @razred, @odjeljenje, @škgod, @nazivPredmeta, @datum, @ocjena)";
                cmd.Parameters.AddWithValue("@učenikID", selectedStudent.studentId);
                cmd.Parameters.AddWithValue("@razred", lesson.razred);
                cmd.Parameters.AddWithValue("@odjeljenje", lesson.department);
                cmd.Parameters.AddWithValue("@škgod", lesson.schoolyear);
                cmd.Parameters.AddWithValue("@nazivPredmeta", lesson.subjectName);
                cmd.Parameters.AddWithValue("@datum", lesson.date);
                cmd.Parameters.AddWithValue("@ocjena", selectedStudent.Ocjena);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Nije uspjelo unošenje ocjene!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static void LoadGradesByDateAndSubjectName(List<Student> students, Lesson lesson)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=e_dnevnik; uid=root; password=mojabaza;");
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT UčenikID, Ocjena from ocjena WHERE Razred=@razred AND Odjeljenje=@odjeljenje AND ŠkolskaGodina=@škgod " +
                    "AND Datum=@datum AND NazivPredmeta=@nazivPredmeta";
                cmd.Parameters.AddWithValue("@razred", lesson.razred);
                cmd.Parameters.AddWithValue("@odjeljenje", lesson.department);
                cmd.Parameters.AddWithValue("@škgod", lesson.schoolyear);
                cmd.Parameters.AddWithValue("@datum", lesson.date);
                cmd.Parameters.AddWithValue("@nazivPredmeta", lesson.subjectName);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    students.ForEach(S =>
                    {
                        if (S.studentId == id)
                        {
                            S.Ocjena = reader.GetInt32(1).ToString();
                        }
                    });
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nije uspjelo učitavanje ocjene!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static bool UpdateGrade(Student selectedStudent, Lesson lesson)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                if (selectedStudent.Ocjena != null) 
                {
                    cmd.CommandText = "UPDATE ocjena SET Ocjena=@ocjena" +
                                        " WHERE UčenikID=@id AND Razred=@razred AND Odjeljenje=@odjeljenje AND ŠkolskaGodina=@škgod AND NazivPredmeta=@nazivPredmeta AND Datum=@datum";
                    cmd.Parameters.AddWithValue("@ocjena", selectedStudent.Ocjena);
                }
                else
                {
                    cmd.CommandText = "DELETE FROM ocjena WHERE UčenikID=@id AND Razred=@razred AND Odjeljenje=@odjeljenje AND ŠkolskaGodina=@škgod AND NazivPredmeta=@nazivPredmeta AND " +
                        "Datum=@datum";
                }
                cmd.Parameters.AddWithValue("@id", selectedStudent.studentId);
                cmd.Parameters.AddWithValue("@razred", lesson.razred);
                cmd.Parameters.AddWithValue("@odjeljenje", lesson.department);
                cmd.Parameters.AddWithValue("@škgod", lesson.schoolyear);
                cmd.Parameters.AddWithValue("@nazivPredmeta", lesson.subjectName);
                cmd.Parameters.AddWithValue("@datum", lesson.date);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unošenje/izmjena ocjene nije uspjela!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static object LoadŠkolskiUspjeh(Student student)
        {
            List<Student> students = new List<Student>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from završio where UčenikID="+student.studentId;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student()
                    {
                        studentId = reader.GetInt32(0),
                        OpštiUspjeh = reader.GetInt32(4).ToString(),
                        Vladanje = reader.GetString(5),
                        studentClass = new SchoolClass()
                        {
                            Razred = reader.GetInt32(1),
                            Odjeljenje = reader.GetString(2),
                            ŠkolskaGodina = reader.GetString(3),
                        }
                    });
                }
                reader.Close();
                conn.Close();
                return students;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Učitavanje djece nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        internal static List<Subject> LoadFinalGradesByYear(Student student)
        {
            List<Subject> subjects = new List<Subject>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT NazivPredmeta, ZaključnaOcjena from predmet_zaključnaocjena WHERE UčenikID=@id AND Razred=@razred AND Odjeljenje=@odjeljenje " +
                    "AND ŠkolskaGodina=@škgod";
                cmd.Parameters.AddWithValue("@id", student.studentId);
                cmd.Parameters.AddWithValue("@razred", student.studentClass.Razred);
                cmd.Parameters.AddWithValue("@odjeljenje", student.studentClass.Odjeljenje);
                cmd.Parameters.AddWithValue("@škgod", student.studentClass.ŠkolskaGodina);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    subjects.Add(new Subject()
                    {
                        subjectName = reader.GetString(0),
                        zaključnaOcjena = reader.GetInt32(1),
                    });
                }
                reader.Close();
                conn.Close();
                return subjects;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Učitavanje zaključnih ocjena nije uspjelo!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
