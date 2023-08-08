using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatV2.Observer;
using Projekat_group5_team8.Model;
using ProjekatV2.Model.DAO;

namespace ProjekatV2.Controller
{
    public class AdresaController
    {
        private readonly AdresaDAO _adrese;

        public AdresaController(AdresaDAO adresaDAO)
        {
            _adrese = adresaDAO;
        }

        public List<Adresa> getAllAdrese()
        {
            return _adrese.getAllAdrese();
        }

        public void Create(Adresa adresa)
        {
            _adrese.addAdresa(adresa);
        }

        public void Delete(Adresa adresa)
        {
            _adrese.removeAdresa(adresa.Aid);
        }

        public void Update(Adresa adresa)
        {
            _adrese.updateAdresa(adresa);
        }

        public void Subscribe(IObserver observer)
        {
            _adrese.Subscribe(observer);
        }
    }
}
