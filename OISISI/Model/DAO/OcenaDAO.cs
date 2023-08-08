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
    public class OcenaDAO : ISubject
    {
        private readonly List<IObserver> _Oobservers;
        private readonly OcenaStorage _Ostorage;
        private readonly List<Ocena> _ocene;
        public OcenaDAO()
        {
            _Ostorage = new OcenaStorage();
            _ocene = _Ostorage.Load();
            spojOcena();
            _Oobservers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_ocene.Count == 0)
            {
                return 1;
            }
            else
            {
                return _ocene.Max(s => s.Oid) + 1;
            }
        }
        public Ocena addOcena(Ocena ocena)
        {
            ocena.Oid = NextId();
            _ocene.Add(ocena);
            _Ostorage.Save(_ocene);
            NotifyObservers();
            return ocena;
        }

        public Ocena updateOcena(Ocena ocena)
        {
            Ocena oldOcena = getOcenaById(ocena.Oid);
            if (oldOcena == null) return null;

            oldOcena.Oid = ocena.Oid;
            oldOcena.Brvrednostocene = ocena.Brvrednostocene;
            oldOcena.Datumpolaganja = ocena.Datumpolaganja;

            _ocene.Remove(ocena);
            _Ostorage.Save(_ocene);
            _ocene.Add(oldOcena);
            _Ostorage.Save(_ocene); //Ne znam kako da ga dodam u listu da li starog da obrisem a novog da dodam za sad cu tako al mozda je greska mozda moze samo ovo SAVE
            NotifyObservers();

            return oldOcena;
        }

        public Ocena removeOcena(int Oid)
        {
            Ocena ocena = getOcenaById(Oid);
            if (ocena == null) return null;

            _ocene.Remove(ocena);
            _Ostorage.Save(_ocene);
            NotifyObservers();

            return ocena;
        }


        public Ocena getOcenaById(int Oid)
        {
            return _ocene.Find(v => v.Oid == Oid);
        }

        public List<Ocena> getAllOcene()
        {
            return _ocene;
        }

        private void spojOcena()
        {
            Serializer<Predmet> serializerProfesor = new Serializer<Predmet>();
            List<Predmet> listPredmeti = serializerProfesor.fromCSV("../../Data/predmeti.csv");
            Serializer<Student> serializerStudent = new Serializer<Student>();
            List<Student> listStudenti = serializerStudent.fromCSV("../../Data/students.csv");

            foreach (Ocena ocena in _ocene)
            {
                ocena.predmet = listPredmeti.Find(predmet => predmet.Pid == ocena.predmetID);
                ocena.studentPolozio = listStudenti.Find(student => student.id == ocena.studentID);
            }


        }


        public void Subscribe(IObserver observer)
        {
            _Oobservers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _Oobservers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (var observer in _Oobservers)
            {
                observer.Update();
            }
        }

    }
}
