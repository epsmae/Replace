using System.IO;
using NUnit.Framework;
using Replace.Service;

namespace Replace.Test
{
    [TestFixture]
    public class FileReplacerTest : Testbase
    {
        private string TestDataPath
        {
            get { return Path.Combine(TestSource, "FileReplacer"); }
        }

        [Test]
        public void TestDotNetStandard()
        {
            const string searchRegex = "<Version>.*</Version>";
            const string replacement = "<Version>7.5.1.2</Version>";

            string expectedTestFilePath = Path.Combine(TestDataPath, "expected_dot_net_standard_2_0.csproj");
            string testFilePath = Path.Combine(TestDataPath, "dot_net_standard_2_0.csproj");
            string testOutputFilePath = Path.Combine(TestDataPath, "dot_net_standard_2_0.csproj_replaced");
            
            FileReplacer replacer = new FileReplacer(testFilePath);
            
            int replaceCount = replacer.Replace(searchRegex, replacement, testOutputFilePath);
            Assert.AreEqual(1, replaceCount);

            AssertSameFileContent(expectedTestFilePath, testOutputFilePath);
        }
    }
}
