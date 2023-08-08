using ConsoleApp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_group5_team8.Model
{
    class StudentPredmetNisuPolozili : Serializable
    {
        public int IdStudenta { get; set; }
        public int IdPredmeta { get; set; }

        public StudentPredmetNisuPolozili(int IDstudentaa, int IDpredmetaa)
        {
            this.IdStudenta = IDstudentaa;
            this.IdPredmeta = IDpredmetaa;
        }

        public StudentPredmetNisuPolozili() { }

        

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                IdStudenta.ToString(),
                IdPredmeta.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            IdStudenta = int.Parse(values[0]);
            IdPredmeta = int.Parse(values[1]);
        }
    }

}