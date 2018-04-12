using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Replace.DataModel;

namespace Replace.Test
{
    [TestFixture]
    public class ConfigReplacerTest : Testbase
    {
        private ConfigFileReplacer _replacer;

        private string TestDataPath
        {
            get { return Path.Combine(TestSource, "ConfigReplacer"); }
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
            _replacer = new ConfigFileReplacer(GetTestConfig(TestDirectory));    
        }

        [Test]
        public void TestReplace()
        {
            _replacer.Replace();

            AssertSameFileContent(ExpectedTestFile1, TestFile1);
            AssertSameFileContent(ExpectedTestFile2, TestFile2);
            AssertSameFileContent(ExpectedTestFile3, TestFile3);
        }
    }
}
