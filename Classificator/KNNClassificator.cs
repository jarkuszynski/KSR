using KSR.Metrics;
using KSR.XmlDataGetter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classificator
{
    class Label_Distance
    {
        public string label;
        public double distance;

        public Label_Distance(string label, double distance)
        {
            this.label = label;
            this.distance = distance;
        }
    }
    public class KNNClassificator
    {
        /*
         1. get traing data, k, maximal distance
         2. get testing data
         3. for each element in testing data count distance to each training data - sort it ascendly 
         4. take label from the nearest neighbous and writes it to testing data entity
         */
        private List<DataFeatureDictionary> trainingData = new List<DataFeatureDictionary>();
        private int k;
        private IMetric metric;

        public KNNClassificator(List<DataFeatureDictionary> trainingData,int k,IMetric metric)
        {
            this.trainingData = trainingData;
            this.k = k;
            this.metric = metric;
        }

        public DataFeatureDictionary classify(DataFeatureDictionary testingData)
        {
            DataFeatureDictionary classifiedData = new DataFeatureDictionary();

            List<Label_Distance> label_Distances = new List<Label_Distance>();
            foreach (var dataFeatureEntity in trainingData)
            {
                label_Distances.Add(new Label_Distance(dataFeatureEntity.Label,
                    metric.getDistance(testingData, dataFeatureEntity)
                    ));
            }
            label_Distances.Sort((prev, next) => prev.distance.CompareTo(next.distance));

            //take only this distances limited by k param
            label_Distances = label_Distances.Take(k).ToList();

            //TODO check!!!
            var grouped_Labels = label_Distances.GroupBy(l_d => l_d.label).Select(d => d.ToList()).ToList();
            grouped_Labels.Sort((prev, next) => prev.Count.CompareTo(next.Count));
            grouped_Labels.Reverse();

            classifiedData = new DataFeatureDictionary(testingData.Label, testingData.Feature, grouped_Labels[0].First().label);


            return classifiedData;
        }


    }
}
