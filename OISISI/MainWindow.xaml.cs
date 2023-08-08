using Projekat_group5_team8.Model;
using ProjekatV2.Controller;
using ProjekatV2.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ProjekatV2.Model;
using ProjekatV2.Model.DAO;

namespace ProjekatV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window , IObserver
    {
        
        public ObservableCollection<Student> Students { get; set; }
        public Student SelectedStudent { get; set; }
        //-----------------------------------------------------------
        public ObservableCollection<Profesor> Profesors { get; set; }
        public Profesor SelectedProfesor { get; set; }
        //-----------------------------------------------------------
        public ObservableCollection<Predmet> Predmets { get; set; }
        public Predmet SelectedPredmet { get; set; }
        //-----------------------------------------------------------
       
        public StudentController _studentController { get; set; }
        //-----------------------------------------------------------
        public ProfesorController _profesorController { get; set; }
        //-----------------------------------------------------------
        public PredmetController _predmetController { get; set; }
        //-----------------------------------------------------------

        public String CurrentTab { get; set; }
        public string SearchText { get; set; }

        public MainWindow()
        {
            
            
            InitializeComponent();
            this.DataContext = this;

            CurrentTab = "Student";

            var app = Application.Current as App;

            _studentController = app.StudentController;
            _studentController.Subscribe(this);
            //------------------------------------------------
            _profesorController = app.ProfesorController;
            _profesorController.Subscribe(this);
            //------------------------------------------------
            _predmetController = app.PredmetController;
            _predmetController.Subscribe(this);


            Students = new ObservableCollection<Student>(_studentController.GetAllStudents());

            Predmets = new ObservableCollection<Predmet>(_predmetController.getAllPredmeti());

            Profesors = new ObservableCollection<Profesor>(_profesorController.GetAllProfesors());


            //-----------program zauzima 3/4 ekrana-------------------------------------
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.75);
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.75);
            //---------------------------------------------------------------------------

            //-------------------tajmer-----------------------
            DispatcherTimer timer = new DispatcherTimer();       
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            //------------------------------------------------
        }


        //openi nam sluze samo za prebacivanje izmedju prozora u data-gridu
        private void OpenStudenti(object sender, RoutedEventArgs e)
        {
            CurrentTab = "Student";
            myTabControl.SelectedIndex = 0;
        }

        private void OpenProfesori(object sender, RoutedEventArgs e)
        {
            CurrentTab = "Profesor";
            myTabControl.SelectedIndex = 1;
        }

        private void OpenPredmeti(object sender, RoutedEventArgs e)
        {
            CurrentTab = "Predmet";
            myTabControl.SelectedIndex = 2;
        }

        private void OpenKatedra(object sender, RoutedEventArgs e)
        {
          KatedraWindow katedra = new KatedraWindow();
            katedra.Show();
        }



        private void AddStudent(object sender, RoutedEventArgs e)
        {
            CreateStudent createStudent = new CreateStudent();
            createStudent.Show();
        }

        private void AddPredmet(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            PredmetController predmetController = app.PredmetController;
            CreatePredmet createPredmet= new CreatePredmet();
            createPredmet.Show();
        }

        private void AddProfesor(object sender, RoutedEventArgs e)
        {
            CreateProfesor createProfesor= new CreateProfesor();
            createProfesor.Show();
           
        }


        private void UpdateStudent()
        {
            if (SelectedStudent == null)
            {
                MessageBox.Show("Odaberite studenta kojeg želite da izmenite.");
            }
            else
            {
                EditStudent editStudent = new EditStudent(SelectedStudent);
                editStudent.Show();
            }

        }


        private void UpdateProfesor()
        {
            if (SelectedProfesor == null)
            {
                MessageBox.Show("Odaberite profesora kojeg želite da izmenite.");
            }
            else
            {
                EditProfesor editProfesor = new EditProfesor(SelectedProfesor);
                editProfesor.Show();
            }
        }



        private void UpdatePredmet()
        {
            if (SelectedPredmet == null)
            {
                MessageBox.Show("Odaberite predmet koji želite da izmenite.");
            }
            else
            {
                EditPredmet editPredmet = new EditPredmet(SelectedPredmet);
                editPredmet.Show();
            }
        }



        private void UpdateTimer_Tick(object sender,EventArgs e)
        {
            DisplayDateTextBlock.Text = DateTime.Now.ToString();
        }


        public void Update()
        {
            Students.Clear();
            foreach (var s in _studentController.GetAllStudents()) 
            {
            Students.Add(s);
            }

            Profesors.Clear();
            foreach (var p in _profesorController.GetAllProfesors())
            {
                Profesors.Add(p);
            }

            Predmets.Clear();
            foreach (var pr in _predmetController.getAllPredmeti())
            {
                Predmets.Add(pr);
            }

        }

        private void MenuItem_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            if (myTabControl.SelectedIndex == 0)
            {
                AddStudent(sender, e);
            }
            else if (myTabControl.SelectedIndex == 1)
            {
                AddProfesor(sender, e); 
            }
            else
            {
                AddPredmet(sender, e);
            }
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {

            if (myTabControl.SelectedIndex == 0)
            {
                if (SelectedStudent != null)
                {
                    UpdateStudent();
                }
                else
                {
                    MessageBox.Show("Odaberite studenta kojeg želite da izmenite.");
                }
            }
            else if (myTabControl.SelectedIndex == 1)
            {
                if (SelectedProfesor != null)
                {
                    UpdateProfesor();
                }
                else
                {
                    MessageBox.Show("Odaberite profesora kojeg želite da izmenite.");
                }
            }
            else
            {
                if (SelectedPredmet != null)
                {
                    UpdatePredmet();
                }
                else
                {
                    MessageBox.Show("Odaberite predmet koji želite da izmenite.");
                }
            }
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            
            if (myTabControl.SelectedIndex == 0 )
            {
                if (SelectedStudent != null)
                {
                    MessageBoxResult result = ConfirmStudentDeletion();

                    if(result == MessageBoxResult.Yes)
                    _studentController.Delete(SelectedStudent); 

                }
                else
                { MessageBox.Show("Odaberite studenta kojeg želite da izbrišete."); }
            }
            else if (myTabControl.SelectedIndex == 1)
            {
                if (SelectedProfesor != null)
                {
                    MessageBoxResult result = ConfirmProfesorDeletion();

                    if (result == MessageBoxResult.Yes)
                        _profesorController.Delete(SelectedProfesor); 

                }
                else
                { MessageBox.Show("Odaberite profesora kojeg želite da izbrišete."); }
            }
            else
            {
                if (SelectedPredmet != null)
                {
                    MessageBoxResult result = ConfirmPredmetDeletion();

                    if (result == MessageBoxResult.Yes)
                        _predmetController.Delete(SelectedPredmet); 
                }
                else
                { MessageBox.Show("Odaberite predmet kojeg želite da izbrišete."); }
            }
        }

        private MessageBoxResult ConfirmStudentDeletion()
        {
            string sMessageBoxText = $"Da li ste sigurni da želite da izbrišete studenta\n{SelectedStudent.ime}{" "}{SelectedStudent.prezime}";
            string sCaption = "Potvrda brisanja";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            return result;
        }

        private MessageBoxResult ConfirmProfesorDeletion()
        {
            string sMessageBoxText = $"Da li ste sigurni da želite da izbrišete profesora\n{SelectedProfesor.Pime}{" "}{SelectedProfesor.Pprezime}";
            string sCaption = "Potvrda brisanja";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            return result;
        }

        private MessageBoxResult ConfirmPredmetDeletion()
        {
            string sMessageBoxText = $"Da li ste sigurni da želite da izbrišete predmet\n{SelectedPredmet.naziv}";
            string sCaption = "Potvrda brisanja";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            return result;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (SearchText == null){
                return;
            }
            if (myTabControl.SelectedIndex == 0)
            {
                string[] delovi = SearchText.Split(',');
                if (delovi.Length == 1) 
                {
                    delovi[0].Trim();
                    List<Student> SviStudenti = _studentController.Search("", "", delovi[0]);
                    Students.Clear();
                    foreach (var student in SviStudenti)
                    {
                        Students.Add(student);
                    }
                }
                else if (delovi.Length == 2) 
                {
                    string prezime = delovi[0].Trim();
                    string ime = delovi[1].Trim();
                    List<Student> SviStudenti = _studentController.Search("", ime, prezime);
                    Students.Clear();
                    foreach (var student in SviStudenti)
                    {
                        Students.Add(student);
                    }

                }
                else if (delovi.Length == 3) 
                {
                    string broj_indeksa = delovi[0].Trim();
                    string ime = delovi[1].Trim();
                    string prezime = delovi[2].Trim();
                    List<Student> SviStudenti = _studentController.Search(broj_indeksa, ime, prezime);
                    Students.Clear();
                    foreach (var student in SviStudenti)
                    {
                        Students.Add(student);
                    }
                }
            }
            else if (myTabControl.SelectedIndex == 1) 
            {
                string[] delovi = SearchText.Split(',');
                if (delovi.Length == 1)
                {
                    delovi[0].Trim();
                    List<Profesor> SviProfesori = _profesorController.Search("", delovi[0]);
                    Profesors.Clear();
                    foreach (var profesor in SviProfesori)
                    {
                        Profesors.Add(profesor);
                    }
                }
                else if (delovi.Length == 2)   
                {
                    string prezime = delovi[0].Trim();
                    string ime = delovi[1].Trim();
                    List<Profesor> SviProfesori = _profesorController.Search(ime, prezime);
                    Profesors.Clear();
                    foreach (var profesor in SviProfesori)
                    {
                        Profesors.Add(profesor);
                    }

                }
            }
            else  
            {
                string[] delovi = SearchText.Split(',');
                if (delovi.Length == 1)
                {
                    delovi[0].Trim();
                    List<Predmet> SviPredmeti = _predmetController.Search("", delovi[0]);
                    Predmets.Clear();
                    foreach (var predmet in SviPredmeti)
                    {
                        Predmets.Add(predmet);
                    }
                }
                else if (delovi.Length == 2)
                {
                    string naziv = delovi[0].Trim();
                    string sifra = delovi[1].Trim();
                    List<Predmet> SviPredmeti = _predmetController.Search(sifra, naziv);
                    Predmets.Clear();
                    foreach (var predmet in SviPredmeti)
                    {
                        Predmets.Add(predmet);
                    }

                }
            }
        }


        //shortcut-ovi
        private void Window_KeyDown(object sender, KeyEventArgs e){
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.C))
                {
                    this.Close();
                } 
            else if(Keyboard.IsKeyDown(Key.LeftCtrl)&& Keyboard.IsKeyDown(Key.N)) 
                {
                CreateClick(sender, e);
                }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.O) && Keyboard.IsKeyDown(Key.D1))
            {
                OpenStudenti(sender, e);
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.O) && Keyboard.IsKeyDown(Key.D2))
            {               
               OpenProfesori(sender, e);         
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.O) && Keyboard.IsKeyDown(Key.D3))
            {
                OpenPredmeti(sender, e);
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.O) && Keyboard.IsKeyDown(Key.D4))
            {
                OpenKatedra(sender, e);
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
            {
               EditClick(sender, e);
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Delete))
            {
                DeleteClick(sender, e);
            }
        }
            

    }
}
