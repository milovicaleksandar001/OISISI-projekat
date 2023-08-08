using ConsoleApp.Serialization;
using ProjekatV2.Model.KonverzijaDatuma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;


public enum enStatus // mozda van namespacea napisati PROBAJ !
{
    B,
    S
};

namespace Projekat_group5_team8.Model
{
    public class Student : Serializable
    {

        private int ID;
        private string Prezime;
        private string Ime;
        private DateTime DatumRodjenja;
        private int idAdrese;
        private Adresa Adresa;
        private string Telefon;
        private string Email;
        private string BrojIndeksa;
        private int GodinaUpisa;
        private int GodinaStudija;
        private enStatus Status;
        private double Prosek;
        private List<Ocena> OcenaNaIspitu;
        private List<Predmet> PolozeniIspiti;
        private List<Predmet> NePolozeniIspiti;


        public Student(int id, string prezime, string ime, DateTime datumrodjenja, int idAdrese, Adresa adresa, string telefon, string email, string brojindeksa, int godinaupisa, int godinastudija, enStatus status, float prosek)
        {
            this.ID = id;
            this.Prezime = prezime;
            this.Ime = ime;
            this.DatumRodjenja = datumrodjenja;
            this.idAdrese = idAdrese;
            this.Adresa = adresa;
            this.Telefon = telefon;
            this.Email = email;
            this.BrojIndeksa = brojindeksa;
            this.GodinaUpisa = godinaupisa;
            this.GodinaStudija = godinastudija;
            this.Status = status;
            this.Prosek = prosek;
            OcenaNaIspitu = new List<Ocena>();
            PolozeniIspiti = new List<Predmet>();
            NePolozeniIspiti = new List<Predmet>();
        }


        public Student()
        {
            OcenaNaIspitu = new List<Ocena>();
            NePolozeniIspiti = new List<Predmet>();
        }

        public List<Ocena> ocenaNaIspitu
        {
            get { return OcenaNaIspitu; }
            set { OcenaNaIspitu = value; }
        }

        public List<Predmet> nePolozeniIspiti
        {
            get { return NePolozeniIspiti; }
            set { NePolozeniIspiti = value; }
        }

        public List<Predmet> polozeniIspiti
        {
            get { return PolozeniIspiti; }
            set { PolozeniIspiti = value; }
        }

        public int id
        {
            get { return ID; }
            set { ID = value; }
        }

        public string prezime
        {
            get { return Prezime; }
            set { Prezime = value; }
        }

        public string ime
        {
            get { return Ime; }
            set { Ime = value; }
        }

        public DateTime datumRodjenja
        {
            get { return DatumRodjenja; }
            set { DatumRodjenja = value; }
        }
        public int IdAdresa
        {
            get { return idAdrese; }
            set { idAdrese = value; }
        }

        public Adresa adresa
        {
            get { return Adresa; }
            set
            {
                Adresa = value;
            }
        }

        public string telefon
        {
            get { return Telefon; }
            set
            {
                Telefon = value;
            }
        }

        public string email
        {
            get { return Email; }
            set
            {
                Email = value;
            }
        }

        public string brojIndeksa
        {
            get { return BrojIndeksa; }
            set { BrojIndeksa = value; }
        }

        public int godinaUpisa
        {
            get { return GodinaUpisa; }
            set { GodinaUpisa = value; }
        }

        public int godinaStudija
        {
            get { return GodinaStudija; }
            set { GodinaStudija = value; }
        }

        public enStatus status
        {
            get { return Status; }
            set { Status = value; }
        }

        public double prosek
        {
            get { return Prosek; }
            set { Prosek = value; }
        }






        //---------------------------------VALIDACIJA---------------------------------------------
/*
        private Regex _IndexRegex = new Regex("^(?:[012]?[0-9]|3[01])[..-](?:0?[1-9]|1[0-2])[..-](?:[0-9]{2}[..-]){1,2}$");

        public string this[string columnName]
        {
            get
            {
                
                if(columnName=="Datum")
                {
                    Match match = _IndexRegex.Match(DatumKonv.DateToString(datumRodjenja));
                    if(!match.Success)
                    {
                        return "Datum je u formatu: DD.MM.YYYY.";
                    }
                }

                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Datum" };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        */








