using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Replace.Service.Test
{
    [TestClass]
    public class ConfigReplacerTest : Testbase
    {
        private ConfigReplacer _replacer;

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
            get { return Path.Combine(TestDataPath, "Develop", "Assembly.cs"); }
        }
        private string ExpectedTestFile1
        {
            get { return Path.Combine(TestDataPath, "Develop", "ExpectedAssembly.cs"); }
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

        [TestInitialize]
        public void Setup()
        {
            _replacer = new ConfigReplacer(GetTestReplaceConfig(TestDirectory));    
        }

        [TestMethod]
        public void TestReplace()
        {
            _replacer.Replace();

            AssertSameFileContent(ExpectedTestFile1, TestFile1);
            AssertSameFileContent(ExpectedTestFile2, TestFile2);
            AssertSameFileContent(ExpectedTestFile3, TestFile3);
        }
    }
}
