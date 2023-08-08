using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using System.Windows.Markup;
using System.Xml.Linq;

namespace Projekat_group5_team8.Model
{
    public class Profesor : Serializable
    {

        private int PID;
        private string PPrezime;
        private string PIme;
        private DateTime PDatumRodjenja;
        private int PAdresaStanovanjaID;
        private Adresa PAdresaStanovanja;
        private string PTelefon;
        private string PEmail;
        private int PAdresaKancelarijeID;
        private Adresa PAdresakancelarije;
        private int PadresaStanovanjaID;
        private Adresa PAdresastanovanja;
        private string PBroj_licne;
        private string PZvanje;
        private int PStaz;
        private List<Predmet> Predmeti;
        private int IDKatedre;



        public Profesor(int Pid, string Pprezime, string Pime, DateTime Pdatumrodjenja, int PadresastanovanjaID, Adresa Padresastanovanja, string Ptelefon, string Pemail, int PadresalancelarijeID, Adresa Padresakancelarije, string Pbroj_licne, string Pzvanje, int Pstaz)
        {
            this.PID = Pid;
            this.PPrezime = Pprezime;
            this.PIme = Pime;
            this.PDatumRodjenja = Pdatumrodjenja;
            this.PAdresaStanovanjaID = PadresastanovanjaID;
            this.PAdresaStanovanja = Padresastanovanja;
            this.PTelefon = Ptelefon;
            this.PEmail = Pemail;
            this.PAdresaKancelarijeID = PadresakancelarijeID;
            this.PAdresakancelarije = Padresakancelarije;
            this.PBroj_licne = Pbroj_licne;
            this.PZvanje = Pzvanje;
            this.PStaz = Pstaz;
            Predmeti = new List<Predmet>();
        }
        public Profesor()
        {
            Predmeti = new List<Predmet>();
        }


        public int Pid
        {
            get { return PID; }
            set { PID = value; }
        }

        public string Pprezime
        {
            get { return PPrezime; }
            set { PPrezime = value; }
        }

        public string Pime
        {
            get { return PIme; }
            set { PIme = value; }
        }

        public DateTime PdatumRodjenja
        {
            get { return PDatumRodjenja; }
            set { PDatumRodjenja = value; }
        }


        // ADRESE
        public int PadresakancelarijeID
        {
            get { return PAdresaKancelarijeID; }
            set { PAdresaKancelarijeID = value; }
        }

        public Adresa Padresastanovanja
        {
            get { return PAdresastanovanja; }
            set { PAdresastanovanja = value; }
        }

        public Adresa Padresakancelarije
        {
            get { return PAdresakancelarije; }
            set { PAdresakancelarije = value; }
        }

        public int PadresastanovanjaID
        {
            get { return PadresaStanovanjaID; }
            set { PadresaStanovanjaID = value; }
        }

        //
        public string Ptelefon
        {
            get { return PTelefon; }
            set { PTelefon = value; }
        }

        public string Pemail
        {
            get { return PEmail; }
            set { PEmail = value; }
        }

      

        public string Pbroj_licne
        {
            get { return PBroj_licne; }
            set { PBroj_licne = value; }
        }

        public string Pzvanje
        {
            get { return PZvanje; }
            set { PZvanje = value; }
        }

        public int Pstaz
        {
            get { return PStaz; }
            set { PStaz = value; }
        }

        public int IDkatedre
        {
            get { return IDKatedre; }
            set { IDKatedre = value; }
        }

        public List<Predmet> predmeti
        {
            get { return Predmeti; }
            set { Predmeti = value; }
        }



        public override string ToString()
        {
            return string.Format(" ID: {0} \n Prezime: {1} \n Ime: {2} \n DatumRodjenja: {3} \n Adresa stanovanja: {4} \n Telefon: {5} \n Email: {6} \n Adresa Kancelarije: {7} \n BrojLicneKarte: {8} \n Zvanje: {9} \n GodineStaza: {10} \n",
                                   PID, PPrezime, PIme, PDatumRodjenja, PadresastanovanjaID, PTelefon, PEmail, PadresakancelarijeID, PBroj_licne, PZvanje, PStaz);
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
            PID.ToString(),
            PBroj_licne,
            PIme,
            PPrezime,
            PDatumRodjenja.ToString(),
            PadresaStanovanjaID.ToString(),
            PTelefon,
            PEmail,
            PadresakancelarijeID.ToString(),
            PZvanje,
            PStaz.ToString(),
            IDKatedre.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            PID = int.Parse(values[0]);
            PBroj_licne = values[1];
            PIme = values[2];
            PPrezime = values[3];
            PDatumRodjenja = DateTime.Parse(values[4]);
            PadresaStanovanjaID = int.Parse(values[5]);
            PTelefon = values[6];
            PEmail = values[7];
            PadresakancelarijeID = int.Parse(values[8]);
            PZvanje = values[9];
            PStaz = int.Parse(values[10]);
            IDKatedre = int.Parse(values[11]);

        }
    }
}
