using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatV2.Observer;
using ProjekatV2.Storage;
using Projekat_group5_team8.Model;
using ConsoleApp.Serialization;


namespace ProjekatV2.Model.DAO
{
    public class PredmetDAO : ISubject
    {
        private readonly List<IObserver> _PRobservers;
        private readonly PredmetStorage _PRstorage;
        private readonly List<Predmet> _predmeti;
        public PredmetDAO()
        {
            _PRstorage = new PredmetStorage();
            _predmeti = _PRstorage.Load();
            Ucitaj();
            Serializer<Student> serializerStudent = new Serializer<Student>();
            List<Student> studenti = serializerStudent.fromCSV("../../Data/students.csv");
            staviPolozeno(studenti);
            staviNepolozeno(studenti);
            //Fali samo pokretanje svih tih veza.
            _PRobservers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_predmeti.Count == 0)
            {
                return 1;
            }
            else
            {
                return _predmeti.Max(s => s.Pid) + 1;
            }
        }

        public Predmet addPredmet(Predmet predmet)
        {
            predmet.Pid = NextId();
            _predmeti.Add(predmet);
            _PRstorage.Save(_predmeti);
            NotifyObservers();
            return predmet;
        }

        private void Ucitaj() 
        {
            Serializer<StudentPredmetNisuPolozili> serializerStudentPredmet = new Serializer<StudentPredmetNisuPolozili>();
            List<StudentPredmetNisuPolozili> listaStudentPredmet = serializerStudentPredmet.fromCSV("../../Data/studentpredmet.csv");

            foreach (Predmet predmet in _predmeti)
            {
                LoadPredmetStudent(predmet, listaStudentPredmet);
            }
            SpojPredmetProfesor();
        }

        public Predmet updatePredmet(Predmet predmet)
        {
            Predmet oldPredmet = GetPredmetById(predmet.Pid);
            if (oldPredmet == null) return null;

            oldPredmet.Pid = predmet.Pid;
            oldPredmet.sifra = predmet.sifra;
            oldPredmet.naziv = predmet.naziv;
            oldPredmet.semestar = predmet.semestar;
            oldPredmet.godinastudija = predmet.godinastudija;
            oldPredmet.esp = predmet.esp;
            oldPredmet.predmetniProfesor = predmet.predmetniProfesor;
            oldPredmet.IdProfesor = predmet.IdProfesor;
            

            _PRstorage.Save(_predmeti); //Ne znam kako da ga dodam u listu da li starog da obrisem a novog da dodam za sad cu tako al mozda je greska mozda moze samo ovo SAVE
            NotifyObservers();

            return oldPredmet;

        }

        public Predmet removePredmet(int Pid)
        {
            Predmet predmet = GetPredmetById(Pid);
            if (predmet == null) return null;

            _predmeti.Remove(predmet);
            _PRstorage.Save(_predmeti);
            NotifyObservers();

            return predmet;
        }

        public Predmet GetPredmetById(int Pid)
        {
            return _predmeti.Find(v => v.Pid == Pid);
        }

        public List<Predmet> getAllPredmeti()
        {
            return _predmeti;
        }

        private void LoadPredmetStudent(Predmet predmet, List<StudentPredmetNisuPolozili> listStudPred)
        {
            List<Student> listPolozili = new List<Student>();
            List<Student> listPali = new List<Student>();
            foreach (StudentPredmetNisuPolozili np in listStudPred)
            {
                if (predmet.Pid == np.IdPredmeta)
                {
                    Student Student = new Student();
                    Student.id = np.IdStudenta;
                    listPali.Add(Student);
                }
                else
                {
                    Student Student = new Student();
                    Student.id = np.IdStudenta;
                    listPolozili.Add(Student);
                }
            }
            predmet.paliPredmet = listPali;
            predmet.poloziliPredmet = listPolozili;
        }

        public List<Predmet> SviPredmetiProfesora(Profesor profesor)
        {
            List<Predmet> listaPredmeta = new List<Predmet>();


            foreach (Predmet predmet in _predmeti)
            {
                if (!profesor.predmeti.Any(pred => pred.Pid == predmet.Pid))
                {

                    listaPredmeta.Add(predmet);
                }

            }
            return listaPredmeta;
        }

