using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.XmlDataGetter.Models
{
    public class DataLabels
    {
        public List<Label> LabelList { get; set; }

        public DataLabels(List<Label> labels)
        {
            LabelList = labels;
        }
    }
}
