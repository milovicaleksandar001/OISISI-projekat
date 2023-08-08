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
    /// Interaction logic for UnosOcene.xaml
    /// </summary>
    public partial class UnosOcene : Window, IObserver
    {

        public Student Student { get; set; } //SelectedStudent samo sam lose nazvao pa necu menjati da ne bih sve morao
        public Predmet Predmet { get; set; } //SelectedPredmet samo sam lose nazvao pa necu menjati da ne bih sve morao
        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<int> OcenaNaPredmetu { get; set; }
        public StudentController StudentController {get; set;}
        public OcenaController OcenaController { get; set; }
        public PredmetController PredmetController { get; set; }
        public int SelectedOcena { get; set; }



        public UnosOcene(Student student, Predmet predmet)
        {
            InitializeComponent();
            this.DataContext = this;
            Student = student;
            Predmet = predmet;

            Sifra = predmet.sifra;
            Naziv = predmet.naziv;

            var app = Application.Current as App;

            OcenaController = app.OcenaController;
            PredmetController = app.PredmetController;
            StudentController = app.StudentController;
            OcenaController.Subscribe(this);
            PredmetController.Subscribe(this);
            StudentController.Subscribe(this);

            OcenaNaPredmetu = new ObservableCollection<int>() {6, 7, 8, 9, 10};

        }

        private string datum;

        public string DatumPolaganja
        {
            get => datum;
            set
            {
                if (value != datum)
                {
                    datum = value;
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
            Ocena ocena = new Ocena();
            ocena.studentPolozio = Student;
            ocena.predmet = Predmet;
            ocena.studentID = Student.id;
            ocena.predmetID = Predmet.Pid;
            ocena.Brvrednostocene = SelectedOcena;
            ocena.Datumpolaganja = DatumKonv.StringToDate(DatumPolaganja);
            Student.nePolozeniIspiti.Remove(Predmet);
            OcenaController.Create(ocena);
            Student.ocenaNaIspitu.Add(ocena);
            StudentController.Update(Student);
            Predmet.poloziliPredmet.Add(Student);
            Predmet.paliPredmet.Remove(Student);
            PredmetController.Update(Predmet);
            this.Close();
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Update()
        {
          
        }
    }
}
