using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classificator;
using KSR.XmlDataGetter.Models;

namespace KSR.Classificator.Metrics
{
    public class ManhattanMetric : IMetric
    {
        //public double getDistance(DataFeatureDictionary toCompareData, DataFeatureDictionary roleData)
        //{
        //    List<string> keyWords = new List<string>();
        //    keyWords.AddRange(toCompareData.Feature.Keys.ToList());
        //    keyWords.AddRange(roleData.Feature.Keys.ToList());
        //    keyWords = keyWords.Distinct().ToList();
        //    double distance = 0.0;
        //    double toCompareDataValue = 0.0;
        //    double roleDataValue = 0.0;
        //    foreach (string keyWord in keyWords)
        //    {
        //        double keyValue = 0.0;
        //        toCompareDataValue = toCompareData.Feature.TryGetValue(keyWord, out keyValue) ? keyValue : 0.0;
        //        roleDataValue = roleData.Feature.TryGetValue(keyWord, out keyValue) ? keyValue : 0.0;
        //        distance += Math.Abs(toCompareDataValue - roleDataValue);
        //    }
        //    return distance;
        //}

        public double getDistance(FeatureVector toCompareData, FeatureVector roleData)
        {
            return toCompareData.Vector.Zip(roleData.Vector, (x, y) => Math.Abs(x - y)).Sum();
        }
    }
}