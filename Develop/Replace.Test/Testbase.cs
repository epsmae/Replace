using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Replace.Service.DataModel;

namespace Replace.Test
{
    public class Testbase
    {
        protected string TestDirectory
        {
            get { return Path.Combine(TestContext.CurrentContext.TestDirectory); }
        }

        protected string TestSource
        {
            get { return Path.Combine(TestContext.CurrentContext.TestDirectory, "TestSource"); }
        }

        protected void AssertSameFileContent(string expectedFileFilePath, string actualFileFilePath)
        {
            string expectedContent = System.IO.File.ReadAllText(expectedFileFilePath);
            string actualContent = System.IO.File.ReadAllText(actualFileFilePath);
            Assert.AreEqual(expectedContent, actualContent);
        }

        protected Config GetTestConfig(string path)
        {
            return new Config
            {
                RegexReplaceValues = new List<RegexReplaceValue>
                {
                    new RegexReplaceValue
                    {
                        Regex = "AssemblyCompany.+?]",
                        ReplaceValue = "AssemblyCompany(\"Replace AG\")]"
                    },
                    new RegexReplaceValue
                    {
                        Regex = "AssemblyCopyright.+?]",
                        ReplaceValue = "AssemblyCopyright(\"Copyright Replace AG\")]"
                    },
                    new RegexReplaceValue
                    {
                        Regex = "AssemblyProduct.+?]",
                        ReplaceValue = "AssemblyProduct(\"Replace\")]"
                    },
                    new RegexReplaceValue
                    {
                        Regex = "AssemblyVersion.+?]",
                        ReplaceValue = "AssemblyVersion(\"0.0.3.4\")]"
                    },
                    new RegexReplaceValue
                    {
                        Regex = "AssemblyFileVersion.+?]",
                        ReplaceValue = "AssemblyFileVersion(\"0.0.1.2\")]"
                    },
                    new RegexReplaceValue
                    {
                        Regex = "AssemblyCulture.+?]",
                        ReplaceValue = "AssemblyCulture(\"Culture\")]"
                    },
                    new RegexReplaceValue
                    {
                        Regex = "AssemblyTrademark.+?]",
                        ReplaceValue = "AssemblyTrademark(\"Trademark\")]"
                    },
                },
                FileExtensions = new List<string>
                {
                    "AssemblyInfo.cs",
                    "AssemblyFile.txt",
                    "Assembly.as"
                },
                PathToSearch = path
            };
        }

        protected ReplaceConfig GetTestReplaceConfig(string path)
        {
            Config config = GetTestConfig(path);
            return new ReplaceConfig()
            {
                FileExtensions = config.FileExtensions,
                PathToSearch = config.PathToSearch,
                RegexReplaceValues = config.RegexReplaceValues
            };
        }
    }
}
