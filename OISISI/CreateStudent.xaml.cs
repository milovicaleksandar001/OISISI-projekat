using Projekat_group5_team8.Model;
using ProjekatV2.Controller;
using ProjekatV2.Model.KonverzijaDatuma;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
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
    /// Interaction logic for CreateStudent.xaml
    /// </summary>
    public partial class CreateStudent : Window
    {
        public StudentController _studentController;
        public AdresaController _adresaController { get; set; }

        public int izabranaGodinaStudija { get; set; }

        public enStatus izabraniStatus { get; set; }

        public ObservableCollection<enStatus> StudentStatus { get; set; }
        public ObservableCollection<int> GodinaStudiranjaOC { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CreateStudent()
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;

            _studentController = app.StudentController;
            _adresaController = app.AdresaController;
            var statusi = Enum.GetValues(typeof(enStatus)).Cast<enStatus>();
            StudentStatus = new ObservableCollection<enStatus>(statusi);

            GodinaStudiranjaOC = new ObservableCollection<int>();
            GodinaStudiranjaOC.Add(1);
            GodinaStudiranjaOC.Add(2);
            GodinaStudiranjaOC.Add(3);
            GodinaStudiranjaOC.Add(4);

        }
        private string _index;
        public string Indeks
        {
            get => _index;
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ime;

        public string Ime
        {
            get => _ime;
            set
            {
                if (value != _ime)
                {
                    _ime = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _prezime;

        public string Prezime
        {
            get => _prezime;
            set
            {
                if (value != _prezime)
                {
                    _prezime = value;
                    OnPropertyChanged();
                }
            }
        }



        private string grad;
        public string Grad
        {
            get => grad;
            set
            {
                if (grad != value)
                {
                    grad = value;
                    OnPropertyChanged("Grad");
                }
            }
        }

        private string drzava;
        public string Drzava
        {
            get => drzava;
            set
            {
                if (drzava != value)
                {
                    drzava = value;
                    OnPropertyChanged("Drzava");
                }
            }
        }

        private string ulica;
        public string Ulica
        {
            get => ulica;
            set
            {
                if (ulica != value)
                {
                    ulica = value;
                    OnPropertyChanged("Ulica");
                }
            }
        }

        private int broj;
        public int Broj
        {
            get => broj;
            set
            {
                if (broj != value)
                {
                    broj = value;
                    OnPropertyChanged("Broj");
                }
            }
        }

        private string _datum;

        public string Datum
        {
            get => _datum;
            set
            {
                if (value != _datum)
                {
                    _datum = value;
                    OnPropertyChanged("Datum rodjenja");
                }
            }
        }

        private string _brojtelefona;

        public string BrojTelefona
        {
            get => _brojtelefona;
            set
            {
                if (value != _brojtelefona)
                {
                    _brojtelefona = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                if (value != _email)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _brojindeksa;

        public string BrojIndeksa
        {
            get => _brojindeksa;
            set
            {
                if (value != _brojindeksa)
                {
                    _brojindeksa = value;
                    OnPropertyChanged();
                }
            }
        }

        private int godinaUpisa;
        public int GodinaUpisa
        {
            get => godinaUpisa;
            set
            {
                if (value != godinaUpisa)
                {
                    godinaUpisa = value;
                    OnPropertyChanged("Godina upisa");
                }
            }
        }

        private string _godinastudija;

        public string GodinaStudija
        {
            get => _godinastudija;
            set
            {
                if (value != _godinastudija)
                {
                    _godinastudija = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _status;

        public string Status
        {
            get => _status;
            set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            Student student = new Student();
            student.ime = Ime;
            student.prezime = Prezime;
            student.brojIndeksa = BrojIndeksa;
            Adresa adresa = new Adresa
            {
                Adrzava = Drzava,
                Aulica = Ulica,
                Abroj = Broj,
                Agrad = Grad
            };
            _adresaController.Create(adresa);
            student.IdAdresa = adresa.Aid;
            student.adresa = adresa;
            student.godinaUpisa = GodinaUpisa;
            student.godinaStudija = izabranaGodinaStudija;
            student.datumRodjenja = DatumKonv.StringToDate(Datum);
            student.telefon = BrojTelefona;
            student.email = Email;
            student.status = izabraniStatus;



           /* if (string.IsNullOrEmpty(DatumKonv.DateToString(student.datumRodjenja)))
            {
                MessageBox.Show("'DATUM RODJENJA' nije unet");
                return;

            }*/

            string pattern = @"^(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.](19|20)\d\d[.]$";

          if (!Regex.IsMatch(DatumKonv.DateToString(student.datumRodjenja), pattern))
            {
                MessageBox.Show("'DATUM RODJENJA' mora biti u formatu 'dd.mm.yyyy'");
                //return;
            }

            if (student.ime == null)
            {
                MessageBox.Show("'IME' nije uneto");
            }
            else if (student.prezime == null)
            {
                MessageBox.Show("'PREZIME' nije uneto");
            }
            else if (string.IsNullOrEmpty(DatumKonv.DateToString(student.datumRodjenja)))
            {
                MessageBox.Show("'DATUM RODJENJA' nije unet");
               // return;
            }
            else if (student.adresa.Aulica == null)
            {
                MessageBox.Show("'ULICA' nije uneta");
            }
            else if (student.adresa.Abroj <= 0)
            {
                MessageBox.Show("'BROJ ULICE' je pozitivan broj >0");
            }
            else if (student.adresa.Agrad == null)
            {
                MessageBox.Show("'GRAD' nije unet");
            }
            else if (student.adresa.Adrzava == null)
            {
                MessageBox.Show("'DRZAVA' nije uneta");
            }
            else if (student.telefon == null)
            {
                MessageBox.Show("'BROJ TELEFONA' nije unet");
            }
            else if (student.email == null)
            {
                MessageBox.Show("'EMAIL' nije unet");
            }
            else if (student.brojIndeksa == null)
            {
                MessageBox.Show("'BROJ INDEKSA' nije unet");
            }
            else if (student.godinaUpisa == 0)
            {
                MessageBox.Show("'GODINA UPISA' nije uneta");
            }
            else if (student.godinaStudija == 0)
            {
                MessageBox.Show("'GODINA STUDIJA' nije uneta");
            }

            /*  else if ((student.status != enStatus.B) || (student.status != enStatus.S))
               {
                   MessageBox.Show("'STATUS' sme da bude samo 'B' ili 'S'");
               }*/
            else if (student.godinaUpisa <= 1970)
            {
                MessageBox.Show("'GODINA UPISA' je nemoguca");
            }

            else
            {
                _studentController.Create(student);
                this.Close();
            }
                       
        }

      

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if(string.IsNullOrEmpty(textBox.Text))
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

