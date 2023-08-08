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
    internal class StudentPredmetNisuPoloziliDAO : ISubject
    {
        private readonly List<IObserver> _observeri;


        private List<StudentPredmetNisuPolozili> studentNijePolozioPredmete;
        private Serializer<StudentPredmetNisuPolozili> serializer;
        public StudentDAO StudentDAO { get; set; }
        public PredmetDAO PredmetDAO { get; set; }

        private readonly string fileName = "../../Data/studentpredmet.csv";

        public StudentPredmetNisuPoloziliDAO()
        {
            serializer = new Serializer<StudentPredmetNisuPolozili>();
            _observeri = new List<IObserver>();
            UcitajStudentPredmet();
        }

        private void UcitajStudentPredmet()
        {
            studentNijePolozioPredmete = serializer.fromCSV(fileName);
        }

        private void SacuvajStudentPredmet()
        {
            serializer.toCSV(fileName, studentNijePolozioPredmete);
        }



        public void DodajStudentPredmet(StudentPredmetNisuPolozili studentNPPredmet)
        {

            studentNijePolozioPredmete.Add(studentNPPredmet);
            SacuvajStudentPredmet();
        }


        public void UveziStudentPredmetNepolozen()
        {
            foreach (StudentPredmetNisuPolozili studentNPPredmet in studentNijePolozioPredmete)
            {
                Student student = StudentDAO.GetStudentById(studentNPPredmet.IdStudenta);
                Predmet predmet = PredmetDAO.GetPredmetById(studentNPPredmet.IdPredmeta);
                if (predmet == null || student == null)
                {
                    System.Console.WriteLine("Greska tokom ucitavanju veze studenta i predmeta");
                }
                else
                {
                    predmet.paliPredmet.Add(student);
                    student.nePolozeniIspiti.Add(predmet);
                }
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observeri.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observeri.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observeri)
            {
                observer.Update();
            }
        }
    }
}
