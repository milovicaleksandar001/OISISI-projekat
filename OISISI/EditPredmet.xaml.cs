using Projekat_group5_team8.Model;
using ProjekatV2.Controller;
using ProjekatV2.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ProjekatV2
{
    /// <summary>
    /// Interaction logic for EditPredmet.xaml
    /// </summary>
    public partial class EditPredmet : Window, IObserver
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<enStatusPredmet> Semestar { get; set; }
        public enStatusPredmet SelectedSemestar { get; set; }
        public ObservableCollection<int> GodStudija { get; set; }
        public int SelectedGodStudija { get; set; }

        private PredmetController _predmetcontroller;
        public bool disable { get; set; }
        public bool enable{ get; set; }        
        
        public Profesor profesor { get; set; }

        public ProfesorSpojPredmet profSpojPredmet { get; set; }
        public EditPredmet(Predmet selecetedPredmet)
        {
            InitializeComponent();

            this.DataContext = this;

            var app = Application.Current as App;
            _predmetcontroller = app.PredmetController;
            _predmetcontroller.Subscribe(this);

            SelectedPredmet = selecetedPredmet;

            enable = selecetedPredmet.predmetniProfesor != null;
            disable = !enable;

            GodStudija = new ObservableCollection<int>();
            GodStudija.Add(1);
            GodStudija.Add(2);
            GodStudija.Add(3);
            GodStudija.Add(4);


            var semestarstatus = Enum.GetValues(typeof(enStatusPredmet)).Cast<enStatusPredmet>();
            Semestar = new ObservableCollection<enStatusPredmet>(semestarstatus);

            Sifra = selecetedPredmet.sifra;
            Naziv = selecetedPredmet.naziv;
            ESPBodovi = selecetedPredmet.esp;
            SelectedGodStudija = selecetedPredmet.godinastudija;
            SelectedSemestar = selecetedPredmet.semestar;
            SelectedPredmet = selecetedPredmet;

        }

        public Predmet selectedPredmet;
        public Predmet SelectedPredmet
        {
            get => selectedPredmet;
            set
            {
                selectedPredmet = value;
                OnPropertyChanged();
            }
        }

        private string sifra;
        public string Sifra
        {
            get => sifra;
            set
            {
                if (sifra != value)
                {
                    sifra = value;
                    OnPropertyChanged();
                }
            }
        }

        private string naziv;
        public string Naziv
        {
            get => naziv;
            set
            {
                if (naziv != value)
                {
                    naziv = value;
                    OnPropertyChanged();
                }
            }
        }

        private int godina;
        public int Godina
        {
            get => godina;
            set
            {
                if (godina != value)
                {
                    godina = value;
                    OnPropertyChanged();
                }
            }
        }

        private int ESPbodovi;
        public int ESPBodovi
        {
            get => ESPbodovi;
            set
            {
                if (ESPbodovi != value)
                {
                    ESPbodovi = value;
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
            SelectedPredmet.godinastudija = SelectedGodStudija;
            SelectedPredmet.esp = ESPBodovi;
            SelectedPredmet.sifra = Sifra;
            SelectedPredmet.naziv = Naziv;
            SelectedPredmet.semestar = SelectedSemestar;
            _predmetcontroller.Update(SelectedPredmet);

            this.Close();
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_DodajProf(object sender, RoutedEventArgs e)
        {

            ProfesorSpojPredmet spoj = new ProfesorSpojPredmet(SelectedPredmet);
            spoj.Show();    
                
        }

        private void por()
        {
            MessageBox.Show(profSpojPredmet.SelectedProfesor.ToString());
        }

        private void Button_Click_ObrisiProf(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = Potvrda();

            if (r == MessageBoxResult.Yes)
            {
                TBlockPIme.Text = "";
                SelectedPredmet.IdProfesor = -1;
                SelectedPredmet.predmetniProfesor = null;
                _predmetcontroller.Update(SelectedPredmet);
                
                

                enable = SelectedPredmet.predmetniProfesor != null;
                disable = !enable;
                OnPropertyChanged("disable");
                OnPropertyChanged("enable");                
            }
            else
            {
                return;
            }

        }

        private MessageBoxResult Potvrda()
        {
            string sMessageBoxText = $"Da li ste sigurni?";
            string sCaption = "Ukloni Profesora";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            return result;
        }

        public void Update()
        {
            if(SelectedPredmet.predmetniProfesor != null)
            {
                TBlockPIme.Text = SelectedPredmet.predmetniProfesor.Pime + " " + SelectedPredmet.predmetniProfesor.Pprezime; 
            }
        }
    }
}
