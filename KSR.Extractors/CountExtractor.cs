using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.DataPreprocessing.Models;
using KSR.XmlDataGetter.Models;

namespace KSR.Extractors
{
    public class CountExtractor : IExtractor
    {
        public double[] extractFeatureDictionary(PreprocessedDataSetItem PreprocessedDataItem, List<string> keyWords)
        {
            List<DataFeatureDictionary> extractedData = new List<DataFeatureDictionary>();

            List<string> tempWordsFromArticle = new List<string>(); // wszystkie slowa z 1 artykulu, ktory jest przefiltrowan

            DataFeatureDictionary extractedDataItem = new DataFeatureDictionary();
            tempWordsFromArticle = new List<string>();
            tempWordsFromArticle = PreprocessedDataItem.ProcessedWords; //TODO change body to List<string>


            foreach (string word in tempWordsFromArticle)
            {
                if (extractedDataItem.Feature.ContainsKey(word))
                {
                    extractedDataItem.Feature[word] = extractedDataItem.Feature[word] + 1.0;
                }
                else
                {
                    extractedDataItem.Feature.Add(word, 1.0);
                }
            }
            extractedDataItem.Label = PreprocessedDataItem.Labels.ElementAt(0).Value;

            //TODO count vector
            double[] extractedVector = new double[keyWords.Count];
            int index = 0;
            foreach (var word in keyWords)
            {
                double defaultValue = 0.0; 
                if(extractedDataItem.Feature.TryGetValue(word, out defaultValue))
                {
                    extractedVector[index] = defaultValue;
                }
                index++;
            }
            return extractedVector;
        }
    }
}
