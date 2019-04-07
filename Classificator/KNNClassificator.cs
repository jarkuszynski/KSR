using KSR.Metrics;
using KSR.XmlDataGetter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classificator
{
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
        private int maxDistance;
        private IMetric metric;

        public KNNClassificator(List<DataFeatureDictionary> trainingData,int k,int maxDistance,IMetric metric)
        {
            this.trainingData = trainingData;
            this.k = k;
            this.maxDistance = maxDistance;
            this.metric = metric;
        }

        public List<DataFeatureDictionary> classify(DataFeatureDictionary testingData)
        {
            List<DataFeatureDictionary> classifiedData = new List<DataFeatureDictionary>();


            return classifiedData;
        }


    }
}
