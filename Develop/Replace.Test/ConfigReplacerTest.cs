using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Replace.DataModel;

namespace Replace.Test
{
    [TestFixture]
    public class ConfigReplacerTest
    {
        private ConfigFileReplacer _replacer;

        private string TestDataPath
        {
            get { return Path.Combine(TestContext.CurrentContext.TestDirectory, "ConfigReplacerTestSource"); }
        }

        private string TestDirectory
        {
            get { return Path.Combine(TestDataPath, "Develop"); }
        }

        private string TestFile1
        {
            get { return Path.Combine(TestDataPath, "Develop", "AssemblyInfo.cs"); }
        }
        private string ExpectedTestFile1
        {
            get { return Path.Combine(TestDataPath, "Develop", "ExpectedAssemblyInfo.cs"); }
        }

        private string TestFile2
        {
            get { return Path.Combine(TestDataPath, "Develop", "App", "AssemblyFile.txt"); }
        }
        private string ExpectedTestFile2
        {
            get { return Path.Combine(TestDataPath, "Develop", "App", "ExpectedAssemblyFile.txt"); }
        }

        private string TestFile3
        {
            get { return Path.Combine(TestDataPath, "Develop", "App", "Config", "Assembly.as"); }
        }
        private string ExpectedTestFile3
        {
            get { return Path.Combine(TestDataPath, "Develop", "App", "Config", "ExpectedAssembly.as"); }
        }

        [SetUp]
        public void Setup()
        {
            _replacer = new ConfigFileReplacer(TestConfig);    
        }

        [Test]
        public void TestReplace()
        {
            _replacer.Replace();

            AssertSameFileContent(ExpectedTestFile1, TestFile1);
            AssertSameFileContent(ExpectedTestFile2, TestFile2);
            AssertSameFileContent(ExpectedTestFile3, TestFile3);
        }

        private void AssertSameFileContent(string expectedFileFilePath, string actualFileFilePath)
        {
            string expectedContent = File.ReadAllText(expectedFileFilePath);
            string actualContent = File.ReadAllText(actualFileFilePath);
            Assert.AreEqual(expectedContent, actualContent);
        }

        private Config TestConfig
        {
            get
            {
                return new Config
                {
                    RegexReplaceValues = new List<RegexReplaceValue>
                    {
                        new RegexReplaceValue{Regex = "AssemblyCompany.+?]", ReplaceValue = "AssemblyCompany(\\\"Replace AG\\\")]"},
                        new RegexReplaceValue{Regex = "AssemblyCopyright.+?]", ReplaceValue = "AssemblyCopyright(\\\"Copyright Replace AG\\\")]"},
                        new RegexReplaceValue{Regex = "AssemblyProduct.+?]", ReplaceValue = "AssemblyProduct(\\\"Replace\\\")]"},
                        new RegexReplaceValue{Regex = "AssemblyVersion.+?]", ReplaceValue = "AssemblyVersion(\\\"0.0.3.4\\\")]"},
                        new RegexReplaceValue{Regex = "AssemblyFileVersion.+?]", ReplaceValue = "AssemblyFileVersion(\\\"0.0.1.2\\\")]"},
                        new RegexReplaceValue{Regex = "AssemblyCulture.+?]", ReplaceValue = "AssemblyCulture(\\\"Culture\\\")]"},
                        new RegexReplaceValue{Regex = "AssemblyTrademark.+?]", ReplaceValue = "AssemblyTrademark(\\\"Trademark\\\")]"},
                    },
                    FileExtensions = new List<string>
                    {
                        "AssemblyInfo.cs",
                        "AssemblyFile.txt",
                        "Assembly.as"
                    },
                    PathToSearch = TestDirectory
                };
            }
        }
    }
}
