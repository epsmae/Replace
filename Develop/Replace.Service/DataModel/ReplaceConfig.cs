using System.Collections.Generic;

namespace Replace.Service.DataModel
{
    public class ReplaceConfig
    {
        public List<RegexReplaceValue> RegexReplaceValues { get; set; }
        public List<string> FileExtensions { get; set; }
        public string PathToSearch { get; set; }
    }
}
