using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.XmlDataGetter.Models
{
    public class DataSetItem
    {
        public DataArticle Article { get; set; }
        public DataLabels Labels { get; set; }
        public DataSetItem(DataArticle dataArticle, DataLabels dataLabels)
        {
            Article = dataArticle;
            Labels = dataLabels;
        }
    }
}
