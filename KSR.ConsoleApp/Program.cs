
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
using Classificator;
using KSR.Metrics;
using KSR.XmlDataGetter.Models;

namespace KSR.ConsoleApp
{

    class Label_Distace
    {
        public string label;
        public double distance;

        public Label_Distace(string label, double distance)
        {
            this.label = label;
            this.distance = distance;
        }
    }
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
            string filePath = "C:\\Users\\Maciej\\source\\repos\\KSR\\data\\reut2-000.sgm";
            var tmp = DataGetter.ReadDataSetItems(filePath, "PLACES");
            Porter2Stemmer stemmer = new Porter2Stemmer();
            var filtered = tmp.Select(s => DataPreprocessingTool.PreprocessText(s)).ToList();
            IExtractor extractor = new TFExtractor();

            var x = extractor.extractFeatureDictionary(filtered);
            x = x.Where(p => p.Label != "UNKNOWN").ToList();
            var xd = x.GetRange(0, (int)(x.Count * 0.6));
            var cd = x.GetRange((int)(x.Count * 0.6), (int)(x.Count * 0.4));
            var training = x.GetRange(0, (int)(x.Count * 0.6));
            var testing = x.GetRange((int)(x.Count * 0.6), (int)(x.Count * 0.4));
            KNNClassificator knn = new KNNClassificator(testing, 9, 20, new EuclideanMetric());

            List<DataFeatureDictionary> classified = new List<DataFeatureDictionary>();
            foreach (var test in testing)
            {
                classified.Add(knn.classify(test));
            }

            int correctlyMatched = 0;
            foreach (var item in classified)
            {
                if (item.ClassifiedLabel == item.Label) correctlyMatched++;
            }
            classified = classified.Where(p => p.ClassifiedLabel != "usa").ToList();

        }
    }
}
