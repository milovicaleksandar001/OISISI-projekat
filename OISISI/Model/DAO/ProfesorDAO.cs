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
    public class ProfesorDAO : ISubject
    {
        private readonly List<IObserver> _Pobservers;
        private readonly ProfesorStorage _Pstorage;
        private readonly List<Profesor> _profesors;
        public ProfesorDAO()
        {
            _Pstorage = new ProfesorStorage();
            _profesors = _Pstorage.Load();
            spojProfesor();
            spojProfesorAdresa();
            spojProfesorPredmet();
            _Pobservers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_profesors.Count == 0)
            {
                return 1;
            }
            else
            {
                return _profesors.Max(s => s.Pid) + 1;
            }
        }


        public Profesor addProfesor(Profesor profesor)
        {
            profesor.Pid = NextId();
            _profesors.Add(profesor);
            _Pstorage.Save(_profesors);
            NotifyObservers();
            return profesor;
        }

        public Profesor removeProfesor(int Pid)
        {
            Profesor profesor = GetProfesorById(Pid);
            if (profesor == null) return null;

            _profesors.Remove(profesor);
            _Pstorage.Save(_profesors);
            NotifyObservers();

            return profesor;
        }

        public Profesor updateProfesor(Profesor profesor)
        {
            Profesor oldProfesor = GetProfesorById(profesor.Pid);
            if (oldProfesor == null) return null;

            oldProfesor.Pprezime = profesor.Pprezime;
            oldProfesor.Pime = profesor.Pime;
            oldProfesor.PdatumRodjenja = profesor.PdatumRodjenja;
            oldProfesor.Padresastanovanja = profesor.Padresastanovanja;
            oldProfesor.Ptelefon = profesor.Ptelefon;
            oldProfesor.Pemail = profesor.Pemail;
            oldProfesor.Padresakancelarije = profesor.Padresakancelarije;
            oldProfesor.Pbroj_licne = profesor.Pbroj_licne;
            oldProfesor.Pzvanje = profesor.Pzvanje;
            oldProfesor.Pstaz = profesor.Pstaz;

            
            _Pstorage.Save(_profesors); 
            NotifyObservers();

            return oldProfesor;
        }

        public Profesor GetProfesorById(int Pid)
        {
            return _profesors.Find(v => v.Pid == Pid);
        }

        public List<Profesor> GetAllProfesori()
        {
            return _profesors;
        }

        private void spojProfesorPredmet()
        {
            Serializer<Predmet> serializerPredmet = new Serializer<Predmet>();
            List<Predmet> listPredmeti = serializerPredmet.fromCSV("../../Data/predmeti.csv");

            foreach (Predmet Predmet in listPredmeti)
            {
                foreach (Profesor Profesor in _profesors)
                {
                    if (Predmet.IdProfesor == Profesor.Pid)
                    {
                        Profesor.predmeti.Add(Predmet);
                    }
                }
            }
        }

        public void spojProfesorAdresa()
        {
            foreach (Profesor Profesor in _profesors)
            {
                AdresaDAO adrese = new AdresaDAO();
                Adresa adresa = adrese.GetAdresaById(Profesor.PadresastanovanjaID);
                Profesor.Padresastanovanja = adresa;
            }

            foreach (Profesor Profesor in _profesors)
            {
                AdresaDAO adrese = new AdresaDAO();
                Adresa adresa = adrese.GetAdresaById(Profesor.PadresakancelarijeID);
                Profesor.Padresakancelarije = adresa;
            }


        }

        public void spojProfesor()
        {
            foreach (Profesor Profesor in _profesors)
            {
                AdresaDAO adresa = new AdresaDAO();
                Adresa Adresaa = adresa.GetAdresaById(Profesor.PadresastanovanjaID);
                Profesor.Padresastanovanja = Adresaa;
            }

            foreach (Profesor Profesor in _profesors)
            {
                AdresaDAO adresa = new AdresaDAO();
                Adresa Adresaa = adresa.GetAdresaById(Profesor.PadresakancelarijeID);
                Profesor.Padresakancelarije = Adresaa;
            }
        }

        public List<Profesor> Search(string ime, string prezime)
        {
            List<Profesor> SviProfesori = new List<Profesor>();

            foreach (Profesor profesor in _profesors)
            {
                if (Condition(profesor,ime, prezime))
                {
                    SviProfesori.Add(profesor);
                }
            }

            return SviProfesori;
        }

        private bool Condition(Profesor profesor,string ime, string prezime)
        {
            return profesor.Pprezime.ToLower().Contains(prezime.ToLower()) && profesor.Pime.ToLower().Contains(ime.ToLower());
        }

        public void Subscribe(IObserver observer)
        {
            _Pobservers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _Pobservers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (var observer in _Pobservers)
            {
                observer.Update();
            }
        }

        public List<Profesor> GetFilterProfesori()
        {
            List<Profesor> _filterprofesors = new List<Profesor>();
            foreach (Profesor p in _profesors)
            {
                if ((p.Pzvanje.Equals("REDOVNI_PROFESOR") || p.Pzvanje.Equals("VANREDNI_PROFESOR")) && p.Pstaz >= 5)
                { 
                    _filterprofesors.Add(p);
                }

            }

                return _filterprofesors;

        }

    }
}
