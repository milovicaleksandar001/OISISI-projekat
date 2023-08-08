using Projekat_group5_team8.Model;
using ProjekatV2.Controller;
using ProjekatV2.Model.KonverzijaDatuma;
using ProjekatV2.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjekatV2
{
    /// <summary>
    /// Interaction logic for EditProfesor.xaml
    /// </summary>
    public partial class EditProfesor : Window, IObserver
    {
        public ProfesorController _profesorController;
        public AdresaController _adresaController { get; set; }
        public PredmetController _predmetController { get; set; }
        public Profesor SelectedProfesor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Predmet> Predmeti { get; set; }

        public Predmet SelectedPredmet { get; set; }


        public EditProfesor(Profesor selectedProfesor)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _adresaController = app.AdresaController;
            _profesorController = app.ProfesorController;
            _predmetController = app.PredmetController;
            _profesorController.Subscribe(this);
            _adresaController.Subscribe(this);
            app.PredmetController.Subscribe(this);

            SelectedProfesor = selectedProfesor;

            PIme = SelectedProfesor.Pime;
            PPrezime = SelectedProfesor.Pprezime;
            //--------------------------------------------------------------
            PUlicaStanovanja = SelectedProfesor.Padresastanovanja.Aulica;
            PBrojStanovanja = SelectedProfesor.Padresastanovanja.Abroj;
            PGradStanovanja = SelectedProfesor.Padresastanovanja.Agrad;
            PDrzavaStanovanja = SelectedProfesor.Padresastanovanja.Adrzava;
            //--------------------------------------------------------------
            PTelefon = SelectedProfesor.Ptelefon;
            //--------------------------------------------------------------
            PUlicaKancelarije = SelectedProfesor.Padresakancelarije.Aulica;
            PBrojKancelarije = SelectedProfesor.Padresakancelarije.Abroj;
            PGradKancelarije = SelectedProfesor.Padresakancelarije.Agrad;
            PDrzavaKancelarije = SelectedProfesor.Padresakancelarije.Adrzava;
            //--------------------------------------------------------------
            PEmail = SelectedProfesor.Pemail;
            PZvanje = SelectedProfesor.Pzvanje;
            PDatumRodjenja = DatumKonv.DateToString(SelectedProfesor.PdatumRodjenja);
            PBroj_licne = SelectedProfesor.Pbroj_licne;
            PStaz = SelectedProfesor.Pstaz;

            Predmeti = new ObservableCollection<Predmet>(selectedProfesor.predmeti);
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

            SelectedProfesor.Pime = PIme;
            SelectedProfesor.Pprezime = PPrezime;
            //--------------------------------------------------------------
            SelectedProfesor.Padresastanovanja.Adrzava = PDrzavaStanovanja;
            SelectedProfesor.Padresastanovanja.Aulica = PUlicaStanovanja;
            SelectedProfesor.Padresastanovanja.Abroj = PBrojStanovanja;
            SelectedProfesor.Padresastanovanja.Agrad = PGradStanovanja;
            //--------------------------------------------------------------
            _adresaController.Update(SelectedProfesor.Padresastanovanja);
            //--------------------------------------------------------------
            SelectedProfesor.Padresakancelarije.Adrzava = PDrzavaStanovanja;
            SelectedProfesor.Padresakancelarije.Aulica = PUlicaStanovanja;
            SelectedProfesor.Padresakancelarije.Abroj = PBrojStanovanja;
            SelectedProfesor.Padresakancelarije.Agrad = PGradStanovanja;
            //--------------------------------------------------------------
            _adresaController.Update(SelectedProfesor.Padresakancelarije);

            SelectedProfesor.PdatumRodjenja = DatumKonv.StringToDate(PDatumRodjenja);
            SelectedProfesor.Ptelefon = PTelefon;
            SelectedProfesor.Pemail = PEmail;
            SelectedProfesor.Pzvanje = PZvanje;
            SelectedProfesor.Pstaz = PStaz;
            SelectedProfesor.Pbroj_licne = PBroj_licne;

            if (string.IsNullOrEmpty(DatumKonv.DateToString(SelectedProfesor.PdatumRodjenja)))
            {
                MessageBox.Show("'DATUM RODJENJA' nije unet");
                return;
            }
           
            string pattern = @"^(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.](19|20)\d\d[.]$";

            if (!Regex.IsMatch(DatumKonv.DateToString(SelectedProfesor.PdatumRodjenja), pattern))
            {
                MessageBox.Show("'DATUM RODJENJA' mora biti u formatu 'dd.mm.yyyy'");
                return;
            }

            _profesorController.Update(SelectedProfesor);
            this.Close();

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

        private void DodajPredmet(object sender, RoutedEventArgs e)
        {
            PredmetSpojProfesor predmetSpojProfesor = new PredmetSpojProfesor(SelectedProfesor);
            predmetSpojProfesor.Show();
        }

        private MessageBoxResult ConfirmDeletion()
        {
            string sMessageBoxText = $"Da li ste sigurni?";
            string sCaption = "Ukloni predmet";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            return result;
        }
        private void ObrisiPredmet(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = ConfirmDeletion();

            if (r == MessageBoxResult.Yes)
            {
                if (SelectedPredmet != null)
                {
                    SelectedProfesor.predmeti.Remove(SelectedPredmet);
                    SelectedPredmet.predmetniProfesor = null;
                    SelectedPredmet.IdProfesor = -1;

                    _predmetController.Update(SelectedPredmet);

                }
            }
        }

        public void Update()
        {
            Predmeti.Clear();
            foreach(Predmet pr in SelectedProfesor.predmeti)
            {
                Predmeti.Add(pr);
            }
        }
    }
}
