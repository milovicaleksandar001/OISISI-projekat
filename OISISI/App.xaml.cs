using ProjekatV2.Controller;
using ProjekatV2.Model.DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjekatV2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public StudentController StudentController { get; set; }

        public ProfesorController ProfesorController { get; set; }

        public AdresaController AdresaController { get; set; }

        public PredmetController PredmetController { get; set; }

        public OcenaController OcenaController { get; set; } 

        public KatedraController KatedraController { get; set; } 

        public App()
        {

            AdresaDAO adresaDAO= new AdresaDAO();
            AdresaController = new AdresaController(adresaDAO);

            StudentDAO studentDAO = new StudentDAO();
            studentDAO.AdresaDAO = adresaDAO;
            StudentController = new StudentController(studentDAO);

            ProfesorDAO profesorDAO= new ProfesorDAO();
            ProfesorController = new ProfesorController(profesorDAO);

            PredmetDAO predmetDAO = new PredmetDAO();
            PredmetController = new PredmetController(predmetDAO);

            OcenaDAO oceneDAO = new OcenaDAO();
            OcenaController = new OcenaController(oceneDAO);


            KatedraDAO katedraDAO = new KatedraDAO();
            KatedraController = new KatedraController(katedraDAO);

            katedraDAO.ProfesorDAO = profesorDAO;
            katedraDAO.BindSefKatedra();

        }

    }
}
