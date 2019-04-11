using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.Metrics;
using KSR.Extractors;


namespace KSR.ConsoleApp
{
    public class ConfigLoader
    {
        public string Sources { get; }
        public string Label { get; }
        public List<string> Labels { get; }
        public int K { get; }
        public double TrainingSetPercentage { get; set; }
        public double TestingSetPercentage { get; set; }
        public IMetric Metric { get; set; }
        public IExtractor Extractor { get; set; }
        public int N { get; set; }

        public ConfigLoader()
        {
            Sources = ConfigurationManager.AppSettings.Get("source");
            Label = ConfigurationManager.AppSettings.Get("label");
            Labels = ConfigurationManager.AppSettings.Get("labels").Split(',').ToList();
            K = int.Parse(ConfigurationManager.AppSettings.Get("k"));
            TrainingSetPercentage = int.Parse(ConfigurationManager.AppSettings.Get("trainingSetPercentage"))/ 100.0;
            TestingSetPercentage = 1.0 - TrainingSetPercentage;
            Metric = setMetric(ConfigurationManager.AppSettings.Get("metric"));
            Extractor = setExtractor(ConfigurationManager.AppSettings.Get("extractor"));
            N = int.Parse(ConfigurationManager.AppSettings.Get("n"));
        }

        IMetric setMetric(string metric)
        {
            switch (metric)
            {
                case "euclidean":
                    return new EuclideanMetric();
                    break;
                default:
                    return new EuclideanMetric();
                    break;
            }
        }

        IExtractor setExtractor(string extractor)
        {
            switch (extractor)
            {
                case "TF":
                    return new TFExtractor();
                    break;
                case "TFIDE":
                    return new TFIDEExtractor();
                    break;
                default:
                    return new TFExtractor(); ;
                    break;

            }
        }

        
    }
}
