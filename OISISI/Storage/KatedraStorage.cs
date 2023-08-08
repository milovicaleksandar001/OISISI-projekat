using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using Projekat_group5_team8.Model;

namespace ProjekatV2.Storage
{
    internal class KatedraStorage
    {
        private const string StoragePath = "../../Data/katedre.csv";

        private readonly Serializer<Katedra> _serializer;

        public KatedraStorage()
        {
            _serializer = new Serializer<Katedra>();
        }

        public List<Katedra> Load()
        {
            return _serializer.fromCSV(StoragePath);
        }

        public void Save(List<Katedra> katedre)
        {
            _serializer.toCSV(StoragePath, katedre);
        }
    }
}
