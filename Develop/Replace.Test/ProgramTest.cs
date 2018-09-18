using System.IO;
using NUnit.Framework;
using Replace.App;
using Replace.Service;

namespace Replace.Test
{
    [TestFixture]
    public class ProgramTest : Testbase
    {
        private string TestData
        {
            get { return Path.Combine(TestSource, "Program"); }
        }

        private string ConfigFile
        {
            get { return Path.Combine(TestData, "AssemblyInfo.cs"); }
        }

        private string ExpectedConfigFile
        {
            get { return Path.Combine(TestData, "ExpectedAssemblyInfo.cs"); }
        }

        private string TestFile
        {
            get { return Path.Combine(TestData, "AndroidManifest.xml"); }
        }

        private string ExpectedTestFile
        {
            get { return Path.Combine(TestData, "ExpectedAndroidManifest.xml"); }
        }

        private string ConfigFileName
        {
            get { return Path.Combine(TestData, "Config.xml"); }
        }

        [Test]
        public void FileReplacerTest()
        {
            string[] p = {"-f", TestFile, "-s", "android:versionCode=\\\"[^\\s+\\t+]+?", "-r", "android:versionCode=\"15" };
            Program.Main(p);

            AssertSameFileContent(ExpectedTestFile, TestFile);
        }

        [Test]
        public void ConfigReplacerTest()
        {
            DeployConfig();

            string[] p = { "-c", ConfigFileName };
            Program.Main(p);
            AssertSameFileContent(ExpectedConfigFile, ConfigFile);
        }

        [Test]
        public void TestInvalidParameters()
        {
            using (StringWriter writer = new StringWriter())
            {
                string[] p = { "-c", "config.xml", "-f", "file.txt" };
                Program.Main(p, writer);

                Assert.True(writer.ToString().Contains("Usage:"));
            }
        }

        private void DeployConfig()
        {
            ConfigPersistence persistence = new ConfigPersistence();
            persistence.Save(ConfigFileName, GetTestConfig(TestData));
        }
    }
}
