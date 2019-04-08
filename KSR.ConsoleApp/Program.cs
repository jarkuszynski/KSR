
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.XmlDataGetter;
using KSR.DataPreprocessing;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using KSR.Extractors;
using Classificator;
using KSR.Metrics;
using KSR.XmlDataGetter.Models;

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

            //TODO 1. select data to read from and load it. 
            ConfigLoader Config = new ConfigLoader();
            string filePath =$"{Directory.GetCurrentDirectory()}\\..\\..\\..\\data\\reut2-000.sgm";
            var tmp = DataGetter.ReadDataSetItems(filePath, Config.Label);
            Porter2Stemmer stemmer = new Porter2Stemmer();
            var filtered = tmp.Select(s => DataPreprocessingTool.PreprocessText(s)).ToList();
            IExtractor extractor = Config.Extractor;
            IMetric metric = Config.Metric;
            int k = Config.K;
            double trainingDataPercentage = Config.TrainingSetPercentage;
            double testingDataPercentage = Config.TestingSetPercentage;

            var x = extractor.extractFeatureDictionary(filtered);
            x = x.Where(p => p.Label != "UNKNOWN").ToList();
            var training = x.GetRange(0, (int)(x.Count * Config.TrainingSetPercentage));
            var testing = x.GetRange((int)(x.Count * Config.TrainingSetPercentage), (int)(x.Count * testingDataPercentage));
            KNNClassificator knn = new KNNClassificator(testing, Config.K, Config.Metric);

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
