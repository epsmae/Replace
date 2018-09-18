using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Replace.Service.DataModel;

namespace Replace.Service.Test
{
    public class Testbase
    {
        protected string TestDirectory
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory); }
        }

        protected string TestSource
        {
            get { return Path.Combine(TestDirectory, "TestSource"); }
        }

        protected void AssertSameFileContent(string expectedFileFilePath, string actualFileFilePath)
        {
            string expectedContent = System.IO.File.ReadAllText(expectedFileFilePath);
            string actualContent = System.IO.File.ReadAllText(actualFileFilePath);
            Assert.AreEqual(expectedContent, actualContent);
        }

        protected void CleanupTestDirectoy(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Directory.CreateDirectory(path);
        }

        protected void DeployTestData(string sourceDirectory, string destinationDirectory)
        {
            foreach (string filePath in Directory.EnumerateFiles(sourceDirectory))
            {
                File.Copy(filePath, Path.Combine(destinationDirectory, Path.GetFileName(filePath)));
            }
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
                    "AssemblyFile.txt",
                    "Assembly.cs",
                    "Assembly.as",
                },
                PathToSearch = path
            };
        }
    }
}
