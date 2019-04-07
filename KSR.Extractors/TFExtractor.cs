using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.XmlDataGetter.Models;
using KSR.XmlDataGetter;
using KSR.DataPreprocessing;
using KSR.DataPreprocessing.Models;

namespace KSR.Extractors
{
    public class TFExtractor : IExtractor
    {
        public TFExtractor()
        {

        }
        public List<DataFeatureDictionary> extractFeatureDictionary(List<PreprocessedDataSetItem> PreprocessedDataSetItems)
        {
            List<DataFeatureDictionary> extractedData = new List<DataFeatureDictionary>();
           
            /*
             1. Take every article from dataSetItems
             2. array: Prepare them to have plain array of words lowercase and after Lemization, without stopwords
             3. take array and create from this DataFeatureDictionary
             */
             // label TODO
            List<string> tempWordsFromArticle; // wszystkie slowa z 1 artykulu, ktory jest przefiltrowan
            foreach (var articleWithLabel in PreprocessedDataSetItems)
            {
                DataFeatureDictionary tempDictionary = new DataFeatureDictionary();
                tempWordsFromArticle = new List<string>();
                tempWordsFromArticle = articleWithLabel.ProcessedWords; //TODO change body to List<string>

                foreach (string word in tempWordsFromArticle)
                {
                    if (tempDictionary.Feature.ContainsKey(word))
                    {
                        tempDictionary.Feature[word] = tempDictionary.Feature[word] + 1.0;
                    }
                    else
                    {
                        tempDictionary.Feature.Add(word, 1.0);
                    }
                }
                tempDictionary.Label = articleWithLabel.Labels.ElementAt(0).Value;
                extractedData.Add(tempDictionary);
            }


            return extractedData;
        }
    }
}
