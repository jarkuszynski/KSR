using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using KSR.XmlDataGetter.Models;

namespace KSR.XmlDataGetter
{
    public class DataGetter
    {
        public static List<DataSetItem> ReadDataSetItems(IEnumerable<string> files, string labelTitle, string[] labelFilterConditions)
        {
            List<DataSetItem> dataSet = new List<DataSetItem>();
            List<Label> labels = new List<Label>();
            bool isLabeled = false;
            foreach (string file in files)
            {
                if (!File.Exists(file))
                {
                    throw new Exception("File does not exists");
                }

                using (var reader = new XmlTextReader(file))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (string.Equals(reader.Name, labelTitle, StringComparison.InvariantCultureIgnoreCase))
                            {
                                labels = new List<Label>();
                                isLabeled = false;
                                while (reader.Read())
                                {
                                    while (reader.Name.ToUpperInvariant() == "D")
                                    {
                                        reader.Read();
                                    }

                                    if (!string.IsNullOrWhiteSpace(reader.Value) &&
                                        labelFilterConditions.Any(l => l == reader.Value))
                                    {
                                        labels.Add(new Label(reader.Value));
                                        isLabeled = true;
                                    }

                                    if (reader.NodeType == XmlNodeType.EndElement && string.Equals(reader.Name,
                                            $"{labelTitle}", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        break;
                                    }
                                }
                            }

                            if (reader.Name.ToUpperInvariant() == "TITLE")
                            {
                                reader.Read();
                                string title = reader.Value;
                                string dateline = ReadElem(reader, "DATELINE");
                                string body = ReadElem(reader, "BODY");
                                if (title != null && dateline != null && body != null)
                                {
                                    if (!labels.Any())
                                    {
                                        labels.Add(new Label("UNKNOWN"));
                                    }

                                    if (isLabeled)
                                        dataSet.Add(new DataSetItem(new DataArticle(title, dateline, body),
                                            new DataLabels(labels)));
                                }
                            }
                        }
                    }
                }
            }

            return dataSet;
        }

        public static List<DataSetItem> ReadDataSetItems(IEnumerable<string> files, string labelTitle)
        {
            List<DataSetItem> dataSet = new List<DataSetItem>();
            List<Label> labels = new List<Label>();
            bool isLabeled = false;
            foreach (string file in files)
            {
                if (!File.Exists(file))
                {
                    throw new Exception("File does not exists");
                }

                using (var reader = new XmlTextReader(file))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (string.Equals(reader.Name, labelTitle, StringComparison.InvariantCultureIgnoreCase))
                            {
                                labels = new List<Label>();
                                isLabeled = false;
                                while (reader.Read())
                                {
                                    while (reader.Name.ToUpperInvariant() == "D")
                                    {
                                        reader.Read();
                                    }

                                    if (!string.IsNullOrWhiteSpace(reader.Value))
                                    {
                                        labels.Add(new Label(reader.Value));
                                        isLabeled = true;
                                    }

                                    if (reader.NodeType == XmlNodeType.EndElement && string.Equals(reader.Name,
                                            $"{labelTitle}", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        break;
                                    }
                                }
                            }

                            if (reader.Name.ToUpperInvariant() == "TITLE")
                            {
                                reader.Read();
                                string title = reader.Value;
                                string dateline = ReadElem(reader, "DATELINE");
                                string body = ReadElem(reader, "BODY");
                                if (title != null && dateline != null && body != null)
                                {
                                    if (!labels.Any())
                                    {
                                        labels.Add(new Label("UNKNOWN"));
                                    }

                                    if (isLabeled)
                                        dataSet.Add(new DataSetItem(new DataArticle(title, dateline, body),
                                            new DataLabels(labels)));
                                }
                            }
                        }
                    }
                }
            }

            return dataSet;
        }

        public static List<DataSetItem> ReadCSVDataSet(string file)
        {
            List<DataSetItem> dataSet = new List<DataSetItem>();
            if (!File.Exists(file))
            {
                throw new Exception("File does not exists");
            }

            using (var reader = new StreamReader(file))
            {
                List<string> topics = new List<string>();
                List<string> text = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    List<Label> labels = new List<Label>();

                    topics.Add(values[0]);
                    text.Add(values[1]);
                    labels.Add(new Label(values[0]));
                    dataSet.Add(new DataSetItem(new DataArticle("", "", values[1]),
                                            new DataLabels(labels)));
                }

            }
            

            return dataSet;
        }

        public static string ReadElem(XmlTextReader reader, string elementTitle)
        {
            bool isReading = true;
            while (isReading && !string.Equals(reader.Name.ToUpperInvariant(), elementTitle, StringComparison.InvariantCultureIgnoreCase))
            {
                isReading = reader.Read();
            }

            if (isReading)
            {
                reader.Read();
                return reader.Value;
            }

            return null;
        }
    }
}
