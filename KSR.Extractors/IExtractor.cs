using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.XmlDataGetter.Models;

namespace KSR.Extractors
{
    public interface IExtractor
    {
        List<DataFeatureDictionary> extractFeatureDictionary(List<DataSetItem> dataSetItems);
    }
}
