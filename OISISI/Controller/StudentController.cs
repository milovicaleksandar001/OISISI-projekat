using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatV2.Model;
using ProjekatV2.Model.DAO;
using ProjekatV2.Storage;
using ProjekatV2.Observer;
using Projekat_group5_team8.Model;

namespace ProjekatV2.Controller
{
    public class StudentController
    {
       private readonly StudentDAO _students;

        public StudentController(StudentDAO studentDAO)
        {
            _students = studentDAO;
        }

        public List<Student> GetAllStudents()
        {
            return _students.GetAllStudenti();
        }

        public void Create(Student student)
        {
            _students.addStudent(student);
        }

        public void Delete(Student student)
        {
            _students.removeStudent(student.id);
        }

        public void Update(Student student) 
        {
            _students.updateStudent(student);
        }

        public void Subscribe(IObserver observer)
        {
            _students.Subscribe(observer);
        }

        public int UkupnoESP(Student student) {
            return _students.UkupnoESP(student);
        }

        public double Prosek(Student student) {
            return _students.Prosek(student);
        }

        public List<Student> Search(string index, string ime, string prezime){
            return _students.Search(index, ime, prezime);
        }

    }
}
