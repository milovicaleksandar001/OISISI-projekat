using ConsoleApp.Serialization;
using Projekat_group5_team8.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjekatV2.Storage
{
    class StudentStorage
    {
        private const string StoragePath = "../../Data/students.csv";

        private readonly Serializer<Student> _serializer;

        public StudentStorage()
        {
            _serializer = new Serializer<Student>();
        }

        public List<Student> Load()
        {
            return _serializer.fromCSV(StoragePath);
        }

        public void Save(List<Student> students)
        {
            _serializer.toCSV(StoragePath, students);
        }

    }
}
