using ProjekatV2.Observer;
using ProjekatV2.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekat_group5_team8.Model;
using ConsoleApp.Serialization;

namespace ProjekatV2.Model.DAO
{
    public class StudentDAO : ISubject
    {

        public AdresaDAO AdresaDAO { get; set; }

        private readonly List<IObserver> _observers;
        private readonly StudentStorage _storage;
        private List<Student> _students;

        public StudentDAO()
        {
            _storage = new StudentStorage();
            LoadStudenti();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_students.Count == 0)
            {
                return 1;
            }
            else
            {
                return _students.Max(s => s.id) + 1;
            }
        }

        public Student addStudent(Student student)
        {
            student.id = NextId();
            _students.Add(student);
            _storage.Save(_students);
            NotifyObservers();
            return student;
        }

        public Student updateStudent(Student student)
        {
            Student oldStudent = GetStudentById(student.id);
            if (oldStudent == null) return null;

            oldStudent.prezime = student.prezime;
            oldStudent.ime = student.ime;
            oldStudent.datumRodjenja = student.datumRodjenja;
            oldStudent.adresa = student.adresa;
            oldStudent.telefon = student.telefon;
            oldStudent.email = student.email;
            oldStudent.brojIndeksa = student.brojIndeksa;
            oldStudent.godinaUpisa = student.godinaUpisa;
            oldStudent.godinaStudija = student.godinaStudija;
            oldStudent.status = student.status;
            oldStudent.prosek = Prosek(student);
            oldStudent.IdAdresa = student.IdAdresa;
            oldStudent.nePolozeniIspiti = student.nePolozeniIspiti;
            oldStudent.ocenaNaIspitu = student.ocenaNaIspitu;

            
            _storage.Save(_students); //Ne znam kako da ga dodam u listu da li starog da obrisem a novog da dodam za sad cu tako al mozda je greska
            NotifyObservers();
            SaveStudentPredmeti();

            return oldStudent;
        }

        public Student GetStudentById(int id)
        {
            return _students.Find(v => v.id == id);
        }

        public Student removeStudent(int id)
        {
            Student student = GetStudentById(id);
            if (student == null) return null;

            _students.Remove(student);
            AdresaDAO.removeAdresa(student.IdAdresa);
            NotifyObservers();
            _storage.Save(_students);

            return student;
        }
        public List<Student> GetAllStudenti()
        {
            return _students;
        }

        private void LoadStudentPredmet(Student student, List<StudentPredmetNisuPolozili> StudentNPPredmet)
        {
            List<Predmet> NepolozeniPredmeti = new List<Predmet>();
            foreach (StudentPredmetNisuPolozili snpp in StudentNPPredmet)
            {
                if (snpp.IdStudenta == student.id)
                {
                    Predmet predmet = new Predmet();
                    predmet.Pid = snpp.IdPredmeta;
                    NepolozeniPredmeti.Add(predmet);
                }
            }
            student.nePolozeniIspiti = NepolozeniPredmeti;
        }

        public void StudentSpojPredmet()
        {

            Serializer<Predmet> Serializer = new Serializer<Predmet>();
            List<Predmet> Predmeti = Serializer.fromCSV("../../Data/predmeti.csv");
            AdresaDAO Adresa = new AdresaDAO();


            foreach (Student s in _students)
            {
                Adresa Adresaa = Adresa.GetAdresaById(s.IdAdresa);
                s.adresa = Adresaa;

                foreach (Predmet predmet in s.nePolozeniIspiti)
                {
                    Predmet NadjenPredmet = Predmeti.Find(ptmp => ptmp.Pid == predmet.Pid);
                    predmet.sifra = NadjenPredmet.sifra;
                    predmet.naziv = NadjenPredmet.naziv;
                    predmet.semestar = NadjenPredmet.semestar;
                    predmet.godinastudija = NadjenPredmet.godinastudija;
                    predmet.IdProfesor = NadjenPredmet.IdProfesor;
                    predmet.predmetniProfesor = NadjenPredmet.predmetniProfesor;
                    predmet.esp = NadjenPredmet.esp;
                    predmet.poloziliPredmet = NadjenPredmet.poloziliPredmet;
                    predmet.paliPredmet = NadjenPredmet.paliPredmet;
                }

            }
        }

        private void StudentSpojOcena()
        {
            Serializer<Ocena> SerializerOcena = new Serializer<Ocena>();
            List<Ocena> Ocene = SerializerOcena.fromCSV("../../Data/ocene.csv");

            Serializer<Predmet> Serializer = new Serializer<Predmet>();
            List<Predmet> Predmeti = Serializer.fromCSV("../../Data/predmeti.csv");


            foreach (Ocena ocena in Ocene)
            {
                Student student = GetStudentById(ocena.studentID);
                student.ocenaNaIspitu.Add(ocena);
                ocena.studentPolozio = student;
                Predmet predmet = Predmeti.Find(p => p.Pid == ocena.predmetID);
                ocena.predmet = predmet;
            }

        }

        private void LoadStudenti()
        {
            _students = _storage.Load();

            Serializer<StudentPredmetNisuPolozili> SerializerStudentNPPredmet = new Serializer<StudentPredmetNisuPolozili>();
            List<StudentPredmetNisuPolozili> StudentNPPredmet = SerializerStudentNPPredmet.fromCSV("../../Data/StudentPredmet.csv");

            foreach (Student student in _students)
            {
                LoadStudentPredmet(student, StudentNPPredmet);
            }
            StudentSpojPredmet();
            StudentSpojOcena();
        }

        private void SaveStudentPredmeti()
        {
            Serializer<StudentPredmetNisuPolozili> SerializerStudentNPPredmet = new Serializer<StudentPredmetNisuPolozili>();
            List<StudentPredmetNisuPolozili> StudentPredmeti = new List<StudentPredmetNisuPolozili>();
            foreach (Student student in _students)
            {
                foreach(Predmet predmet in student.nePolozeniIspiti)
                {
                    StudentPredmeti.Add(new StudentPredmetNisuPolozili(student.id, predmet.Pid));
                }
            }
            SerializerStudentNPPredmet.toCSV("../../Data/StudentPredmet.csv", StudentPredmeti);


        }

        public int UkupnoESP(Student student)  {
            int suma = 0;
            foreach (Ocena ocena in student.ocenaNaIspitu) {
                suma = suma+ocena.predmet.esp;
            }

            return suma;
        }

        public double Prosek(Student student) {
            double prosek=0;
            int pomoc=0;
            foreach (Ocena ocena in student.ocenaNaIspitu) {
                prosek = prosek+ocena.Brvrednostocene;
                pomoc++;
            }
            return prosek/pomoc;
        }

        public List<Student> Search(string index, string ime, string prezime)
        {
            List<Student> searchedStudents = new List<Student>();

            foreach (Student student in _students)
            {
                if (Condition(student, index, ime, prezime))
                {
                    searchedStudents.Add(student);
                }
            }

            return searchedStudents;
        }

        private bool Condition(Student student, string index, string ime, string prezime)
        {
            return student.prezime.ToLower().Contains(prezime.ToLower()) && student.ime.ToLower().Contains(ime.ToLower()) && student.brojIndeksa.ToLower().Contains(index.ToLower());
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

    }
}
