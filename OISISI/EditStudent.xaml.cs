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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjekatV2
{
    /// <summary>
    /// Interaction logic for EditStudent.xaml
    /// </summary>
    public partial class EditStudent : Window, IObserver, INotifyPropertyChanged
    {

        public StudentController StudentController;
        public AdresaController AdresaController { get; set; }
        public PredmetController PredmetController { get; set; }

        public int izabranaGodinaStudija { get; set; }

        public enStatus izabraniStatus { get; set; }
        public OcenaController OcenaController { get; set; }

        public ObservableCollection<enStatus> StudentStatus { get; set; }
        public ObservableCollection<int> GodinaStudiranjaOC { get; set; }

        public Student SelectedStudent { get; set; }

        public ObservableCollection<Predmet> Predmeti { get; set; }

        public ObservableCollection<Ocena> Ocene { get; set; }
        public Predmet SelectedPredmetNepolozen { get; set; }
        public Ocena SelectedOcena { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int UkupnoESP { get; set; }

        public double Prosek { get; set; }
        public EditStudent(Student selectedStudent)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;

            StudentController = app.StudentController;
            AdresaController = app.AdresaController;
            
            var statusi = Enum.GetValues(typeof(enStatus)).Cast<enStatus>();
            StudentStatus = new ObservableCollection<enStatus>(statusi);
            StudentController.Subscribe(this);
            AdresaController.Subscribe(this);
            PredmetController = app.PredmetController;
            PredmetController.Subscribe(this);
            OcenaController = app.OcenaController;
            OcenaController.Subscribe(this);


            GodinaStudiranjaOC = new ObservableCollection<int>();
            GodinaStudiranjaOC.Add(1);
            GodinaStudiranjaOC.Add(2);
            GodinaStudiranjaOC.Add(3);
            GodinaStudiranjaOC.Add(4);
            

            SelectedStudent = selectedStudent;



            Ime = SelectedStudent.ime;
            Prezime = SelectedStudent.prezime;
            Ulica = SelectedStudent.adresa.Aulica;
            Broj = SelectedStudent.adresa.Abroj;
            Grad = SelectedStudent.adresa.Agrad;
            Drzava = SelectedStudent.adresa.Adrzava;
            BrojTelefona = SelectedStudent.telefon;
            Email = SelectedStudent.email;
            izabranaGodinaStudija = SelectedStudent.godinaStudija;
            godinaUpisa = SelectedStudent.godinaUpisa;
            izabraniStatus = SelectedStudent.status;
            Datum = DatumKonv.DateToString(SelectedStudent.datumRodjenja);
            BrojIndeksa = SelectedStudent.brojIndeksa;

            UkupnoESP = StudentController.UkupnoESP(SelectedStudent);
            SelectedStudent.prosek = StudentController.Prosek(SelectedStudent);
            Prosek = SelectedStudent.prosek;

            Predmeti = new ObservableCollection<Predmet>(selectedStudent.nePolozeniIspiti);
            Ocene = new ObservableCollection<Ocena>(selectedStudent.ocenaNaIspitu);
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
            SelectedStudent.ime = Ime;
            SelectedStudent.prezime = Prezime;
            SelectedStudent.brojIndeksa = BrojIndeksa;
            //-------------------------------------------------
            SelectedStudent.adresa.Adrzava = Drzava;
            SelectedStudent.adresa.Aulica = Ulica;
            SelectedStudent.adresa.Abroj = Broj;
            SelectedStudent.adresa.Agrad = Grad;
            AdresaController.Update(SelectedStudent.adresa);
            //-------------------------------------------------
            SelectedStudent.godinaUpisa = GodinaUpisa;
            SelectedStudent.godinaStudija = izabranaGodinaStudija;
            SelectedStudent.datumRodjenja = DatumKonv.StringToDate(Datum);
            SelectedStudent.telefon = BrojTelefona;
            SelectedStudent.email = Email;
            SelectedStudent.status = izabraniStatus;
            SelectedStudent.nePolozeniIspiti = new List<Predmet>();
            SelectedStudent.ocenaNaIspitu = new List<Ocena>();



            if (string.IsNullOrEmpty(DatumKonv.DateToString(SelectedStudent.datumRodjenja)))
            {
                MessageBox.Show("'DATUM RODJENJA' nije unet");
                return;

            }

            string pattern = @"^(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.](19|20)\d\d[.]$";

            if (!Regex.IsMatch(DatumKonv.DateToString(SelectedStudent.datumRodjenja), pattern))
            {
                MessageBox.Show("'DATUM RODJENJA' mora biti u formatu 'dd.mm.yyyy'");
                return;
            }

            StudentController.Update(SelectedStudent);
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

        private void PonistiOcenu(object sender, RoutedEventArgs e)
        {
            if (SelectedOcena != null)
            {
                SelectedStudent.ocenaNaIspitu.Remove(SelectedOcena);
                SelectedStudent.nePolozeniIspiti.Add(SelectedOcena.predmet);
                SelectedOcena.predmet.paliPredmet.Add(SelectedStudent);
                SelectedOcena.predmet.poloziliPredmet.RemoveAll(stud => stud.id == SelectedStudent.id);

                OcenaController.Delete(SelectedOcena);
                StudentController.Update(SelectedStudent);

            }

        }

        public void Update()
        {
            Predmeti.Clear();
            foreach(Predmet pr in SelectedStudent.nePolozeniIspiti) 
            {
                Predmeti.Add(pr);
            }

            Ocene.Clear();
            foreach(Ocena ocena in SelectedStudent.ocenaNaIspitu)
            {
                Ocene.Add(ocena);
            }
            Prosek= SelectedStudent.prosek;
            OnPropertyChanged("Prosek");
            UkupnoESP = StudentController.UkupnoESP(SelectedStudent);
            OnPropertyChanged("UkupnoESP");

        }

        private void DodajPredmetNepolozen(object sender, RoutedEventArgs e)
        {
            
            PredmetSpojStudent predmetSpojStudent = new PredmetSpojStudent(SelectedStudent);
            predmetSpojStudent.Show();
        }

        private void PolozioPredmet(object sender, RoutedEventArgs e)
        {
            if (SelectedPredmetNepolozen != null)
            {
                UnosOcene unosOcene = new UnosOcene(SelectedStudent, SelectedPredmetNepolozen);
                unosOcene.Show();

            }
        }

        private void ObrisiPredmetNepolozen(object sender, RoutedEventArgs e)
        {
            if (SelectedStudent != null)
            {
                SelectedStudent.nePolozeniIspiti.Remove(SelectedPredmetNepolozen);
                SelectedPredmetNepolozen.paliPredmet.Remove(SelectedStudent);

                PredmetController.Update(SelectedPredmetNepolozen);
                StudentController.Update(SelectedStudent);
            }
        }
    }
}
