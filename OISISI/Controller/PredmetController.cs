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
    public class PredmetController
    {
        private readonly PredmetDAO _predmeti;

        public PredmetController(PredmetDAO predmetDAO)
        {
            _predmeti = predmetDAO;
        }

        public List<Predmet> getAllPredmeti()
        {
            return _predmeti.getAllPredmeti();
        }

        public void Create(Predmet predmet)
        {
            _predmeti.addPredmet(predmet);
        }

        public void Delete(Predmet predmet)
        {
            _predmeti.removePredmet(predmet.Pid);
        }

        public void Update(Predmet predmet)
        {
            _predmeti.updatePredmet(predmet);
        }

        public List<Predmet> SviPredmetiProfesora(Profesor profesor)
        {
            return _predmeti.SviPredmetiProfesora(profesor);
        }

        public List<Predmet> SviPredmetiStudentaNepolozeni(Student student)
        {
            return _predmeti.SviPredmetiStudentaNepolozeni(student);
            //return _predmeti.staviNepolozeno();// prima listu ko arg
        }

        public List<Predmet> SviPredmetiStudentaPolozeni(Student student)
        {
            return _predmeti.SviPredmetiStudentaPolozeni(student);
            //return _predmeti.staviPolozeno();// prima listu ko arg
        }

        public void Subscribe(IObserver observer)
        {
            _predmeti.Subscribe(observer);
        }

        public List<Predmet> Search(string sifra, string naziv)
        {
            return _predmeti.Search(sifra, naziv);
        }

    }
}
