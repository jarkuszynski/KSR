using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classificator
{
    public class FeatureVector
    { 
        public double[] Vector { get; set; }
        public string Label { get; set; }

        public FeatureVector(string label, double[] vector)
        {
            Vector = vector;
            Label = label;
        }
    }
}
