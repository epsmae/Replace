using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Replace.Test
{
    
    [TestFixture]
    public class ReplaceTest
    {
        [Test]
        public void TestSimpleReplace()
        {
            const string replacement = "Unit Test";
            const string search = "Test String";
            const string source = "Replace Test String";
            const string expectedResult = "Replace Unit Test";

            string result = source.Replace(search, replacement);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
