using System.Collections.Generic;
using CommandLine;

namespace Replace
{
    internal class Options
    {
        [Option('s', "search", Required = true, HelpText = "Search regex pattern")]
        public string Regex { get; set; }

        [Option('f', "file", Required = true, HelpText = "File to search and replace")]
        public string File { get; set; }

        [Option('r', "replacement", Required = true, HelpText = "Replacement text")]
        public string Replacement { get; set; }
    }
}
