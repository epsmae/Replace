using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Replace
{
    public static class RegexUtility
    {
        public static IList<string> GetMatches(string source, string searchRegex)
        {
            IList<string> foundMatches = new List<string>();
            Regex filterRegex = new Regex(searchRegex, RegexOptions.CultureInvariant);
            MatchCollection matches = filterRegex.Matches(source);
            
            foreach (Match match in matches)
            {
                foundMatches.Add(match.Value);
            }

            return foundMatches;
        }
    }
}
