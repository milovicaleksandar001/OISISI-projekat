using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjekatV2.Model.KonverzijaDatuma
{
    public class DatumKonv
    {
        public static string DateToString(DateTime date)
        {
            return date.ToString("dd.MM.yyyy.");
        }

        public static DateTime StringToDate(string date)
        {
            if(date == null)
            { 
                MessageBox.Show("Datum nije u dobrom formatu ili nije unet");
                return DateTime.MinValue;
            }
        else { return DateTime.ParseExact(date, "dd.MM.yyyy.", null); }
            
        }


    }
}
