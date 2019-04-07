using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.XmlDataGetter.Models
{
    public class DataArticle
    {
        public string Title { get; set; }
        public string Dateline { get; set; }
        public string Body { get; set; }

        public DataArticle(string title, string dateline, string body)
        {
            Title = title;
            Dateline = dateline;
            Body = body;
        }
    }
}
