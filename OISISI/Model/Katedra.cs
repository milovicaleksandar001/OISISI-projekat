using ConsoleApp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_group5_team8.Model
{
    public class Katedra : Serializable
    {
        private int KatedraID;
        private string KSifra;
        private string KNaziv;
        private int SefKatedreID;
        private Profesor Sef;
        private List<Profesor> Profesori;


        public Katedra(int Kid, string Ksifra, string Knaziv, Profesor sef, List<Profesor> profesori)
        {
            this.KatedraID = Kid;
            this.KSifra = Ksifra;
            this.KNaziv = Knaziv;
            this.Sef = sef;
            Profesori = new List<Profesor>();
        }

        public Katedra()
        {
            Profesori = new List<Profesor>();
        }


        public int Kid
        {
            get { return KatedraID; }
            set { KatedraID = value; }
        }

        public string Ksifra
        {
            get { return KSifra; }
            set { KSifra = value; }
        }

        public string Knaziv
        {
            get { return KNaziv; }
            set { KNaziv = value; }
        }

        public int sefKatedreID
        {
            get { return SefKatedreID; }
            set { SefKatedreID = value; }
        }

        public Profesor sef
        {
            get { return Sef; }
            set { Sef = value; }
        }

        public List<Profesor> profesori
        {
            get { return Profesori; }
            set { Profesori = value; }
        }
        public override string ToString()
        {
            return string.Format(" ID: {0} \n Sifra: {1} \n Naziv: {2} \n Sef Katedre: {3} \n",
                                   KatedraID, KSifra, KNaziv, SefKatedreID);
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
            KatedraID.ToString(),
            KSifra,
            KNaziv,
            SefKatedreID.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            KatedraID = int.Parse(values[0]);
            KSifra = values[1];
            KNaziv = values[2];
            SefKatedreID = int.Parse(values[3]);
        }
    }
}
