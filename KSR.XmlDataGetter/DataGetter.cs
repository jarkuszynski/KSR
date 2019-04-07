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
        public static List<DataSetItem> ReadDataSetItems(string pathFile, string labelTitle)
        {
            List<DataSetItem> dataSet = new List<DataSetItem>();
            List<Label> labels = new List<Label>();
            if (!File.Exists(pathFile))
            {
                throw new Exception("File does not exists");
            }

            using (var reader = new XmlTextReader(pathFile)) 
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (string.Equals(reader.Name, labelTitle, StringComparison.InvariantCultureIgnoreCase))
                        {
                            labels = new List<Label>();
                            while (reader.Read())
                            {
                                while (reader.Name.ToUpperInvariant() == "D")
                                {
                                    reader.Read();
                                }

                                if (!string.IsNullOrWhiteSpace(reader.Value))
                                {
                                    labels.Add(new Label(reader.Value));
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

                                dataSet.Add(new DataSetItem(new DataArticle(title, dateline, body), new DataLabels(labels)));
                            }
                        }
                    }
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
