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
        //public List<DataFeatureDictionary> extractFeatureDictionary(List<PreprocessedDataSetItem> PreprocessedDataSetItems, List<string> keyWords)
        //{
        //    TFExtractor preExtractor = new TFExtractor();
        //    List<DataFeatureDictionary> extractedData = preExtractor.getPreparedData(PreprocessedDataSetItems);
        //    List<DataFeatureDictionary> TFIDEextractedData = new List<DataFeatureDictionary>();
        //    Dictionary<string, double> tempFeatures = new Dictionary<string, double>();


        //    int numberOfDocuments = extractedData.Count;
        //    double IDFFactor = 0.0;
        //    int numberOfDocumentsContainingWord = 0;
        //    //count for each word in how many documents its present
        //    foreach (var article in extractedData)
        //    {

        //        tempFeatures = new Dictionary<string, double>();
        //        foreach (var word in article.Feature.Keys)
        //        {
        //            IDFFactor = 0.0;
        //            numberOfDocumentsContainingWord = 0;
        //            foreach (var item in extractedData)
        //            {
        //                numberOfDocumentsContainingWord += item.Feature.ContainsKey(word) ? 1 : 0;
        //            }
        //            IDFFactor = Math.Log10(numberOfDocuments / numberOfDocumentsContainingWord * 1.0);
        //            tempFeatures.Add(word, article.Feature[word] * IDFFactor);

        //        }
        //        TFIDEextractedData.Add(new DataFeatureDictionary(article.Label, tempFeatures, ""));

        //    }

        //    //TODO debug TFIDE 
        //    return TFIDEextractedData;
        //}

        public double[] extractFeatureDictionary(PreprocessedDataSetItem PreprocessedDataSetItems, List<string> keyWords)
        {
            TFExtractor preExtractor = new TFExtractor();
            DataFeatureDictionary extractedData = preExtractor.getPreparedData(PreprocessedDataSetItems, keyWords);
            List<DataFeatureDictionary> TFIDEextractedData = new List<DataFeatureDictionary>();
            Dictionary<string, double> tempFeatures = new Dictionary<string, double>();


            int numberOfDocuments = extractedData.Count;
            double IDFFactor = 0.0;
            int numberOfDocumentsContainingWord = 0;
            //count for each word in how many documents its present
           
                tempFeatures = new Dictionary<string, double>();
                foreach (var word in extractedData.Feature.Keys)
                {
                    IDFFactor = 0.0;
                    numberOfDocumentsContainingWord = 0;
                    foreach (var item in extractedData.Feature)
                    {
                        numberOfDocumentsContainingWord += item(word) ? 1 : 0;
                    }
                    IDFFactor = Math.Log10(numberOfDocuments / numberOfDocumentsContainingWord * 1.0);
                    tempFeatures.Add(word, extractedData.Feature[word] * IDFFactor);

                }
                TFIDEextractedData.Add(new DataFeatureDictionary(extractedData.Label, tempFeatures, ""));

            //TODO debug TFIDE 
            return TFIDEextractedData;
        }
    }
}
