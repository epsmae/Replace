using System.Collections.Generic;

namespace Replace.Service.DataModel
{
    public class ReplaceResult
    {
        public int NumberOfReplacements
        {
            get { return Replacements.Count; }
        }

        public int NumberOfAffectedFiles { get; set; }

        public List<KeyValuePair<string, string>> Replacements { get; set; }

        public ReplaceResult()
        {
            Replacements = new List<KeyValuePair<string, string>>();
        } 

        public override string ToString()
        {
            return
                $"replaced {NumberOfReplacements} regex results in {NumberOfAffectedFiles} files";
        }
    }
}
