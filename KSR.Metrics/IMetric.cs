using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.XmlDataGetter.Models;

namespace KSR.Metrics
{
    public interface IMetric
    {
        double getDistance(DataFeatureDictionary toCompareData, DataFeatureDictionary roleData);
    }
}
