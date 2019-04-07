
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.XmlDataGetter;
using KSR.DataPreprocessing;
using System.Configuration;
using System.Collections.Specialized;

namespace KSR.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // steps 
            /*
             1. load configuration file 
             extract data from file ( tokenize ) 
             split data to training set and testing set 60/40
             clasify training data using knn
             after getting knn results, classify testing data and collect results
             */
            ConfigLoader Config = new ConfigLoader();
            Console.WriteLine(Config);
            string filePath = "C:\\Users\\Maciej\\source\\repos\\KSR\\data\\reut2-000.sgm";
            var tmp = DataGetter.ReadDataSetItems(filePath, "PLACES");
            var filtered = tmp.Select(s => (s.Labels, Tokenization.Tokenize(s.Report.Body))).ToList();
            Console.WriteLine();
        }
    }
}
