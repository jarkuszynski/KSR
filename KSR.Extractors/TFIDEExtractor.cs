using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.DataPreprocessing.Models;
using KSR.XmlDataGetter.Models;

namespace KSR.Extractors
{
    public class TFIDEExtractor : IExtractor
    {
        public List<DataFeatureDictionary> extractFeatureDictionary(List<PreprocessedDataSetItem> PreprocessedDataSetItems)
        {
            TFExtractor preExtractor = new TFExtractor();
            List<DataFeatureDictionary> extractedData = preExtractor.extractFeatureDictionary(PreprocessedDataSetItems);
            List<DataFeatureDictionary> TFIDEextractedData = new List<DataFeatureDictionary>();
            Dictionary<string, double> tempFeatures = new Dictionary<string, double>();


            int numberOfDocuments = extractedData.Count;
            double IDFFactor = 0.0;
            int numberOfWordOccurances = 0;
            //count for each word in how many documents its present
            foreach (var article in extractedData)
            {

                tempFeatures = new Dictionary<string, double>();
                foreach (var word in article.Feature.Keys)
                {

                    foreach (var item in extractedData)
                    {
                        numberOfWordOccurances += item.Feature.ContainsKey(word) ? 1 : 0;
                    }
                    IDFFactor = Math.Log10(numberOfDocuments / numberOfWordOccurances);
                    tempFeatures.Add(word, article.Feature[word] * IDFFactor);
                    IDFFactor = 0.0;
                    numberOfWordOccurances = 0;
                }
                TFIDEextractedData.Add(new DataFeatureDictionary(article.Label, tempFeatures, ""));

            }

            //TODO debug TFIDE 
            return TFIDEextractedData;
        }
    }
}
