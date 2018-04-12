using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Replace.DataModel;

namespace Replace
{
    public class ConfigFileReplacer
    {
        private readonly Config _config;
        private readonly Dictionary<Regex, string> _dictionary = new Dictionary<Regex, string>();
        private DirectoryInfo _currentDirectory;

        public ConfigFileReplacer(Config config)
        {
            _config = config;

            foreach (RegexReplaceValue value in config.RegexReplaceValues)
            {
                _dictionary.Add(new Regex(value.Regex, RegexOptions.Compiled), value.ReplaceValue);
            }
        }

        public void Replace()
        {
            _currentDirectory = new DirectoryInfo(_config.PathToSearch);
            ReplaceInsideDirectory(_currentDirectory);
        }

        private void ReplaceInsideDirectory(DirectoryInfo directory)
        {
            IList<DirectoryInfo> currentSubDirectories = directory.EnumerateDirectories("*").ToList();
            IEnumerable<FileInfo> files = directory.GetFiles("*", SearchOption.TopDirectoryOnly);
            IList<FileInfo> filteredFiles = files.Where(file => _config.FileExtensions.Any(ext => file.Name.EndsWith(ext, StringComparison.OrdinalIgnoreCase))).ToList();

            foreach (FileInfo fileInfo in filteredFiles)
            {
                Replace(fileInfo);
            }

            foreach (DirectoryInfo subDirectory in currentSubDirectories)
            {
                ReplaceInsideDirectory(subDirectory);
            }
        }

        private void Replace(FileInfo file)
        {
            bool modified = false;
            string source = File.ReadAllText(file.FullName);

            foreach (Regex regex in _dictionary.Keys)
            {
                IList<string> matches = RegexUtility.GetMatches(source, regex);

                foreach (string match in matches)
                {
                    source = source.Replace(match, _dictionary[regex]);
                    modified = true;
                }
            }

            if (modified)
            {
                File.WriteAllText(file.FullName, source);
            }
        }
    }
}
