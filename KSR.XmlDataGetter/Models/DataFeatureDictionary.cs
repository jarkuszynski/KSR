using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.XmlDataGetter.Models
{
    public class DataFeatureDictionary
    {
        public string Label { get; set; }
        public Dictionary<string, double> Feature { get; set; }
        public string ClassifiedLabel { get; set; }

        public DataFeatureDictionary()
        {

            Feature = new Dictionary<string, double>();
            ClassifiedLabel = "";
        }

        /// <summary>
        /// Used in classification, to provide original label and the classified one
        /// </summary>
        /// <param name="label"> original label </param>
        /// <param name="feature"> Dictionary with features </param>
        /// <param name="classifiedLabel"> classified label - to check with original</param>
        public DataFeatureDictionary(string label, Dictionary<string, double> feature, string classifiedLabel)
        {
            Label = label;
            Feature = feature;
            ClassifiedLabel = classifiedLabel;
        }
    }
}
