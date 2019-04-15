﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.DataPreprocessing.Interfaces
{
    public interface IStemmer
    {
        /// <summary>
        /// Stem a word.
        /// </summary>
        /// <param name="word">Word to stem.</param>
        /// <returns>
        /// The stemmed word, with a reference to the original unstemmed word.
        /// </returns>
        string Stem(string word);
    }
}
