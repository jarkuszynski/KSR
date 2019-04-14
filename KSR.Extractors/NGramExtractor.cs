using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.DataPreprocessing.Models;
using KSR.XmlDataGetter.Models;

namespace KSR.Extractors
{
    public class NGramExtractor : IExtractor
    {
        public int N { get; set; }
        public NGramExtractor(int n)
        {
            N = n;
        }

        public double[] extractFeatureDictionary(PreprocessedDataSetItem PreprocessedDataSetItems, List<string> keyWords)
        {
            DataFeatureDictionary extractedData = new DataFeatureDictionary();

            Dictionary<string, double> tempDictionary = new Dictionary<string, double>();
            string connectedWords = string.Join("", PreprocessedDataSetItems.ProcessedWords);
            char[] chars = connectedWords.ToCharArray();

            for (int i = 0; i < chars.Length - N; i++)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int j = i; j < i + N; j++)
                {
                    stringBuilder.Append(chars[j]);
                }

                string tmpKey = stringBuilder.ToString();
                if (tempDictionary.ContainsKey(tmpKey))
                    tempDictionary[tmpKey] = tempDictionary[tmpKey] + 1.0;
                else
                {
                    tempDictionary.Add(tmpKey, 1.0);
                }
            }
            extractedData = new DataFeatureDictionary(PreprocessedDataSetItems.Labels[0].Value, tempDictionary, "");

            double[] extractedVector = new double[keyWords.Count];
           

            int index = 0;
            foreach (var word in keyWords)
            {
                double defaultValue = 0.0;
                if (extractedData.Feature.TryGetValue(word, out defaultValue))
                {
                    extractedVector[index] = defaultValue;
                }
                index++;
            }
            return extractedVector;
        }
    }
}
