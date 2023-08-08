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
    public class OcenaController
    {
        private readonly OcenaDAO _ocene;

        public OcenaController(OcenaDAO ocenaDAO)
        {
            _ocene = ocenaDAO;
        }

        public List<Ocena> getAllOcene()
        {
            return _ocene.getAllOcene();
        }

        public void Create(Ocena ocena)
        {
            _ocene.addOcena(ocena);
        }

        public void Delete(Ocena ocena)
        {
            _ocene.removeOcena(ocena.Oid);
        }

        public void Update(Ocena ocena)
        {
            _ocene.updateOcena(ocena);
        }

        public void Subscribe(IObserver observer)
        {
            _ocene.Subscribe(observer);
        }
    }
}
