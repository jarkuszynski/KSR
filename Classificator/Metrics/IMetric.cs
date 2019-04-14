using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.XmlDataGetter.Models;
using KSR.Classificator;
using Classificator;

namespace KSR.Classificator.Metrics
{
    public interface IMetric
    {
        double getDistance(FeatureVector toCompareData, FeatureVector roleData);
    }
}
