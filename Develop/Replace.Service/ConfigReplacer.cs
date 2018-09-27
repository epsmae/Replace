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
        private readonly AsterisksMatcher _asterisksMatcher;
        private DirectoryInfo _currentDirectory;
        
        public ConfigReplacer(ReplaceConfig config)
        {
            _asterisksMatcher = new AsterisksMatcher();
            _config = config;

            foreach (RegexReplaceValue value in config.RegexReplaceValues)
            {
                _dictionary.Add(new Regex(value.Regex, RegexOptions.Compiled), value.ReplaceValue);
            }
        }

        public ReplaceResult Replace()
        {
            _currentDirectory = new DirectoryInfo(_config.PathToSearch);
            return ReplaceInsideDirectory(_currentDirectory);
        }

        private ReplaceResult ReplaceInsideDirectory(DirectoryInfo directory)
        {
            ReplaceResult replaceResult = new ReplaceResult();
            IList<DirectoryInfo> currentSubDirectories = directory.EnumerateDirectories("*").ToList();
            IEnumerable<FileInfo> files = directory.GetFiles("*", SearchOption.TopDirectoryOnly);
            //IList<FileInfo> filteredFiles = files.Where(file => _config.FileExtensions.Any(ext => file.Name.EndsWith(ext, StringComparison.OrdinalIgnoreCase))).ToList();

            IList<FileInfo> filteredFiles = files.Where(file => _asterisksMatcher.IsMatch(_config.FileExtensions, file.Name)).ToList();

            foreach (FileInfo fileInfo in filteredFiles)
            {
                List<KeyValuePair<string, string>> replacements = Replace(fileInfo);
                if (replacements.Count > 0)
                {
                    replaceResult.Replacements.AddRange(replacements);
                    replaceResult.NumberOfAffectedFiles++;
                }
            }

            foreach (DirectoryInfo subDirectory in currentSubDirectories)
            {
                ReplaceResult result = ReplaceInsideDirectory(subDirectory);
                if (result.NumberOfReplacements > 0)
                {
                    replaceResult.Replacements.AddRange(result.Replacements);
                    replaceResult.NumberOfAffectedFiles += result.NumberOfAffectedFiles;
                }
            }

            return replaceResult;
        }

        private List<KeyValuePair<string, string>> Replace(FileInfo file)
        {
            List<KeyValuePair<string, string>> replacements = new List<KeyValuePair<string, string>>();
            string source = File.ReadAllText(file.FullName);

            foreach (Regex regex in _dictionary.Keys)
            {
                IList<string> matches = RegexUtility.GetMatches(source, regex);

                foreach (string match in matches)
                {
                    source = source.Replace(match, _dictionary[regex]);
                    replacements.Add(new KeyValuePair<string, string>(match, $"{_dictionary[regex]} {file.FullName}"));
                }
            }

            if (replacements.Count > 0)
            {
                File.WriteAllText(file.FullName, source);
            }

            return replacements;
        }
    }
}
