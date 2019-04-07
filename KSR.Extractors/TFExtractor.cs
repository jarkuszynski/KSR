using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.XmlDataGetter.Models;

namespace KSR.Extractors
{
    public class TFExtractor : IExtractor
    {
        public List<DataFeatureDictionary> extractFeatureDictionary(List<DataSetItem> dataSetItems)
        {
            List<DataFeatureDictionary> extractedData = new List<DataFeatureDictionary>();
            DataFeatureDictionary tempDictionary = new DataFeatureDictionary();
            /*
             1. Take every article from dataSetItems
             2. array: Prepare them to have plain array of words lowercase and after Lemization, without stopwords
             3. take array and create from this DataFeatureDictionary
             */
            List<string> tempWordsFromArticle; // wszystkie slowa z 1 artykulu, ktory jest przefiltrowany

            foreach (DataSetItem articleWithLabel in dataSetItems)
            {
                tempWordsFromArticle = articleWithLabel.Article.Body; //TODO change body to List<string>

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
                extractedData.Add(tempDictionary);
            }


            return extractedData;
        }
    }
}
