using System.IO;
using NUnit.Framework;
using Replace.App;
using Replace.Service;
using Replace.Service.DataModel;

namespace Replace.Test.ConfigTagReplacement
{
    [TestFixture]
    public class ConfigTagReplacementTest : Testbase
    {
        private string TestDataPath
        {
            get { return Path.Combine(TestDirectory, "ConfigTagReplacement", "TestData"); }
        }

        private string TestExecutionPath
        {
            get { return Path.Combine(TestDirectory, "ConfigTagReplacement", "TestExecutionPath"); }
        }

        private string TestConfigPath
        {
            get { return Path.Combine(TestDirectory, "ConfigTagReplacement", "TestConfig", "value_config.xml"); }
        }

        private string ExpectedResultDataPath
        {
            get { return Path.Combine(TestDirectory, "ConfigTagReplacement", "ExpectedResultData"); }
        }

        private string ConfigFileName
        {
            get { return Path.Combine(TestDataPath, "Config.xml"); }
        }

        [SetUp]
        public void Setup()
        {
            CleanupTestDirectoy();
            DeployTestData(TestDataPath, TestExecutionPath);
        }

        private void CleanupTestDirectoy()
        {
            if (Directory.Exists(TestExecutionPath))
            {
                Directory.Delete(TestExecutionPath, true);
            }

            Directory.CreateDirectory(TestExecutionPath);
        }

        private void DeployTestData(string sourceDirectory, string destinationDirectory)
        {
            foreach (string filePath in Directory.EnumerateFiles(sourceDirectory))
            {
                File.Copy(filePath, Path.Combine(destinationDirectory, Path.GetFileName(filePath)));
            }
        }

        [Test]
        public void ConfigReplacerTestParameter()
        {
            DeployConfig(TestConfigPath, TestExecutionPath);

            string[] p = { "-c", ConfigFileName, "-t", "%0,1.0.1.5", "%1,15" };
            Program.Main(p);

            string expectedAssemblyInfoPath = Path.Combine(ExpectedResultDataPath, "AssemblyInfo.cs");
            string actualdAssemblyInfoPath = Path.Combine(TestExecutionPath, "AssemblyInfo.cs");
            AssertSameFileContent(expectedAssemblyInfoPath, actualdAssemblyInfoPath);

            string expectedAndroidManifestPath = Path.Combine(ExpectedResultDataPath, "AndroidManifest.xml");
            string actualAndroidManifestPath = Path.Combine(TestExecutionPath, "AndroidManifest.xml");
            AssertSameFileContent(expectedAndroidManifestPath, actualAndroidManifestPath);
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