        private void SpojPredmetProfesor()
        {
            Serializer<Profesor> serializerProfesor = new Serializer<Profesor>();
            List<Profesor> profesori = serializerProfesor.fromCSV("../../Data/profesors.csv");

            foreach (Predmet Predmet in _predmeti)
            {
                Predmet.predmetniProfesor = profesori.Find(p => p.Pid == Predmet.IdProfesor);
            }

        }
        private bool uPolozenim(Predmet p, Student student)
        {
            foreach(Ocena o in student.ocenaNaIspitu)
            {
                if(o.predmetID == p.Pid)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Predmet> SviPredmetiStudentaNepolozeni(Student student)
        {
            List<Predmet> listaPredmeta = new List<Predmet>();


            foreach (Predmet predmet in _predmeti)
            {
                if (!student.nePolozeniIspiti.Any(pred => pred.Pid == predmet.Pid) && uPolozenim(predmet, student) && student.godinaStudija >= predmet.godinastudija)
                {
                    listaPredmeta.Add(predmet);
                }

            }
            return listaPredmeta;
        }

        public List<Predmet> SviPredmetiStudentaPolozeni(Student student)
        {
            List<Predmet> listaPredmeta = new List<Predmet>();


            foreach (Predmet predmet in _predmeti)
            {
                if (!student.polozeniIspiti.Any(pred => pred.Pid == predmet.Pid))
                {
                    listaPredmeta.Add(predmet);
                }

            }
            return listaPredmeta;
        }

        public void staviPolozeno(List<Student> listStud)
        {
            foreach (Predmet Predmet in _predmeti)
            {
                List<Student> studP = new List<Student>();
                foreach (Student student in listStud)
                {
                    bool polozen = true;
                    foreach (Student Student in Predmet.paliPredmet)
                    {
                        if (student.id == Student.id)
                        {
                            polozen = false;
                        }
                    }
                    if (polozen)
                    {
                        studP.Add(student);
                    }
                }
                Predmet.poloziliPredmet = studP;
            }

        }

        public void staviNepolozeno(List<Student> listStud)
        {
            foreach (Predmet predmet in _predmeti)
            {
                foreach (Student studentt in predmet.paliPredmet)
                {
                    Student student = listStud.Find(spom => spom.id == studentt.id);
                    studentt.ime = student.ime;
                    studentt.prezime = student.prezime;
                    studentt.datumRodjenja = student.datumRodjenja;
                    studentt.telefon = student.telefon;
                    studentt.email = student.email;
                    studentt.brojIndeksa = student.brojIndeksa;
                    studentt.godinaStudija = student.godinaStudija;
                    studentt.godinaUpisa = student.godinaUpisa;
                    studentt.status = student.status;
                    studentt.prosek = student.prosek;
                    studentt.nePolozeniIspiti = student.nePolozeniIspiti;

                }
            }
        }

        public List<Predmet> Search(string sifra, string naziv)
        {
            List<Predmet> SviPredmeti = new List<Predmet>();

            foreach (Predmet predmet in _predmeti)
            {
                if (Condition(predmet, sifra, naziv))
                {
                    SviPredmeti.Add(predmet);
                }
            }

            return SviPredmeti;
        }

        private bool Condition(Predmet predmet, string sifra, string naziv)
        {
            return predmet.naziv.ToLower().Contains(naziv.ToLower()) && predmet.sifra.ToLower().Contains(sifra.ToLower());
        }

        public void Subscribe(IObserver observer)
        {
            _PRobservers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _PRobservers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (var observer in _PRobservers)
            {
                observer.Update();
            }
        }


        public List<Predmet> SelectPredmetProfesor(Profesor SelectedProfesor)
        {
            List<Predmet> predmetlist = new List<Predmet>();

            foreach (Predmet pr in getAllPredmeti())
            {
                if(!SelectedProfesor.predmeti.Any(pred => pred.IdProfesor == pr.IdProfesor) && pr.predmetniProfesor == null)
                {
                    predmetlist.Add(pr);
                }
            }

            return predmetlist;
        }

    }
}
