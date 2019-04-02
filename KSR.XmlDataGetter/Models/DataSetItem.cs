using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.XmlDataGetter.Models
{
    public class DataSetItem
    {
        public DataReport Report { get; set; }
        public DataLabels Labels { get; set; }
        public DataSetItem(DataReport dataReport, DataLabels dataLabels)
        {
            Report = dataReport;
            Labels = dataLabels;
        }
    }
}
