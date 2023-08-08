using Projekat_group5_team8.Model;
using ProjekatV2.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ProjekatV2
{
    /// <summary>
    /// Interaction logic for PredmetSpojStudentPolozeni.xaml
    /// </summary>
    public partial class PredmetSpojStudentPolozeni : Window
    {
        public Student student { get; set; }
        public ObservableCollection<Predmet> Predmeti { get; set; }
        public Predmet OdabraniPredmet { get; set; }
        public PredmetController ControllerPredmeta;
        public StudentController ControllerStudenta;
        public event PropertyChangedEventHandler PropertyChanged;

        public PredmetSpojStudentPolozeni(Student student1)
        {
            InitializeComponent();

            student = student1;
            var app = Application.Current as App;

            this.DataContext = this;
            ControllerPredmeta = app.PredmetController;
            ControllerStudenta = app.StudentController;

            //List<Predmet> predmetiLista = ControllerPredmeta.SviPredmetiStudenta(student);
            //Predmeti = new ObservableCollection<Predmet>(predmetiLista);
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {

        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
