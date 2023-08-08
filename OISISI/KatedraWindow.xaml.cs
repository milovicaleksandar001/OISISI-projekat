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
    /// Interaction logic for KatedraWindow.xaml
    /// </summary>
    public partial class KatedraWindow : Window, INotifyPropertyChanged, IObserver
    {

        public ObservableCollection<Katedra> katedras { get; set; }
        public KatedraController _katedracontroller { get; set; }
        public Katedra SelectedKatedra { get; set; }


        public ProfesorController ProfesorController;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public KatedraWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _katedracontroller = app.KatedraController;
            ProfesorController = app.ProfesorController;
            _katedracontroller.Subscribe(this);
            ProfesorController.Subscribe(this);

            katedras = new ObservableCollection<Katedra>(_katedracontroller.GetAllKatedre());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedKatedra != null)
            {
                ProfesorFilter pf = new ProfesorFilter(SelectedKatedra);
                pf.Show();
            }
            else
            {
                MessageBox.Show("Odaberite katedru za koju zelite da dodate sefa!");
            }


        }

        public void Update()
        {
            katedras.Clear();
            foreach (Katedra katedra in _katedracontroller.GetAllKatedre())
            {
                katedras.Add(katedra);
            }
            //SelectedKatedra.sef = ProfesorController.GetAllProfesors().Find(p => p.Pid == SelectedKatedra.sefKatedreID);

            OnPropertyChanged("SelectedKatedra.sef");
        }
    }
}
