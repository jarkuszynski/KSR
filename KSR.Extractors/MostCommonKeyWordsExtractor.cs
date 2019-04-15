using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.DataPreprocessing.Models;
using KSR.XmlDataGetter.Models;

namespace KSR.Extractors
{
    public class MostCommonKeyWordsExtractor
    {
        private readonly int NUMBER_OF_COMMON_WORDS = 10;

        public List<string> Labels { get; set; }

        public List<string> getAllKeyWords(List<PreprocessedDataSetItem> PreprocessedDataSetItems)
        {
            Labels = new List<string>();
            List<DataFeatureDictionary> extractedData = new List<DataFeatureDictionary>();

            List<string> tempWordsFromArticle = new List<string>(); // wszystkie slowa z 1 artykulu, ktory jest przefiltrowan
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

            //List<DataFeatureDictionary> orderedExtractedData = new List<DataFeatureDictionary>();

            var orderedExtractedData = extractedData.GroupBy((l => l.Label));
            Dictionary<string, Dictionary<string, int>> ordered = new Dictionary<string, Dictionary<string, int>>();
            foreach (var l in orderedExtractedData)
            {
                ordered.Add(l.Key, new Dictionary<string, int>());
            }

            foreach (var data in extractedData)
            {

                foreach (var Feature in data.Feature)
                {

                    if (ordered[data.Label].ContainsKey(Feature.Key))
                    {
                        ordered[data.Label][Feature.Key] += (int)Feature.Value;
                    }
                    else
                    {
                        ordered[data.Label][Feature.Key] = (int)Feature.Value;
                    }
                }

            }

            Dictionary<string, int> allWordsCouted = new Dictionary<string, int>();
            foreach (var data in extractedData)
            {
                foreach (var Feature in data.Feature)
                {
                    if (allWordsCouted.ContainsKey(Feature.Key))
                    {
                        allWordsCouted[Feature.Key] += (int)Feature.Value;
                    }
                    else
                    {
                        allWordsCouted.Add(Feature.Key, 1);
                    }
                }
            }
            var countedAllWords = allWordsCouted.OrderByDescending(d => d.Value).ToDictionary(k => k.Key, v => v.Value);
            List<string> mostCommonWords = new List<string>();
            for (int k = 0; k < NUMBER_OF_COMMON_WORDS; k++)
            {
                mostCommonWords.Add(countedAllWords.ElementAt(k).Key);
            }


            var groupedWordsByLabel = ordered.Select(s => s.Value).ToList();
            Dictionary<string, Dictionary<string, int>> labeledGroupsWithWordsSortedDescending = new Dictionary<string, Dictionary<string, int>>();
            int index = 0;
            foreach (var toSort in groupedWordsByLabel)
            {
                var x = toSort.OrderByDescending(d => d.Value).ToDictionary(k => k.Key, v => v.Value);
                labeledGroupsWithWordsSortedDescending.Add(ordered.Keys.ElementAt(index), x);
                index++;
            }


            foreach (var key in labeledGroupsWithWordsSortedDescending.Keys)
            {
                Labels.Add(key);
                foreach (var commonWord in mostCommonWords)
                {
                    labeledGroupsWithWordsSortedDescending[key].Remove(commonWord);
                }
            }

            List<string> keyWords = new List<string>();

            foreach (var key in labeledGroupsWithWordsSortedDescending.Keys)
            {
                for (int i = 0; i < NUMBER_OF_COMMON_WORDS; i++)
                {
                    keyWords.Add(labeledGroupsWithWordsSortedDescending[key].ElementAt(i).Key);
                }
            }

            return keyWords;
        }
    }
}
