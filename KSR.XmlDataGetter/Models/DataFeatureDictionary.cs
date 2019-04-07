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


    }
}
