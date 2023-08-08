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
    public partial class PredmetSpojProfesor : Window
    {
        public Profesor profesor { get; set; }
        public ObservableCollection<Predmet> Predmeti { get; set; }
        public Predmet OdabraniPredmet { get; set; }
        public PredmetController ControllerPredmeta;
        public ProfesorController ControllerProfesora;
        public event PropertyChangedEventHandler PropertyChanged;

        public PredmetSpojProfesor(Profesor profesor1)
        {
            InitializeComponent();

            profesor = profesor1;
            var app = Application.Current as App;

            this.DataContext = this;
            ControllerPredmeta = app.PredmetController;
            ControllerProfesora = app.ProfesorController;

            List<Predmet> predmetiLista = ControllerPredmeta.SviPredmetiProfesora(profesor);
            Predmeti = new ObservableCollection<Predmet>(predmetiLista);
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            profesor.predmeti.Add(OdabraniPredmet);
            OdabraniPredmet.predmetniProfesor = profesor;
            OdabraniPredmet.IdProfesor = profesor.Pid;
            ControllerPredmeta.Update(OdabraniPredmet);
            this.Close();

        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
