using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using Projekat_group5_team8.Model;

namespace ProjekatV2.Storage
{
    internal class OcenaStorage
    {
        private const string StoragePath = "../../Data/ocene.csv";

        private readonly Serializer<Ocena> _serializer;

        public OcenaStorage()
        {
            _serializer = new Serializer<Ocena>();
        }

        public List<Ocena> Load()
        {
            return _serializer.fromCSV(StoragePath);
        }

        public void Save(List<Ocena> ocene)
        {
            _serializer.toCSV(StoragePath, ocene);
        }

    }
}
