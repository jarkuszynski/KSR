using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.XmlDataGetter.Models;

namespace KSR.Metrics
{
    public class ChebyshevMetric: IMetric
    {
        public double getDistance(DataFeatureDictionary toCompareData, DataFeatureDictionary roleData)
        {
            List<double> distances = new List<double>();
            List<string> keyWords = new List<string>();
            keyWords.AddRange(toCompareData.Feature.Keys.ToList());
            keyWords.AddRange(roleData.Feature.Keys.ToList());
            keyWords = keyWords.Distinct().ToList();
            double toCompareDataValue = 0.0;
            double roleDataValue = 0.0;

            foreach (string keyWord in keyWords)
            {
                double keyValue = 0.0;
                toCompareDataValue = toCompareData.Feature.TryGetValue(keyWord, out keyValue) ? keyValue : 0.0;
                roleDataValue = roleData.Feature.TryGetValue(keyWord, out keyValue) ? keyValue : 0.0;
                distances.Add(Math.Abs(toCompareDataValue - roleDataValue));
            }

            return distances.Max();
        }
    }
}
