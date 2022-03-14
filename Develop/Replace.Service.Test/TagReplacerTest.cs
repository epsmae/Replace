using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Replace.Service.DataModel;

namespace Replace.Service.Test
{
    [TestClass]
    public class TagReplacerTest
    {
        [TestMethod]
        public void TestReplaceFileExtensionTags()
        {
            Config config = new Config();
            config.FileNames.Add("#0.cs");
            config.FileNames.Add("#1.txt");
            config.TagReplacements.Add(new KeyValuePair<string, string>("#0", "Tag1"));
            config.TagReplacements.Add(new KeyValuePair<string, string>("#1", "Tag2"));

            ReplaceConfig replaceConfig = TagReplacer.ReplaceTags(config);
            Assert.AreEqual("Tag1.cs", replaceConfig.FileExtensions[0]);
            Assert.AreEqual("Tag2.txt", replaceConfig.FileExtensions[1]);
        }

        [TestMethod]
        public void TestReplaceRegexValueTags()
        {
            const string expectedValue = "0.1.2.3";
            Config config = new Config();
            config.RegexReplaceValues.Add(new RegexReplaceValue{Regex = "version_regex", ReplaceValue = "#0"});
            config.TagReplacements.Add(new KeyValuePair<string, string>("#0", expectedValue));

            ReplaceConfig replaceConfig = TagReplacer.ReplaceTags(config);
            Assert.AreEqual(expectedValue, replaceConfig.RegexReplaceValues[0].ReplaceValue);
        }


        [TestMethod]
        public void TestReplaceRegexValueMultipleTags()
        {
            const string expectedValuePart1 = "0.1.2";
            const string expectedValuePart2 = ".3";
            Config config = new Config();
            config.RegexReplaceValues.Add(new RegexReplaceValue { Regex = "version_regex", ReplaceValue = "#0#1" });
            config.TagReplacements.Add(new KeyValuePair<string, string>("#0", expectedValuePart1));
            config.TagReplacements.Add(new KeyValuePair<string, string>("#1", expectedValuePart2));

            ReplaceConfig replaceConfig = TagReplacer.ReplaceTags(config);
            Assert.AreEqual($"{expectedValuePart1}{expectedValuePart2}", replaceConfig.RegexReplaceValues[0].ReplaceValue);
        }



        [TestMethod]
        public void TestReplaceRegexTags()
        {
            const string expectedRegex = "regex";
            Config config = new Config();
            config.RegexReplaceValues.Add(new RegexReplaceValue { Regex = "#0", ReplaceValue = "value" });
            config.TagReplacements.Add(new KeyValuePair<string, string>("#0", expectedRegex));

            ReplaceConfig replaceConfig = TagReplacer.ReplaceTags(config);
            Assert.AreEqual(expectedRegex, replaceConfig.RegexReplaceValues[0].Regex);
        }

        [TestMethod]
        public void TestReplacePathTag()
        {
            Config config = new Config();
            config.PathToSearch = "#0/Project";
            config.TagReplacements.Add(new KeyValuePair<string, string>("#0", "Customer"));

            ReplaceConfig replaceConfig = TagReplacer.ReplaceTags(config);
            Assert.AreEqual("Customer/Project", replaceConfig.PathToSearch);
        }
    }
}
