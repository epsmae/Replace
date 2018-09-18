using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Replace.Service
{
    public static class RegexUtility
    {
        public static IList<string> GetMatches(string source, string searchRegex)
        {
            Regex filterRegex = new Regex(searchRegex, RegexOptions.Compiled);
            return GetMatches(source, filterRegex);
        }

        public static IList<string> GetMatches(string source, Regex filterRegex)
        {
            IList<string> foundMatches = new List<string>();
            MatchCollection matches = filterRegex.Matches(source);

            foreach (Match match in matches)
            {
                foundMatches.Add(match.Value);
            }

            return foundMatches;
        }

        public static IList<string> GetMatches(string source, IList<Regex> filterRegexList)
        {
            IList<string> foundMatches = new List<string>();

            foreach (Regex regex in filterRegexList)
            {
                MatchCollection matches = regex.Matches(source);

                foreach (Match match in matches)
                {
                    foundMatches.Add(match.Value);
                }
            }

            return foundMatches;
        }
    }
}
