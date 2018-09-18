using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Replace.Service.DataModel;

namespace Replace.Service
{
    public class ConfigReplacer
    {
        private readonly ReplaceConfig _config;
        private readonly IDictionary<Regex, string> _dictionary = new Dictionary<Regex, string>();
        private DirectoryInfo _currentDirectory;

        public ConfigReplacer(ReplaceConfig config)
        {
            _config = config;

            foreach (RegexReplaceValue value in config.RegexReplaceValues)
            {
                _dictionary.Add(new Regex(value.Regex, RegexOptions.Compiled), value.ReplaceValue);
            }
        }

        public int Replace()
        {
            _currentDirectory = new DirectoryInfo(_config.PathToSearch);
            return ReplaceInsideDirectory(_currentDirectory);
        }

        private int ReplaceInsideDirectory(DirectoryInfo directory)
        {
            int replaceCount = 0;
            IList<DirectoryInfo> currentSubDirectories = directory.EnumerateDirectories("*").ToList();
            IEnumerable<FileInfo> files = directory.GetFiles("*", SearchOption.TopDirectoryOnly);
            IList<FileInfo> filteredFiles = files.Where(file => _config.FileExtensions.Any(ext => file.Name.EndsWith(ext, StringComparison.OrdinalIgnoreCase))).ToList();

            foreach (FileInfo fileInfo in filteredFiles)
            {
                replaceCount += Replace(fileInfo);
            }

            foreach (DirectoryInfo subDirectory in currentSubDirectories)
            {
                replaceCount += ReplaceInsideDirectory(subDirectory);
            }

            return replaceCount;
        }

        private int Replace(FileInfo file)
        {
            int replaceCount = 0;
            string source = File.ReadAllText(file.FullName);

            foreach (Regex regex in _dictionary.Keys)
            {
                IList<string> matches = RegexUtility.GetMatches(source, regex);

                foreach (string match in matches)
                {
                    source = source.Replace(match, _dictionary[regex]);
                    replaceCount++;
                }
            }

            if (replaceCount > 0)
            {
                File.WriteAllText(file.FullName, source);
            }

            return replaceCount;
        }
    }
}