                /*  public override string ToString()
                  {
                      return $"{Ime} {Prezime} {brojIndeksa} {adresa.Adrzava} {adresa.Aulica} {adresa.Abroj} {adresa.Agrad} {GodinaStudija} {datumRodjenja} {telefon} {email} {status}";
                  }

                  public string Error => null;

                  public string this[string columnName]
                  {
                      get
                      {
                          if (columnName == "Ime")
                          {
                              if (string.IsNullOrEmpty(Ime))
                                  return "Potrebno je uneti 'IME'!";
                          }
                          else if (columnName == "Prezime")
                          {
                              if (string.IsNullOrEmpty(Prezime))
                                  return "Potrebno je uneti 'PREZIME'!";
                          }
                          else if (columnName == "Datum")
                          {
                              if (string.IsNullOrEmpty(DatumKonv.DateToString(datumRodjenja)))
                                  return "Potrebno je uneti 'DATUM RODJENJA'!";
                          }

                          else if (columnName == "Ulica")
                          {
                              if (string.IsNullOrEmpty(adresa.Aulica))
                                  return "Potrebno je uneti 'ULICU'!";
                          }

                          else if (columnName == "Broj")
                          {
                              if (string.IsNullOrEmpty(adresa.Abroj.ToString()))
                                  return "Potrebno je uneti 'BROJ'!";
                          }
                          else if (columnName == "Grad")
                          {
                              if (string.IsNullOrEmpty(adresa.Agrad))
                                  return "Potrebno je uneti 'GRAD'!";
                          }
                          else if (columnName == "Drzava")
                          {
                              if (string.IsNullOrEmpty(adresa.Adrzava))
                                  return "Potrebno je uneti 'DRZAVU'!";
                          }
                          else if (columnName == "BrojTelefona")
                          {
                              if (string.IsNullOrEmpty(telefon))
                                  return "Potrebno je uneti 'BROJ TELEFONA'!";
                          }

                          else if (columnName == "Email")
                          {
                              if (string.IsNullOrEmpty(email))
                                  return "Potrebno je uneti 'EMAIL'!";
                          }
                          else if (columnName == "BrojIndeksa")
                          {
                              if (string.IsNullOrEmpty(BrojIndeksa))
                                  return "Potrebno je uneti 'BROJ INDEKSA'!";
                          }
                          else if (columnName == "GodinaUpisa")
                          {
                              if (string.IsNullOrEmpty(godinaUpisa.ToString()))
                                  return "Potrebno je uneti 'GODINU UPISA'!";
                          }
                          else if (columnName == "izabranaGodinaStudija")
                          {
                              if (string.IsNullOrEmpty(GodinaStudija.ToString()))
                                  return "Potrebno je uneti 'GODINU STUDIJA'!";
                          }


                          else if (columnName == "izabraniStatus")
                          {
                              if (string.IsNullOrEmpty(status.ToString()))
                                  return "Potrebno je uneti 'STATUS'!";
                          }


                          return null;

                      }
                  }

                  private readonly string[] _validatedProperties = { "Ime", "Prezime", "Datum", "Ulica", "Broj", "Grad", "Drzava", "BrojTelefona",  "Email","brojIndeksa", "GodinaUpisa", "izabranaGodinaStudija",  "izabraniStatus" };

                  public bool IsValid
                  {
                      get
                      {
                          foreach (var property in _validatedProperties)
                          {
                              if (this[property] != null)
                                  return false;
                          }

                          return true;
                      }
                  }*/

            //-----------------------------------------------------------------------------------------------------------
                public string[] ToCSV()
        {
            string[] csvValues =
            {
            ID.ToString(),
            BrojIndeksa,
            Ime,
            Prezime,
            DatumKonv.DateToString(datumRodjenja),
            idAdrese.ToString(),
            Telefon,
            Email,
            GodinaUpisa.ToString(),
            GodinaStudija.ToString(),
            Status.ToString(),
            Prosek.ToString(),
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            BrojIndeksa = values[1];
            Ime = values[2];
            Prezime = values[3];
            DatumRodjenja = DatumKonv.StringToDate(values[4]);
            idAdrese = int.Parse(values[5]);
            Telefon = values[6];
            Email = values[7];
            GodinaUpisa = int.Parse(values[8]);
            GodinaStudija = int.Parse(values[9]);
            if (values[10] == "B")
            {
                Status = enStatus.B;
            }
            else if (values[10] == "S")
            {
                Status = enStatus.S;
            }

            Prosek = Double.Parse(values[11]);

        }


    }

}



