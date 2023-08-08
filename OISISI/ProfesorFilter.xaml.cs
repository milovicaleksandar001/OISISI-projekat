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
    /// Interaction logic for ProfesorFilter.xaml
    /// </summary>
    public partial class ProfesorFilter : Window, IObserver, INotifyPropertyChanged
    {
        public Profesor SelectedProfesor { get; set; }
        public Katedra Katedra { get; set; }
        public ObservableCollection<Profesor> Profesors { get; set; }
        public ProfesorController _profesorController { get; set; }

        public KatedraController KatedraController;
        public ProfesorFilter(Katedra katedra)
        {
            InitializeComponent();
            var app = Application.Current as App;
            this.DataContext = this;

            Katedra = katedra;
            _profesorController = app.ProfesorController;
            KatedraController = app.KatedraController;
            _profesorController.Subscribe(this);
            KatedraController.Subscribe(this);

            if (_profesorController.GetFilterProfesori() != null)
            {
                Profesors = new ObservableCollection<Profesor>(_profesorController.GetFilterProfesori());
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedProfesor != null)
            {
                if (Katedra.sefKatedreID != null)
                {
                    Katedra.sefKatedreID = SelectedProfesor.Pid;
                    Katedra.sef = SelectedProfesor;
                    KatedraController.Update(Katedra);
                    OnPropertyChanged();
                }
                else
                {
                    MessageBoxResult result = ConfirmSef();

                    if (result == MessageBoxResult.Yes)
                    {
                        Katedra.sefKatedreID = SelectedProfesor.Pid;
                        Katedra.sef = SelectedProfesor;
                        KatedraController.Update(Katedra);
                        OnPropertyChanged();
                    }

                }

                _profesorController.Update(SelectedProfesor);
                this.Close();
            }
            else
            {
                MessageBox.Show("Odaberite profesora");
            }

        }

        private MessageBoxResult ConfirmSef()
        {
            string sMessageBoxText = $"Ova katedra vec ima sefa{Katedra.sef.Pime}{Katedra.sef.Pprezime}, da li zelite da dodate novog sefa?\n";
            string sCaption = "Potvrda stavljanja sefa";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            return result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Update()
        { }
    }
}
