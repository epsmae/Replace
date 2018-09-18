using System.Collections.Generic;

namespace Replace.Service.DataModel
{
    public class Config
    {
        public Config()
        {
            RegexReplaceValues = new List<RegexReplaceValue>();
            FileExtensions = new List<string>();
            TagReplacements = new List<KeyValuePair<string, string>>();
        }

        public List<RegexReplaceValue> RegexReplaceValues { get; set; }
        public List<string> FileExtensions { get; set; }
        public string PathToSearch { get; set; }
        public List<KeyValuePair<string, string>> TagReplacements { get; set; }
    }
}
