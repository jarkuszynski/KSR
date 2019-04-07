using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.DataPreprocessing
{
    public class DataPreprocessingTool
    {
        public static List<string> PreprocessText(string text)
        {
            char[] delimiters = new[] {' ', ',', ';', '.', '\t', '\r', '\n'};
            var wordsWithoutDelimiters =
                text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
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

            return builder.ToString().Split(' ').ToList();
        }
    }
}
