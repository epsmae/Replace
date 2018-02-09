using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Replace.Test
{
    public class RegexUtitlityTest
    {
        [Test]
        public void TestRegexReplaceXmlNode()
        {
            const string sourceString = @"\n\nversion<Version>1.0.1.5</Version>version\nversion";
            const string searchRegex = "<Version>.*</Version>";
            const string expectedMatch = "<Version>1.0.1.5</Version>";

            IList<string> matches = RegexUtility.GetMatches(sourceString, searchRegex);
            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual(expectedMatch, matches.First());
        }

        [Test]
        public void TestRegexAssemblyInfo()
        {
            const string sourceString = "[assembly: AssemblyProduct(\"Replace.exe\")]";
            const string searchRegex = " .*AssemblyProduct(.*)";
            const string expectedMatch = " AssemblyProduct(\"Replace.exe\")]";

            IList<string> matches = RegexUtility.GetMatches(sourceString, searchRegex);
            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual(expectedMatch, matches.First());
        }

        [Test]
        public void TestRegexManifest()
        {
            const string sourceString = "<manifest name=\"App\" android:versionCode=\"15\" android:versionName=\"0.1.7.15\">";
            const string searchRegex = " android:versionCode=\".*\" ";
            const string expectedMatch = " android:versionCode=\"15\" ";

            IList<string> matches = RegexUtility.GetMatches(sourceString, searchRegex);
            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual(expectedMatch, matches.First());
        }
    }
}
