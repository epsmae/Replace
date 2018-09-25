using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Replace.Service.Test
{
    [TestClass]
    public class AsterisksMatcherTest
    {
        private AsterisksMatcher _matcher;

        [TestInitialize]
        public void Initialize()
        {
            _matcher = new AsterisksMatcher();
        }

        [TestMethod]
        public void TestNoAsterisks()
        {
            Assert.IsFalse(_matcher.IsMatch("csproj", "replace.csproj"));
            Assert.IsTrue(_matcher.IsMatch("replace.csproj", "replace.csproj"));
        }

        [TestMethod]
        public void TestAsterisksStart()
        {
            Assert.IsFalse(_matcher.IsMatch(".csproj", "replace.csproj"));
            Assert.IsTrue(_matcher.IsMatch("*.csproj", "replace.csproj"));
        }

        [TestMethod]
        public void TestAsterisksEnd()
        {
            Assert.IsTrue(_matcher.IsMatch("replace.cs*", "replace.csproj"));
        }

        [TestMethod]
        public void TestAsterisksMiddle()
        {
            Assert.IsTrue(_matcher.IsMatch("replace*csproj", "replace.csproj"));
            Assert.IsFalse(_matcher.IsMatch("csproj*replace", "replace.csproj"));
        }

        [TestMethod]
        public void TestMultipleAsterisks()
        {
            Assert.IsTrue(_matcher.IsMatch("re*ace*proj", "replace.csproj"));
            Assert.IsFalse(_matcher.IsMatch("ace*re*proj", "replace.csproj"));
        }

        [TestMethod]
        public void TestMultipleExpected()
        {
            IList<string> expected = new List<string>(){ "*.csproj", "*.assembly", "*.jpg", "config.xml"};

            Assert.IsTrue(_matcher.IsMatch(expected, "config.xml"));
            Assert.IsTrue(_matcher.IsMatch(expected, "Replace.csproj"));
        }
    }
}
