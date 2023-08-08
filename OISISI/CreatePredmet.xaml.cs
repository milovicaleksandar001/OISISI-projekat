using Projekat_group5_team8.Model;
using ProjekatV2.Controller;
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
    /// Interaction logic for CreatePredmet.xaml
    /// </summary>
    public partial class CreatePredmet : Window
    {
        public ObservableCollection<enStatusPredmet> Semestar { get; set; }
        public enStatusPredmet SelectedSemestar { get; set; }
        public ObservableCollection<int> GodStudija { get; set; }
        public int SelectedGodStudija { get; set; }

        private PredmetController _predmetcontroller;

        public event PropertyChangedEventHandler PropertyChanged;

        public CreatePredmet()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            
            _predmetcontroller = app.PredmetController;

            GodStudija = new ObservableCollection<int>();
            GodStudija.Add(1);
            GodStudija.Add(2);
            GodStudija.Add(3);
            GodStudija.Add(4);

            var semestarstatus = Enum.GetValues(typeof(enStatusPredmet)).Cast<enStatusPredmet>();
            Semestar = new ObservableCollection<enStatusPredmet>(semestarstatus);
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
                Predmet predmet = new Predmet();
            predmet.godinastudija = Godina;
            predmet.esp = ESPBodovi;
            predmet.sifra = Sifra;
            predmet.naziv= Naziv;
            predmet.semestar = SelectedSemestar;


            if (predmet.sifra == null)
            {
                MessageBox.Show("'SIFRA PREDMETA' nije uneta");           
            }
                else if (predmet.naziv == null)
                {
                MessageBox.Show("'NAZIV PREDMETA' nije unet");
                }
                else if (predmet.godinastudija == 0)
                {
                    MessageBox.Show("'GODINA STUDIJA' nije unet");
                }
                /*else if (predmet.semestar == -1)
                {
                    MessageBox.Show("'SEMESTAR' nije unet");
                }*/
                else if (predmet.esp <= 0)
                {
                    MessageBox.Show("'ESPB' moraju biti >0");
                }
            else {
                _predmetcontroller.Create(predmet);
                this.Close();
            }
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
