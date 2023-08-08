using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatV2.Observer;
using ProjekatV2.Storage;
using Projekat_group5_team8.Model;
using ConsoleApp.Serialization;
using System.Runtime.Serialization;

namespace ProjekatV2.Model.DAO
{
    public class KatedraDAO : ISubject
    {

        private readonly List<IObserver> _Kobservers;
        private readonly KatedraStorage _Kstorage;
        private readonly List<Katedra> _katedre;
        public ProfesorDAO ProfesorDAO { get; set; }
        public KatedraDAO()
        {
            _Kstorage = new KatedraStorage();
            _katedre = _Kstorage.Load();
            _Kobservers = new List<IObserver>();
        }
        public int NextId()
        {
            if (_katedre.Count == 0)
            {
                return 1;
            }
            else
            {
                return _katedre.Max(s => s.Kid) + 1;
            }
        }

        public void BindSefKatedra()
        {
            foreach(Katedra k in _katedre)
            {
                k.sef = ProfesorDAO.GetProfesorById(k.sefKatedreID);
            }
        }

        public Katedra addKatedra(Katedra katedra)
        {
            katedra.Kid = NextId();
            _katedre.Add(katedra);
            _Kstorage.Save(_katedre);
            NotifyObservers();
            return katedra;
        }

        public Katedra updateKatedra(Katedra katedra)
        {
            Katedra oldKatedra = GetKatedraByid(katedra.Kid);
            if (oldKatedra == null) return null;

            oldKatedra.Kid = katedra.Kid;
            oldKatedra.Ksifra = katedra.Ksifra;
            oldKatedra.Knaziv = katedra.Knaziv;

            _katedre.Remove(katedra);
            _Kstorage.Save(_katedre);
            _katedre.Add(oldKatedra);
            _Kstorage.Save(_katedre); 
            NotifyObservers();


            return oldKatedra;
        }

        public Katedra removeKatedra(int id)
        {
            Katedra katedra = GetKatedraByid(id);
            if (katedra == null) return null;

            _katedre.Remove(katedra);
            _Kstorage.Save(_katedre);
            NotifyObservers();

            return katedra;
        }

        public Katedra GetKatedraByid(int Kid)
        {
            return _katedre.Find(v => v.Kid == Kid);
        }

        public List<Katedra> GetAllKatedre()
        {
            return _katedre;
        }

        public void Subscribe(IObserver observer)
        {
            _Kobservers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _Kobservers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (var observer in _Kobservers)
            {
                observer.Update();
            }
        }
    }
}
