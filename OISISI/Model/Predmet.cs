using ConsoleApp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum enStatusPredmet
{
    L,
    Z
};


namespace Projekat_group5_team8.Model
{
    public class Predmet : Serializable
    {
        private int PredmetID;
        private string Sifra;
        private string Naziv;
        private enStatusPredmet Semestar;
        private int GodinaStudija;
        private int IDProfesora;
        private Profesor PredmetniProfesor;
        private int ESP;
        private List<Student> PoloziliPredmet;
        private List<Student> PaliPredmet;


        public Predmet(int Pid, string sifra, string naziv, enStatusPredmet semestar, int godinastudija, int esp)
        {
            this.PredmetID = Pid;
            this.Sifra = sifra;
            this.Naziv = naziv;
            this.Semestar = semestar;
            this.GodinaStudija = godinastudija;
            ESP = esp;
            PoloziliPredmet = new List<Student>();
            PaliPredmet = new List<Student>();
        }

        public Predmet()
        {
            PoloziliPredmet = new List<Student>();
            PaliPredmet = new List<Student>();
        }

        public int IdProfesor
        {
            get { return IDProfesora; }
            set { IDProfesora = value; }
        }
        public Profesor predmetniProfesor
        {
            get { return PredmetniProfesor; }
            set { PredmetniProfesor = value; }
        }

        public List<Student> poloziliPredmet
        {
            get { return PoloziliPredmet; }
            set { PoloziliPredmet = value; }
        }

        public List<Student> paliPredmet
        {
            get { return PaliPredmet; }
            set { PaliPredmet = value; }
        }

        public int Pid
        {
            get { return PredmetID; }
            set { PredmetID = value; }
        }

        public string sifra
        {
            get { return Sifra; }
            set { Sifra = value; }
        }

        public string naziv
        {
            get { return Naziv; }
            set { Naziv = value; }
        }
        public enStatusPredmet semestar
        {
            get { return Semestar; }
            set { Semestar = value; }
        }

        public int godinastudija
        {
            get { return GodinaStudija; }
            set { GodinaStudija = value; }
        }

        public int esp
        {
            get { return ESP; }
            set { ESP = value; }
        }

        public override string ToString()
        {
            return string.Format(" ID: {0} \n Sifra: {1} \n Naziv: {2} \n Semestar: {3} \n GodinaStudija: {4} \n Profesor:{5} \n ESP: {6} \n",
                                   PredmetID, Sifra, Naziv, Semestar, GodinaStudija, IDProfesora, ESP);
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
            PredmetID.ToString(),
            Sifra,
            Naziv,
            Semestar.ToString(),
            GodinaStudija.ToString(),
            IDProfesora.ToString(),
            ESP.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            PredmetID = int.Parse(values[0]);
            Sifra = values[1];
            Naziv = values[2];
            if (values[3] == "L")
            {
                Semestar = enStatusPredmet.L;
            }
            else if (values[3] == "Z")
            {
                Semestar = enStatusPredmet.Z;
            }
            GodinaStudija = int.Parse(values[4]);
            IDProfesora = int.Parse(values[5]);
            ESP = int.Parse(values[6]);
        }

    }
}
