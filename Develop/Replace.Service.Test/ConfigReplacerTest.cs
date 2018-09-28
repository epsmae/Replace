using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Replace.Service.DataModel;

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

        private string TestDevelopDirectory
        {
            get { return Path.Combine(TestDataPath, "Develop"); }
        }

        private string TestCountDirectory
        {
            get { return Path.Combine(TestDataPath, "Count"); }
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
             
        }

        [TestMethod]
        public void TestReplace()
        {
            _replacer = new ConfigReplacer(GetTestReplaceConfig(TestDevelopDirectory));
            _replacer.Replace();

            AssertSameFileContent(ExpectedTestFile1, TestFile1);
            AssertSameFileContent(ExpectedTestFile2, TestFile2);
            AssertSameFileContent(ExpectedTestFile3, TestFile3);
        }

        [TestMethod]
        public void TestReplaceCount()
        {
            const int expectedRegexCount = 18;
            const int expectedFileCount = 6;

            _replacer = new ConfigReplacer(GetMinimalisticReplaceConfig(TestCountDirectory));

            ReplaceResult result = _replacer.Replace();
            Assert.AreEqual(expectedRegexCount, result.NumberOfReplacements);
            Assert.AreEqual(expectedFileCount, result.NumberOfAffectedFiles);
        }
    }
}
