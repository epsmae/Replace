using System.Collections.Generic;
using CommandLine;

namespace Replace.App
{
    internal class Options
    {
        [Option('s', "search", Required = false, HelpText = "Search regex pattern")]
        public string Regex { get; set; }

        [Option('f', "file", Required = false, HelpText = "File to search and replace")]
        public string File { get; set; }

        [Option('r', "replacement", Required = false, HelpText = "Replacement text")]
        public string Replacement { get; set; }

        [Option('c', "Config", Required = false, HelpText = "Config file path")]
        public string Config { get; set; }

        [Option('t', "tagreplacements", Required = false, HelpText = "Tags which will be replaced by the value")]
        public IList<string> TagReplacements { get; set; }
    }
}
