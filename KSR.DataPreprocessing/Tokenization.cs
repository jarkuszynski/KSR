using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.DataPreprocessing
{
    public class Tokenization
    {
        public static IEnumerable<string> Tokenize(string text)
        {
            char[] delimiters = new char[] { ' ', ',', ';', '.', '\t', '\r', '\n'  };
            return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
