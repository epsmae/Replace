using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Replace.Service.Test;

namespace Replace.App.Test
{
    [TestClass]
    public class FileReplacementTest : Testbase
    {
        private string TestData
        {
            get { return Path.Combine(TestSource, "FileReplacer"); }
        }

        private string TestFile
        {
            get { return Path.Combine(TestExecutionPath, "Android.xml"); }
        }

        private string ExpectedTestFile
        {
            get { return Path.Combine(TestData, "ExpectedAndroid.xml"); }
        }

        private string TestExecutionPath
        {
            get { return Path.Combine(TestDirectory, "FileReplacement"); }
        }

        [TestInitialize]
        public void Setup()
        {
            CleanupTestDirectoy(TestExecutionPath);
            DeployTestData(TestData, TestExecutionPath);
        }

        [TestMethod]
        public void TestReplaceInFile()
        {
            string[] p = {"-f", TestFile, "-s", "android:versionCode=\\\"[^\\s+\\t+]+?", "-r", "android:versionCode=\"15" };
            Program.Main(p);

            AssertSameFileContent(ExpectedTestFile, TestFile);
        }
    }
}
