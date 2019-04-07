
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
            string filePath = "C:\\Users\\johnn\\Desktop\\Studia_Projekty\\KSR\\KSR\\data\\reut2-000.sgm";
            var tmp = DataGetter.ReadDataSetItems(filePath, "PLACES");
            Porter2Stemmer stemmer = new Porter2Stemmer();
             var filtered = tmp.Select(s => (s.Labels, (DataPreprocessingTool.PreprocessText(s.Report.Body)))).ToList();
        }
    }
}
