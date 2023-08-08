using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using Projekat_group5_team8.Model;

namespace ProjekatV2.Storage
{
    internal class AdresaStorage
    {
        private const string StoragePath = "../../Data/adrese.csv";

        private readonly Serializer<Adresa> _serializer;

        public AdresaStorage()
        {
            _serializer = new Serializer<Adresa>();
        }

        public List<Adresa> Load()
        {
            return _serializer.fromCSV(StoragePath);
        }

        public void Save(List<Adresa> adrese)
        {
            _serializer.toCSV(StoragePath, adrese);
        }
    }
}
