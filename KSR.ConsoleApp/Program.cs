
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.XmlDataGetter;
using KSR.DataPreprocessing;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using KSR.Extractors;
using Classificator;
using KSR.Classificator.Metrics;
using KSR.XmlDataGetter.Models;
using System.Diagnostics;
using KSR.DataPreprocessing.Models;

namespace KSR.ConsoleApp
{
    class ConfusionParams
    {
        public int MatrixIndex { get; set; }
        public int TFN { get; set; }
        public int TFP { get; set; }
        public int TTN { get; set; }
        public double Specificity { get; set; }
        public double Precision { get; set; }
        public double Recall { get; set; }
        public ConfusionParams(int matrixNumber)
        {
            MatrixIndex = matrixNumber;
        }
        public void CountSpecificity(int TTPall, int TTNall)
        {
            Specificity = (1.0 * TTNall) / (TTNall + TFP);

        }
        public void CountPrecision(int TTPall)
        {
            Precision = (1.0 * TTPall) / (TTPall + TFP);
        }
        public void CountRecall(int TTPall)
        {
            Recall = (1.0 * TTPall) / (TTPall + TFN);
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            // steps 
            /*
             1. load configuration file 
             extract data from file ( tokenize ) 
             split data to training set and testing set 60/40
             clasify training data using knn
             after getting knn results, classify testing data and collect results
             */

            //TODO 1. select data to read from and load i
            Console.WriteLine("Initalization, please wait a minute....");
            ConfigLoader Config = new ConfigLoader();
            string directoryPath = $"{Directory.GetCurrentDirectory()}\\..\\..\\..\\data\\";
            string searchPattern = "";

            
            string[] filteredLabel = new[] { "west-germany", "usa", "france", "uk", "canada", "japan" };

            List<DataSetItem> tmp = new List<DataSetItem>();
            if (Config.Sources == "reuters")
            {
                searchPattern = "*.sgm";
            }
            else if (Config.Sources == "custom")
            {
                searchPattern = "*.csv";
            }

            IEnumerable<string> filesInDirectory = Directory.EnumerateFiles(directoryPath, searchPattern);

            if (Config.Label == "PLACES")
            {
                tmp = DataGetter.ReadDataSetItems(filesInDirectory, Config.Label, filteredLabel);

            }
            else if ( Config.Label == "TOPICS")
            {
                if (searchPattern != "*.csv")
                    tmp = DataGetter.ReadDataSetItems(filesInDirectory, Config.Label);
                else
                    tmp = DataGetter.ReadCSVDataSet(filesInDirectory.ElementAt(0));
                
            }

            Porter2Stemmer stemmer = new Porter2Stemmer();
            var filtered = tmp.Select(s => DataPreprocessingTool.PreprocessText(s)).ToList();

            IExtractor extractor = Config.Extractor;
            IMetric metric = Config.Metric;
            int k = Config.K;

            double trainingDataPercentage = Config.TrainingSetPercentage;
            double testingDataPercentage = Config.TestingSetPercentage;
            filtered = filtered.GetRange(0, 500);

            var training = filtered.GetRange(0, (int)(filtered.Count * Config.TrainingSetPercentage));
            var testing = filtered.GetRange((int)(filtered.Count * Config.TrainingSetPercentage), (int)(filtered.Count * testingDataPercentage));

            MostCommonKeyWordsExtractor most = new MostCommonKeyWordsExtractor();
            List<string> keyWords = most.getAllKeyWords(filtered);

            var knn = new KNNClassificator(training, Config.K, Config.Metric, keyWords, Config.Extractor);

            
            ///////////////////////////////////////////////////////////////////////////

            string[] distingushLabels = most.Labels.ToArray();

            Dictionary<string, int> matrixIndexes = new Dictionary<string, int>();
            int index = 0;
            foreach (var label in distingushLabels)
            {
                matrixIndexes.Add(label, index);
                index++;
            }

            Console.WriteLine("\n1. Change extractor - invalid, restart program");
            Console.WriteLine("\n2. Change metric");
            Console.WriteLine("\n3. Change K number - neighbours");
            Console.WriteLine("\n4. Change N number");
            Console.WriteLine("\n5. Start Classification");
            Console.WriteLine("\n6. show configuration");
            Console.WriteLine("\nq. Quit");
            char key = '0';
            do
            {
                Console.WriteLine("choose option:");
                key = Console.ReadKey().KeyChar;
                switch (key)
                {
                    case '1':
                        //Console.WriteLine("\nTF-a, TFIDE-b, NGram-c, KeyWords-d\n");
                        break;
                    case '2':
                        Console.WriteLine("\nChebyshev metric-e, Manhattan-f, Euclidean-g\n");
                        break;
                    case 'a':
                        Config.Extractor = new TFExtractor();
                        extractor = Config.Extractor;
                        break;
                    case 'b':
                        Config.Extractor = new TFIDEExtractor();
                        extractor = Config.Extractor;
                        break;
                    case 'c':
                        Config.Extractor = new NGramExtractor(Config.N);
                        extractor = Config.Extractor;
                        break;
                    case 'e':
                        Config.Metric = new ChebyshevMetric();
                        metric = Config.Metric;
                        knn = new KNNClassificator(training, Config.K, Config.Metric, keyWords, Config.Extractor);
                        break;
                    case 'f':
                        Config.Metric = new ManhattanMetric();
                        metric = Config.Metric;
                        knn = new KNNClassificator(training, Config.K, Config.Metric, keyWords, Config.Extractor);
                        break;
                    case 'g':
                        Config.Metric = new EuclideanMetric();
                        metric = Config.Metric;
                        knn = new KNNClassificator(training, Config.K, Config.Metric, keyWords, Config.Extractor);
                        break;
                    case '3':
                        Console.WriteLine("\nProvide number representing K neighbours - 9 is default\n");
                        int defaultK = 9;
                        int neighbourhood = int.TryParse(Console.ReadLine(), out defaultK) ? defaultK : 9;
                        Config.K = neighbourhood;
                        knn = new KNNClassificator(training, Config.K, Config.Metric, keyWords, Config.Extractor);
                        break;
                    case '4':
                        Console.WriteLine("\nProvide number representing N in Ngram - 3 is default\n");
                        int defaultN = 3;
                        int nGram = int.TryParse(Console.ReadLine(), out defaultN) ? defaultN : 9;
                        Config.N = nGram;
                        knn = new KNNClassificator(training, Config.K, Config.Metric, keyWords, Config.Extractor);
                        break;
                    case '5':
                        ////////////////////////////// core of program

                        Console.WriteLine("++++++++++CLASSIFICATION STARTED++++++++++");
                        Stopwatch classificationTime = Stopwatch.StartNew();

                        List<PreprocessedDataSetItem> classified = new List<PreprocessedDataSetItem>();
                        foreach (var testItem in testing)
                        {
                            classified.Add(knn.classify(testItem));
                        }
                        classificationTime.Stop();

                        foreach (var item in classified)
                        {
                            Console.WriteLine(item.ClassifiedLabel + " = " + item.Labels[0].Value);
                        }
                        int[,] ConfusionMarix = new int[distingushLabels.Length, distingushLabels.Length];
                        int TTPall = 0;

                        List<ConfusionParams> ConfusionList = new List<ConfusionParams>();
                        foreach (var label in distingushLabels)
                        {
                            ConfusionList.Add(new ConfusionParams(matrixIndexes[label]));
                        }

                        
                        foreach (var item in classified)
                        {
                            if (item.ClassifiedLabel == item.Labels[0].Value) TTPall++;

                            ConfusionMarix = incrementConfusionMatrix(ConfusionMarix, item, matrixIndexes);
                        }
                        //classified = classified.Where(p => p.ClassifiedLabel != "usa").ToList();

                        StringBuilder classificationResults = new StringBuilder();
                        for (int t = 0; t < ConfusionMarix.GetLength(0); t++)
                        {
                            for (int j = 0; j < ConfusionMarix.GetLength(1); j++)
                            {
                                Console.Write(" | " + ConfusionMarix[t, j] + " | ");
                            }
                            Console.WriteLine("------------");
                        }

                        foreach (var label in ConfusionList)
                        {
                            for (int t = 0; t < ConfusionMarix.GetLength(1); t++)
                            {
                                if (t != label.MatrixIndex)
                                {
                                    label.TFN += ConfusionMarix[label.MatrixIndex, t];
                                }
                            }
                            for (int t = 0; t < ConfusionMarix.GetLength(0); t++)
                            {
                                if (t != label.MatrixIndex)
                                {
                                    label.TFN += ConfusionMarix[t, label.MatrixIndex];
                                }
                            }
                            for (int i = 0; i < ConfusionMarix.GetLength(0); i++)
                            {
                                if (i != label.MatrixIndex)
                                {
                                    for (int j = 0; j < ConfusionMarix.GetLength(1); j++)
                                    {
                                        if (j != label.MatrixIndex)
                                            label.TTN += ConfusionMarix[i, j];
                                    }
                                }
                            }
                        }
                        int TTNall = 0;
                        foreach (var label in ConfusionList)
                        {
                            TTNall += label.TTN;
                        }
                        foreach (var label in ConfusionList)
                        {
                            label.CountPrecision(TTPall);
                            label.CountRecall(TTPall);
                            label.CountSpecificity(TTPall, TTNall);
                        }
                        double OverallAccurency = (1.0 * TTPall) / testing.Capacity;
                        double OverallSpecificity = 0.0;
                        foreach (var label in ConfusionList)
                        {
                            OverallSpecificity += label.Specificity;
                        }
                        OverallSpecificity = (1.0 * OverallSpecificity) / ConfusionList.Capacity;
                        double OverallPrecision = 0.0;
                        foreach (var label in ConfusionList)
                        {
                            OverallPrecision += label.Precision;
                        }
                        OverallPrecision = (1.0 * OverallPrecision) / ConfusionList.Capacity;
                        double OverallRecall = 0.0;
                        foreach (var label in ConfusionList)
                        {
                            OverallRecall += label.Recall;
                        }
                        OverallRecall = (1.0 * OverallRecall) / ConfusionList.Capacity;

                        classificationResults.Append("\n" + Config.ToString() + "\n");
                        classificationResults.AppendLine("Overall Accurency: " + OverallAccurency);
                        classificationResults.AppendLine("Overall Specificity: " + OverallSpecificity);
                        classificationResults.AppendLine("Overall Precision: " + OverallPrecision);
                        classificationResults.AppendLine("Overall Recall: " + OverallRecall);
                        classificationResults.AppendLine("Classification Time: " + classificationTime.ElapsedMilliseconds + " ms");

                        Console.WriteLine(classificationResults.ToString());

                        break;
                    case '6':
                        Console.WriteLine("\n" + Config.ToString() + "\n");
                        break;
                    default:
                        break;
                }
            } while (key != 'q');


            int[,] incrementConfusionMatrix(int[,] confusionMatrix, PreprocessedDataSetItem classifiedArticle, Dictionary<string, int> indexMatrix)
            {
                confusionMatrix[indexMatrix[classifiedArticle.Labels[0].Value], indexMatrix[classifiedArticle.ClassifiedLabel]]++;
                return confusionMatrix;
            }
        }
    }
}
