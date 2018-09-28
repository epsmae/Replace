using System;
using System.Collections.Generic;

namespace Replace.Service
{
    public class AsterisksMatcher
    {
        /// <summary>
        /// Return true when actual string matched the expected with respect to the asterisks
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public bool IsMatch(string expected, string actual)
        {
            if (string.IsNullOrEmpty(actual) || string.IsNullOrEmpty(expected))
            {
                return false;
            }

            if (expected.Contains("*"))
            {
                string[] parts = expected.Split('*');

                int lastIndex = -1;

                foreach (string part in parts)
                {
                    if (string.IsNullOrEmpty(part))
                    {
                        continue;
                    }

                    int index = actual.IndexOf(part, StringComparison.Ordinal);

                    if (index < 0 || index < lastIndex)
                    {
                        return false;
                    }

                    lastIndex = index;
                }
                
                return true;
            }

            return actual == expected;
        }

        /// <summary>
        /// Return true when actual string matched the expected with respect to the asterisks
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <returns></returns>
        public bool IsMatch(IList<string> expected, string actual)
        {
            foreach (string s in expected)
            {
                if (IsMatch(s, actual))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
