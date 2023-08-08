using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using Projekat_group5_team8.Model;

namespace ProjekatV2.Storage
{
    internal class PredmetStorage
    {
        private const string StoragePath = "../../Data/predmeti.csv";

        private readonly Serializer<Predmet> _serializer;

        public PredmetStorage()
        {
            _serializer = new Serializer<Predmet>();
        }

        public List<Predmet> Load()
        {
            return _serializer.fromCSV(StoragePath);
        }

        public void Save(List<Predmet> predmeti)
        {
            _serializer.toCSV(StoragePath, predmeti);
        }
    }
}
