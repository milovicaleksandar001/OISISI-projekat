using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekat_group5_team8.Model;
using ConsoleApp.Serialization;

namespace ProjekatV2.Storage
{
    internal class ProfesorStorage
    {
        private const string StoragePath = "../../Data/profesors.csv";

        private readonly Serializer<Profesor> _serializer;

        public ProfesorStorage()
        {
            _serializer = new Serializer<Profesor>();
        }

        public List<Profesor> Load()
        {
            return _serializer.fromCSV(StoragePath);
        }

        public void Save(List<Profesor> profesors)
        {
            _serializer.toCSV(StoragePath, profesors);
        }

    }
}
