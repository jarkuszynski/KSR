using KSR.DataPreprocessing.Models;
using KSR.XmlDataGetter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.DataPreprocessing
{
    public class DataPreprocessingTool
    {
        public static PreprocessedDataSetItem PreprocessText(DataSetItem dataSetItem)
        {
            char[] delimiters = new[] {' ', ',', ';', '.', '\t', '\r', '\n', '+'};
            var wordsWithoutDelimiters =
                dataSetItem.Article.Body.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                    .Where(w => w.Length > 1 && !double.TryParse(w, out _)).ToList();       //000?
            StringBuilder builder = new StringBuilder();
            Porter2Stemmer stemmer = new Porter2Stemmer();
            foreach (string currWord in wordsWithoutDelimiters)
            {
                string lowerWord = currWord.ToLowerInvariant();

                if (!StopWordsDictionary.Stops.ContainsKey(lowerWord))
                {
                    lowerWord = stemmer.Stem(currWord);
                    builder.Append(lowerWord.ToLowerInvariant()).Append(' ');
                }
            }

            var wordList = builder.ToString().Split(' ').ToList();
            wordList.RemoveAt(wordList.Count - 1);
            return new PreprocessedDataSetItem(dataSetItem.Labels.LabelList, wordList);
        }
    }
}
