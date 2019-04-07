using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.ConsoleApp
{
    public class ConfigLoader
    {
        public string source { get; }
        public string label { get; }
        public List<string> labels { get; }
        public int k { get; }
        public int trainingSetPercentage { get; set; }
        public string chosenMetric { get; set; }
        public string featuresExtractor { get; set; }
        public int n { get; set; }
        public int limit { get; set; }

        public ConfigLoader()
        {
            source = ConfigurationManager.AppSettings.Get("source");
            label = ConfigurationManager.AppSettings.Get("label");
            labels = ConfigurationManager.AppSettings.Get("labels").Split(',').ToList();
            k = int.Parse(ConfigurationManager.AppSettings.Get("k"));
            trainingSetPercentage = int.Parse(ConfigurationManager.AppSettings.Get("trainingSetPercentage"));
            chosenMetric = ConfigurationManager.AppSettings.Get("metric");
            featuresExtractor = ConfigurationManager.AppSettings.Get("extractor");
            n = int.Parse(ConfigurationManager.AppSettings.Get("n"));
            limit = int.Parse(ConfigurationManager.AppSettings.Get("limit"));
        }
    }
}
