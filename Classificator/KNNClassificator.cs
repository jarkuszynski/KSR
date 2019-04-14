using KSR.Classificator.Metrics;
using KSR.DataPreprocessing.Models;
using KSR.Extractors;
using KSR.XmlDataGetter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classificator
{
    class Label_Distance
    {
        public string label;
        public double distance;

        public Label_Distance(string label, double distance)
        {
            this.label = label;
            this.distance = distance;
        }
    }
    public class KNNClassificator
    {
        /*
         1. get traing data, k, maximal distance
         2. get testing data
         3. for each element in testing data count distance to each training data - sort it ascendly 
         4. take label from the nearest neighbous and writes it to testing data entity
         */
        private List<PreprocessedDataSetItem> ColdStart = new List<PreprocessedDataSetItem>();
        private List<FeatureVector> TrainingFeatureVectors = new List<FeatureVector>();
        private int k;
        private IMetric metric;
        private IExtractor extractor;
        private List<string> KeyWords = new List<string>();

        public KNNClassificator(List<PreprocessedDataSetItem> coldStart,int k,IMetric metric, List<string> keyWords, IExtractor extractor)
        {
            ColdStart = coldStart;
            this.k = k;
            this.metric = metric;
            this.extractor = extractor;
            KeyWords = keyWords;
            InitializeTrainingFeatureVectors();
        }

        private void InitializeTrainingFeatureVectors()
        {
            foreach (var item in ColdStart)
            {
                TrainingFeatureVectors.Add(new FeatureVector(item.Labels[0].Value, extractor.extractFeatureDictionary(item, KeyWords)));
            }
        }

        public PreprocessedDataSetItem classify(PreprocessedDataSetItem testingData)
        {

            FeatureVector testingVector = new FeatureVector(testingData.Labels[0].Value, extractor.extractFeatureDictionary(testingData, KeyWords));

            List<Label_Distance> label_Distances = new List<Label_Distance>();
            foreach (var trainingVector in TrainingFeatureVectors)
            {
                label_Distances.Add(new Label_Distance(trainingVector.Label,
                    metric.getDistance(testingVector, trainingVector)
                    ));
            }

            label_Distances.Sort((prev, next) => prev.distance.CompareTo(next.distance));

            //take only this distances limited by k param
            label_Distances = label_Distances.Take(k).ToList();

            //TODO check!!!
            var grouped_Labels = label_Distances.GroupBy(l_d => l_d.label).Select(d => d.ToList()).ToList();
            grouped_Labels.Sort((prev, next) => prev.Count.CompareTo(next.Count));
            grouped_Labels.Reverse();

            PreprocessedDataSetItem classifiedData = new PreprocessedDataSetItem(testingData.Labels, testingData.ProcessedWords, grouped_Labels[0].First().label);


            return classifiedData;
        }


    }
}
