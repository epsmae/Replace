using System.Collections.Generic;

namespace Replace.DataModel
{
    public class Config
    {
        public List<RegexReplaceValue> RegexReplaceValues { get; set; }
        public List<string> FileExtensions { get; set; }
        public string PathToSearch { get; set; }
        public List<KeyValuePair<string, string>> TagReplacements { get; set; }
    }
}
