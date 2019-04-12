using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.DataPreprocessing.Models;
using KSR.XmlDataGetter.Models;

namespace KSR.Extractors
{
    public class KeyWordsExtractor
    {
        //public List<DataFeatureDictionary> extractMostCommonKeyWords(List<PreprocessedDataSetItem> PreprocessedDataSetItems, int howManyToSave)
        //{
        //    List<DataFeatureDictionary> extractedData = new List<DataFeatureDictionary>();
        //    var groupedSamplesByLabel = PreprocessedDataSetItems.GroupBy(l => l.Labels[0]);
        //    foreach (var sample in groupedSamplesByLabel)
        //    {
        //        var mostCommonKeyWords = sample.Select(s => s.Labels[0])
        //            .GroupBy(s => s)
        //            .Select(s => (s.Key.Value.ToString(), s.Count() * 1.0))
        //            .ToList();
        //        mostCommonKeyWords.Sort((f, s) => f.Item2.CompareTo(s.Item2));
        //        extractedData.Add(new DataFeatureDictionary(sample.Key.Value,mostCommonKeyWords,""));
        //    }

        //    return null;
        //}
    }
}
