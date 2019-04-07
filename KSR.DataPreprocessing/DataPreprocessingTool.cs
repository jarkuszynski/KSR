using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.DataPreprocessing
{
    public class DataPreprocessingTool
    {
        public static IEnumerable<string> TokenizeAndRemoveStopWords(string text)
        {
            char[] delimiters = new[] {' ', ',', ';', '.', '\t', '\r', '\n'};
            var wordsWithoutDelimiters =
                text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                    .Where(w => w.Length > 1 && !double.TryParse(w, out _)).ToList();
            StringBuilder builder = new StringBuilder();

            foreach (string currWord in wordsWithoutDelimiters)
            {
                string lowerWord = currWord.ToLowerInvariant();

                if (!StopWordsDictionary.Stops.ContainsKey(lowerWord))
                {
                    builder.Append(lowerWord).Append(' ');
                }
            }

            return builder.ToString().Split(' ').ToList();
        }
    }
}
