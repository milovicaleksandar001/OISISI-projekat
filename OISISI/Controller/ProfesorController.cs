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
    public class ProfesorController
    {
        private readonly ProfesorDAO _profesors;

        public ProfesorController(ProfesorDAO profesorDAO)
        {
            _profesors = profesorDAO;
        }

        public List<Profesor> GetAllProfesors()
        {
            return _profesors.GetAllProfesori();
        }

        public void Create(Profesor profesor)
        {
            _profesors.addProfesor(profesor);
        }

        public void Delete(Profesor profesor)
        {
            _profesors.removeProfesor(profesor.Pid);
        }

        public void Update(Profesor profesor)
        {
            _profesors.updateProfesor(profesor);
        }

        public void Subscribe(IObserver observer)
        {
            _profesors.Subscribe(observer);
        }


        public List<Profesor> GetFilterProfesori()
        {
            return _profesors.GetFilterProfesori();
        }

        public List<Profesor> Search(string ime, string prezime){
            return _profesors.Search(ime, prezime);
        }

    }
}