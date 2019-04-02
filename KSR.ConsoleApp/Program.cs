using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.XmlDataGetter;
using KSR.DataPreprocessing;

namespace KSR.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\Johnny\\Desktop\\ksr\\data\\reut2-000.sgm";
            var tmp = DataGetter.ReadDataSetItems(filePath, "PLACES");
            var filtered = tmp.Select(s => (s.Labels, Tokenization.Tokenize(s.Report.Body))).ToList();
        }
    }
}
