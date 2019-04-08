using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.XmlDataGetter.Models;

namespace KSR.Metrics
{
    public class EuclideanMetric : IMetric
    {
        public double getDistance(DataFeatureDictionary toCompareData, DataFeatureDictionary roleData)
        {
            List<string> keyWords = new List<string>();
            keyWords.AddRange(toCompareData.Feature.Keys.ToList());
            keyWords.AddRange(roleData.Feature.Keys.ToList());
            double distance = 0.0;
            double keyValue = 0.0;
            double toCompareDataValue = 0.0;
            double roleDataValue = 0.0;
            foreach (string keyWord in keyWords)
            {
                if (toCompareData.Feature.TryGetValue(keyWord, out keyValue))
                    toCompareDataValue = keyValue;
                if(roleData.Feature.TryGetValue(keyWord, out keyValue))
                {
                    roleDataValue = keyValue;
                }
                distance += Math.Abs(toCompareDataValue - roleDataValue);
            }
            return distance;
        }
    }
}
