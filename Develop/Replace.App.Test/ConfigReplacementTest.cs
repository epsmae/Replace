using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Replace.Service;
using Replace.Service.Test;

namespace Replace.App.Test
{
    [TestClass]
    public class ConfigReplacementTest : Testbase
    {
        private string TestSourcePath
        {
            get { return Path.Combine(TestDirectory, "TestSource", "ConfigReplacer"); }
        }

        private string TestData
        {
            get { return Path.Combine(TestSourcePath, "TestData"); }
        }

        private string ActualFile
        {
            get { return Path.Combine(TestExecutionPath, "Assembly.cs"); }
        }

        private string ExpectedFile
        {
            get { return Path.Combine(TestSourcePath, "ExpectedAssembly.cs"); }
        }

        private string ConfigFileName
        {
            get { return Path.Combine(TestExecutionPath, "Config.xml"); }
        }

        private string TestExecutionPath
        {
            get { return Path.Combine(TestDirectory, "ConfigReplacement"); }
        }

        [TestInitialize]
        public void Setup()
        {
            CleanupTestDirectoy(TestExecutionPath);
            DeployTestData(TestData, TestExecutionPath);
        }

        [TestMethod]
        public void ConfigReplacerTest()
        {
            DeployConfig(TestExecutionPath);

            string[] p = { "-c", ConfigFileName };
            Program.Main(p);
            
            AssertSameFileContent(ExpectedFile, ActualFile);
        }

        private void DeployConfig(string path)
        {
            ConfigPersistence persistence = new ConfigPersistence();
            persistence.Save(ConfigFileName, GetTestConfig(path));
        }
    }
}
