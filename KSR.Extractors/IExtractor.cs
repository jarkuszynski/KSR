using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.DataPreprocessing.Models;
using KSR.XmlDataGetter.Models;

namespace KSR.Extractors
{
    public interface IExtractor
    {
         double[] extractFeatureDictionary(PreprocessedDataSetItem PreprocessedDataSetItems, List<string> keyWords);
    }
}
