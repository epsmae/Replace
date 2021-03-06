﻿using System.Collections.Generic;
using System.Linq;

namespace Replace.Service
{
    public class FileReplacer
    {
        private readonly string _filePath;

        public FileReplacer(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Replace all occurance of a regex in a file
        /// </summary>
        /// <param name="searchRegex"></param>
        /// <param name="replacement"></param>
        /// <returns>Replacement count</returns>
        public int Replace(string searchRegex, string replacement)
        {
            return Replace(searchRegex, replacement, _filePath);
        }

        /// <summary>
        /// Replace all occurance of a regex and store in a diffrent file
        /// </summary>
        /// <param name="searchRegex"></param>
        /// <param name="replacement"></param>
        /// <param name="outFilePath"></param>
        /// <returns>Replacement count</returns>
        public int Replace(string searchRegex, string replacement, string outFilePath)
        {
            string source = System.IO.File.ReadAllText(_filePath);
            IList<string> matches = RegexUtility.GetMatches(source, searchRegex);

            foreach (string match in matches)
            {
                source = source.Replace(match, replacement);
            }

            if (matches.Any())
            {
                System.IO.File.WriteAllText(outFilePath, source);
            }

            return matches.Count;
        }
    }
}
