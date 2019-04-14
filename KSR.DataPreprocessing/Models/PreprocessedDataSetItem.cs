using KSR.XmlDataGetter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.DataPreprocessing.Models
{
    public class PreprocessedDataSetItem
    {
        public List<Label> Labels { get; set; }
        public List<string> ProcessedWords { get; set; }

        public string ClassifiedLabel { get; set; }

        public PreprocessedDataSetItem(List<Label> labels, List<string> processedWords, string classifiedLabel ="")
        {
            Labels = labels;
            ProcessedWords = processedWords;
            ClassifiedLabel = classifiedLabel;
        }
        public PreprocessedDataSetItem(Label label)
        {
            ProcessedWords = new List<string>();
            Labels.Add(label);
        }
    }
}
