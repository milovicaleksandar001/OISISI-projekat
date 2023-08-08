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
    public class KatedraController
    {
        private readonly KatedraDAO _katedre;

        public KatedraController(KatedraDAO katedraDAO)
        {
            _katedre = katedraDAO;
        }

        public List<Katedra> GetAllKatedre()
        {
            return _katedre.GetAllKatedre();
        }

        public void Create(Katedra katedra)
        {
            _katedre.addKatedra(katedra);
        }

        public void Delete(Katedra katedra)
        {
            _katedre.removeKatedra(katedra.Kid);
        }

        public void Update(Katedra katedra)
        {
            _katedre.updateKatedra(katedra);
        }

        public void Subscribe(IObserver observer)
        {
            _katedre.Subscribe(observer);
        }
    }
}
