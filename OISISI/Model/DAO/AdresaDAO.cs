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
    public class AdresaDAO : ISubject
    {
        private readonly List<IObserver> _Aobservers;
        private readonly AdresaStorage _Astorage;
        private readonly List<Adresa> _adrese;

        public AdresaDAO()
        {
            _Astorage = new AdresaStorage();
            _adrese = _Astorage.Load();
            _Aobservers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_adrese.Count == 0)
            {
                return 1;
            }
            else
            {
                return _adrese.Max(s => s.Aid) + 1;
            }
        }

        public Adresa addAdresa(Adresa adresa)
        {
            adresa.Aid = NextId();
            _adrese.Add(adresa);
            _Astorage.Save(_adrese);
            NotifyObservers();
            return adresa;
        }

        public Adresa updateAdresa(Adresa adresa)
        {
            Adresa oldAdresa = GetAdresaById(adresa.Aid);
            if (oldAdresa == null) return null;

            oldAdresa.Aid = adresa.Aid;
            oldAdresa.Aulica = adresa.Aulica;
            oldAdresa.Abroj = adresa.Abroj;
            oldAdresa.Agrad = adresa.Agrad;
            oldAdresa.Adrzava = adresa.Adrzava;

            _adrese.Remove(adresa);
            _Astorage.Save(_adrese);
            _adrese.Add(oldAdresa);
            _Astorage.Save(_adrese); //Ne znam kako da ga dodam u listu da li starog da obrisem a novog da dodam za sad cu tako al mozda je greska mozda moze samo ovo SAVE
            NotifyObservers();

            return oldAdresa;

        }

        public Adresa removeAdresa(int Aid)
        {
            Adresa adresa = GetAdresaById(Aid);
            if (adresa == null) return null;

            _adrese.Remove(adresa);
            _Astorage.Save(_adrese);
            NotifyObservers();

            return adresa;
        }

        public Adresa GetAdresaById(int Aid)
        {
            return _adrese.Find(v => v.Aid == Aid);
        }

        public List<Adresa> getAllAdrese()
        {
            return _adrese;
        }


        public void Subscribe(IObserver observer)
        {
            _Aobservers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _Aobservers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (var observer in _Aobservers)
            {
                observer.Update();
            }
        }

    }
}
