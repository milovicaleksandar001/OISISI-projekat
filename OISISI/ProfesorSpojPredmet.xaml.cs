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
    /// Interaction logic for ProfesorSpojPredmet.xaml
    /// </summary>
    public partial class ProfesorSpojPredmet : Window, IObserver, INotifyPropertyChanged
    {
        public Profesor SelectedProfesor { get; set; }
        public Predmet Predmet { get; set; }
        public ObservableCollection<Profesor> Profesors { get; set; }
        public ProfesorController _profesorController { get; set; }
        public PredmetController _predmetController { get; set; }
        public ProfesorSpojPredmet(Predmet predmet)
        {
            InitializeComponent();
            var app = Application.Current as App;
            this.DataContext = this;

            Predmet = predmet;
            _profesorController =  app.ProfesorController;
            _predmetController = app.PredmetController;
            _predmetController.Subscribe(this);
            _profesorController.Subscribe(this);


            Profesors = new ObservableCollection<Profesor>(_profesorController.GetAllProfesors());
        
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                 this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if(SelectedProfesor != null)
            { 
                Predmet.IdProfesor=SelectedProfesor.Pid;
                Predmet.predmetniProfesor=SelectedProfesor;
                SelectedProfesor.predmeti.Add(Predmet);
                _predmetController.Update(Predmet);
                _profesorController.Update(SelectedProfesor);

                     this.Close();

            }

        }

        public void Update()
        {    }
    }
}
