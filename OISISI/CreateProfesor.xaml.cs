using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Projekat_group5_team8.Model;
using ProjekatV2.Controller;
using ProjekatV2.Model.KonverzijaDatuma;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ProjekatV2
{
    /// <summary>
    /// Interaction logic for CreateProfesor.xaml
    /// </summary>
    public partial class CreateProfesor : Window
    {
        public ProfesorController _profesorController;
        public AdresaController _adresaProfesorController { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public CreateProfesor()
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            _profesorController = app.ProfesorController;
            _adresaProfesorController = app.AdresaController;
        }

        private string _Pprezime;

        public string PPrezime
        {
            get => _Pprezime;
            set
            {
                if (value != _Pprezime)
                {
                    _Pprezime = value;
                    OnPropertyChanged("PPrezime");
                }
            }
        }

        private string _Pime;

        public string PIme
        {
            get => _Pime;
            set
            {
                if (value != _Pime)
                {
                    _Pime = value;
                    OnPropertyChanged("PIme");
                }
            }
        }

        private string _PdatumRodjenja;
        public string PDatumRodjenja
        {
            get => _PdatumRodjenja;
            set
            {
                if (value != _PdatumRodjenja)
                {
                    _PdatumRodjenja = value;
                    OnPropertyChanged("PDatumRodjenja");
                }
            }
        }

        private string _PulicaStanovanja;
        public string PUlicaStanovanja
        {
            get => _PulicaStanovanja;
            set
            {
                if (value != _PulicaStanovanja)
                {
                    _PulicaStanovanja = value;
                    OnPropertyChanged("PUlicaStanovanja");
                }
            }
        }

        private int _PbrojStanovanja;
        public int PBrojStanovanja
        {
            get => _PbrojStanovanja;
            set
            {
                if (_PbrojStanovanja != value)
                {
                    _PbrojStanovanja = value;
                    OnPropertyChanged("PBrojStanovanja");
                }
            }
        }



        private string _PgradStanovanja;
        public string PGradStanovanja
        {
            get => _PgradStanovanja;
            set
            {
                if (_PgradStanovanja != value)
                {
                    _PgradStanovanja = value;
                    OnPropertyChanged("PGradStanovanja");
                }
            }
        }

        private string _PdrzavaStanovanja;
        public string PDrzavaStanovanja
        {
            get => _PdrzavaStanovanja;
            set
            {
                if (_PdrzavaStanovanja != value)
                {
                    _PdrzavaStanovanja = value;
                    OnPropertyChanged("PDrzavaStanovanja");
                }
            }
        }

        private string _Ptelefon;
        public string PTelefon
        {
            get => _Ptelefon;
            set
            {
                if (value != _Ptelefon)
                {
                    _Ptelefon = value;
                    OnPropertyChanged("PTelefon");
                }
            }
        }



        private string _Pemail;
        public string PEmail
        {
            get => _Pemail;
            set
            {
                if (value != _Pemail)
                {
                    _Pemail = value;
                    OnPropertyChanged("PEmail");
                }
            }
        }


        private string _PulicaKancelarije;
        public string PUlicaKancelarije
        {
            get => _PulicaKancelarije;
            set
            {
                if (value != _PulicaKancelarije)
                {
                    _PulicaKancelarije = value;
                    OnPropertyChanged("PUlicaKancelarije");
                }
            }
        }

        private int _PbrojKancelarije;
        public int PBrojKancelarije
        {
            get => _PbrojKancelarije;
            set
            {
                if (_PbrojKancelarije != value)
                {
                    _PbrojKancelarije = value;
                    OnPropertyChanged("PBrojKancelarije");
                }
            }
        }

        private string _PgradKancelarije;
        public string PGradKancelarije
        {
            get => _PgradKancelarije;
            set
            {
                if (_PgradKancelarije != value)
                {
                    _PgradKancelarije = value;
                    OnPropertyChanged("PGradKancelarije");
                }
            }
        }

        private string _PdrzavaKancelarije;
        public string PDrzavaKancelarije
        {
            get => _PdrzavaKancelarije;
            set
            {
                if (_PdrzavaKancelarije != value)
                {
                    _PdrzavaKancelarije = value;
                    OnPropertyChanged("PDrzavaKancelarije");
                }
            }
        }

        private string _Pbroj_licne;

        public string PBroj_licne
        {
            get => _Pbroj_licne;
            set
            {
                if (value != _Pbroj_licne)
                {
                    _Pbroj_licne = value;
                    OnPropertyChanged("PBroj_licne");
                }
            }
        }

        private string _Pzvanje;

        public string PZvanje
        {
            get => _Pzvanje;
            set
            {
                if (value != _Pzvanje)
                {
                    _Pzvanje = value;
                    OnPropertyChanged("PZvanje");
                }
            }
        }

        private int _Pstaz;

        public int PStaz
        {
            get => _Pstaz;
            set
            {
                if (value != _Pstaz)
                {
                    _Pstaz = value;
                    OnPropertyChanged("PStaz");
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            Profesor profesor = new Profesor();
            profesor.Pprezime = _Pprezime;
            profesor.Pime = _Pime;
            profesor.PdatumRodjenja = DatumKonv.StringToDate(_PdatumRodjenja);
            Adresa adresaStanovanja = new Adresa
            {
                Aulica = _PulicaStanovanja,
                Abroj = _PbrojStanovanja,
                Agrad = _PgradStanovanja,
                Adrzava = _PdrzavaStanovanja
            };
            _adresaProfesorController.Create(adresaStanovanja);
            profesor.PadresastanovanjaID = adresaStanovanja.Aid;
            profesor.Padresastanovanja = adresaStanovanja;
            profesor.Ptelefon = _Ptelefon;
            profesor.Pemail = _Pemail;
            Adresa adresaKancelarije = new Adresa
            {
                Aulica = _PulicaKancelarije,
                Abroj = _PbrojKancelarije,
                Agrad = _PgradKancelarije,
                Adrzava = _PdrzavaKancelarije
            };
            _adresaProfesorController.Create(adresaKancelarije);
            profesor.PadresakancelarijeID = adresaKancelarije.Aid;
            profesor.Padresakancelarije = adresaKancelarije;
            profesor.Pbroj_licne = _Pbroj_licne;
            profesor.Pzvanje = _Pzvanje;
            profesor.Pstaz = _Pstaz;


            string pattern = @"^(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.](19|20)\d\d[.]$";

            if (!Regex.IsMatch(DatumKonv.DateToString(profesor.PdatumRodjenja), pattern))
            {
                MessageBox.Show("'DATUM RODJENJA' mora biti u formatu 'dd.mm.yyyy'");
                return;
            }


            if (profesor.Pprezime == null)
            {
                MessageBox.Show("'PREZIME' nije uneto");
            }
            else if (profesor.Pime == null)
            {
                MessageBox.Show("'IME' nije uneto");
            }
            else if (string.IsNullOrEmpty(DatumKonv.DateToString(profesor.PdatumRodjenja)))
            {
                MessageBox.Show("'DATUM RODJENJA' nije unet");
                // return;
            }


            //------------------------------------------------------
            else if (profesor.Padresastanovanja.Aulica == null)
            {
                MessageBox.Show("'ULICA STANOVANJA' nije uneta");
            }
            else if (profesor.Padresastanovanja.Abroj <= 0)
            {
                MessageBox.Show("'BROJ ULICE STANOVANJA' je pozitivan broj >0");
            }
            else if (profesor.Padresastanovanja.Agrad == null)
            {
                MessageBox.Show("'GRAD STANOVANJA' nije unet");
            }
            else if (profesor.Padresastanovanja.Adrzava == null)
            {
                MessageBox.Show("'DRZAVA STANOVANJA' nije uneta");
            }
            //------------------------------------------------------
            else if (profesor.Ptelefon == null)
            {
                MessageBox.Show("'BROJ TELEFONA' nije unet");
            }
            else if (profesor.Pemail == null)
            {
                MessageBox.Show("'EMAIL' nije unet");
            }
            //------------------------------------------------------
            else if (profesor.Padresakancelarije.Aulica == null)
            {
                MessageBox.Show("'ULICA KANCELARIJE' nije uneta");
            }
            else if (profesor.Padresakancelarije.Abroj <= 0)
            {
                MessageBox.Show("'BROJ ULICE KANCELARIJE' je pozitivan broj >0");
            }
            else if (profesor.Padresakancelarije.Agrad == null)
            {
                MessageBox.Show("'GRAD KANCELARIJE' nije unet");
            }
            else if (profesor.Padresakancelarije.Adrzava == null)
            {
                MessageBox.Show("'DRZAVA KANCELARIJE' nije uneta");
            }
            //------------------------------------------------------
            else if(profesor.Pbroj_licne == null)
            {
                MessageBox.Show("'BROJ LICNE KARTE' nije unet");
            }
            else if (profesor.Pzvanje == null)
            {
                MessageBox.Show("'ZVANJE PROFESORA' nije uneto");
            }
            else if (profesor.Pstaz <= 0)
            {
                MessageBox.Show("'STAZ' moze biti samo pozitivan broj");
            }


            else
            {
                _profesorController.Create(profesor);
                this.Close();
            }

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("'DATUM RODJENJA' nije unet");
                return;
            }

            string pattern = @"^(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.](19|20)\d\d[.]$";
            if (!Regex.IsMatch(textBox.Text, pattern))
            {
                MessageBox.Show("'DATUM RODJENJA' mora biti u formatu 'dd.mm.yyyy'");
            }
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
