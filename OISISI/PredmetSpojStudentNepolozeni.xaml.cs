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
using ProjekatV2.Storage;
using Projekat_group5_team8.Model;
using ConsoleApp.Serialization;
using ProjekatV2.Controller;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ProjekatV2
{

    public partial class PredmetSpojStudent : Window
    {
       
        public Student student { get; set; }
        public ObservableCollection<Predmet> Predmeti { get; set; }
        public Predmet OdabraniPredmet { get; set; }
        public PredmetController ControllerPredmeta;
        public StudentController ControllerStudenta;
        public event PropertyChangedEventHandler PropertyChanged;


        public PredmetSpojStudent(Student student1)
        {
            InitializeComponent();

            student = student1;
            var app = Application.Current as App;

            this.DataContext = this;
            ControllerPredmeta = app.PredmetController;
            ControllerStudenta = app.StudentController;

            List<Predmet> predmetiLista = ControllerPredmeta.SviPredmetiStudentaNepolozeni(student);
            Predmeti = new ObservableCollection<Predmet>(predmetiLista);

        }


        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            

                student.nePolozeniIspiti.Add(OdabraniPredmet);
                OdabraniPredmet.paliPredmet.Add(student);
                //dodati ako nesto bude trebalo
                ControllerPredmeta.Update(OdabraniPredmet);
                ControllerStudenta.Update(student);
                this.Close();
            
   
        }

      
        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
