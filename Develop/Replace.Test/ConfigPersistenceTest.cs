using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Replace.Service;
using Replace.Service.DataModel;

namespace Replace.Test
{
    [TestFixture]
    public class ConfigPersistenceTest : Testbase
    {
        private string TestDataPath
        {
            get { return Path.Combine(TestSource, "config.xml"); }
        }

        private ConfigPersistence _persistence;

        [SetUp]
        public void Setup()
        {
            _persistence = new ConfigPersistence();            
        }

        [Test]
        public void TestSerializeDeserialize()
        {
            Config config = new Config()
            {
                RegexReplaceValues = new List<RegexReplaceValue>()
                {
                    new RegexReplaceValue{Regex = "Regex1", ReplaceValue = "Value1"},
                    new RegexReplaceValue{Regex = "Regex2", ReplaceValue = "Value2"},
                    new RegexReplaceValue{Regex = "Regex3", ReplaceValue = "Value3"}
                },
                FileNames = new List<string>()
                {
                    ".png", ".txt"
                },
                PathToSearch = "..\\..\\Develop"
            };
            
            _persistence.Save(TestDataPath, config);
            Config loadedConfig = _persistence.Load(TestDataPath);
            AssertAreEqual(config, loadedConfig);

        }

        private void AssertAreEqual(Config expected, Config actual)
        {
            AssertAreEqual(expected.RegexReplaceValues, actual.RegexReplaceValues);
            AssertAreEqual(expected.FileNames, actual.FileNames);
            Assert.AreEqual(expected.PathToSearch, actual.PathToSearch);
        }

        private void AssertAreEqual(List<string> expected, List<string> actual)
        {
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        private void AssertAreEqual(List<RegexReplaceValue> expected, List<RegexReplaceValue> actual)
        {
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Regex, actual[i].Regex);
                Assert.AreEqual(expected[i].ReplaceValue, actual[i].ReplaceValue);
            }
        }
    }
}
