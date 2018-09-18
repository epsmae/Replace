using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Replace.Service;
using Replace.Service.DataModel;
using Replace.Service.Test;

namespace Replace.App.Test
{
    [TestClass]
    public class ConfigTagReplacementTest : Testbase
    {
        private string TestSourcePath
        {
            get { return Path.Combine(TestDirectory, "TestSource", "ConfigTagReplacer"); }
        }

        private string TestDataPath
        {
            get { return Path.Combine(TestSourcePath, "TestData"); }
        }

        private string TestExecutionPath
        {
            get { return Path.Combine(TestDirectory, "ConfigTagReplacement"); }
        }

        private string ConfigPath
        {
            get { return Path.Combine(TestSourcePath, "value_config.xml"); }
        }

        private string ConfigFileName
        {
            get { return Path.Combine(TestDataPath, "Config.xml"); }
        }

        [TestInitialize]
        public void Setup()
        {
            CleanupTestDirectoy();
            DeployTestData(TestDataPath, TestExecutionPath);
        }

        [TestMethod]
        public void TestConfigReplacerParameter()
        {
            DeployConfig(ConfigPath, TestExecutionPath);

            string[] p = { "-c", ConfigFileName, "-t", "%0,1.0.1.5", "%1,15" };
            Program.Main(p);

            string expectedAssemblyInfoPath = Path.Combine(TestSourcePath, "ExpectedAssembly.cs");
            string actualdAssemblyInfoPath = Path.Combine(TestExecutionPath, "Assembly.cs");
            AssertSameFileContent(expectedAssemblyInfoPath, actualdAssemblyInfoPath);

            string expectedAndroidManifestPath = Path.Combine(TestSourcePath, "ExpectedAndroid.xml");
            string actualAndroidManifestPath = Path.Combine(TestExecutionPath, "Android.xml");
            AssertSameFileContent(expectedAndroidManifestPath, actualAndroidManifestPath);
        }

        private void CleanupTestDirectoy()
        {
            if (Directory.Exists(TestExecutionPath))
            {
                Directory.Delete(TestExecutionPath, true);
            }

            Directory.CreateDirectory(TestExecutionPath);
        }

        private void DeployConfig(string sourcePath, string destinationPath)
        {
            ConfigPersistence persistence = new ConfigPersistence();
            Config config = persistence.Load(sourcePath);
            config.PathToSearch = destinationPath;
            persistence.Save(ConfigFileName, config);
        }
    }
}
