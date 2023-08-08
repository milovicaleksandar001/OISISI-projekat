using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using System.Windows.Markup;
using System.Xml.Linq;
//using System.Reflection.Metadata.Ecma335;

namespace Projekat_group5_team8.Model
{
    public class Adresa : Serializable
    {
        private int AID;
        private string AUlica;
        private int ABroj;
        private string AGrad;
        private string ADrzava;



        public Adresa(int Aid, string Aulica, int Abroj, string Agrad, string Adrzava)
        {
            this.AID = Aid;
            this.AUlica = Aulica;
            this.ABroj = Abroj;
            this.AGrad = Agrad;
            this.ADrzava = Adrzava;

        }

        public Adresa() { }

        public int Aid
        {
            get { return AID; }
            set { AID = value; }
        }

        public string Aulica
        {
            get { return AUlica; }
            set { AUlica = value; }
        }
        public int Abroj
        {
            get { return ABroj; }
            set { ABroj = value; }
        }

        public string Agrad
        {
            get { return AGrad; }
            set { AGrad = value; }
        }
        public string Adrzava
        {
            get { return ADrzava; }
            set { ADrzava = value; }
        }

        public override string ToString()
        {
            return string.Format("ID: {0} \n Ulica:{1} \n Broj: {2} \n Grad: {3} \n Drzava: {4}",
                                  AID, AUlica, ABroj, AGrad, ADrzava);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
           {
            AID.ToString(),
            AUlica,
            ABroj.ToString(),
            AGrad,
            ADrzava
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            AID = int.Parse(values[0]);
            Aulica = values[1];
            ABroj = int.Parse(values[2]);
            AGrad = values[3];
            ADrzava = values[4];
        }
    }
}
