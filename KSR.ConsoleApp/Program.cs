
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.XmlDataGetter;
using KSR.DataPreprocessing;
using System.Configuration;
using System.Collections.Specialized;
using KSR.Extractors;

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
            string filePath = "C:\\Users\\johnn\\Desktop\\Studia_Projekty\\KSR\\KSR\\data\\reut2-000.sgm";
            var tmp = DataGetter.ReadDataSetItems(filePath, "PLACES");
            Porter2Stemmer stemmer = new Porter2Stemmer();
            var filtered = tmp.Select(s => DataPreprocessingTool.PreprocessText(s)).ToList();
            IExtractor extractor = new TFExtractor();
            var x = extractor.extractFeatureDictionary(filtered);
        }
    }
}
