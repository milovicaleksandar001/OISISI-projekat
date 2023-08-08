using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using System.Windows.Markup;
using System.Xml.Linq;
//using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;


namespace Projekat_group5_team8.Model
{
    public class Ocena : Serializable
    {
        private int OID;
        private Student StudentPolozio;
        private Predmet Predmet;
        private int StudentID;
        private int PredmetID;
        private int BrVrednostOcene;
        DateTime DatumPolaganja;


        public Ocena(int oID, int brVrednostOcene, DateTime datumPolaganja)
        {
            this.OID = oID;
            this.BrVrednostOcene = brVrednostOcene;
            this.DatumPolaganja = datumPolaganja;
            this.StudentPolozio = studentPolozio;
            this.Predmet = predmet;
            this.PredmetID = predmetID;
            this.StudentID = studentID;
        }

        public Ocena() { }

        public int Oid
        {
            get { return OID; }
            set { OID = value; }
        }

        public int Brvrednostocene
        {
            get { return BrVrednostOcene; }
            set { BrVrednostOcene = value; }
        }

        public DateTime Datumpolaganja
        {
            get { return DatumPolaganja; }
            set { DatumPolaganja = value; }
        }

        public Predmet predmet
        {
            get { return Predmet; }
            set { Predmet = value; }
        }

        public Student studentPolozio
        {
            get { return StudentPolozio; }
            set { StudentPolozio = value; }
        }

        public int studentID
        {
            get { return StudentID; }
            set { StudentID = value; }
        }

        public int predmetID
        {
            get { return PredmetID; }
            set { PredmetID = value; }
        }

        public override string ToString()
        {
            return string.Format("ID: {0} \n Brojcana vrednost ocene: {1} \n Datum polaganja: {2} \n Student polozio: {3} \n Predmet: {4}",
                                  Oid, Brvrednostocene, Datumpolaganja, StudentID, PredmetID);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
           {
            OID.ToString(),
            StudentID.ToString(),
            PredmetID.ToString(),
            BrVrednostOcene.ToString(),
            DatumPolaganja.ToString()
           };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            OID = int.Parse(values[0]);
            StudentID = int.Parse(values[1]);
            PredmetID = int.Parse(values[2]);
            BrVrednostOcene = int.Parse(values[3]);
            DatumPolaganja = DateTime.Parse(values[4]);
        }


    }
}
