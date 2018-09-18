using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Replace.Service;
using Replace.Service.Test;

namespace Replace.App.Test
{
    [TestClass]
    public class ParameterTest : Testbase
    {
        [TestMethod]
        public void TestInvalidParameters()
        {
            using (StringWriter writer = new StringWriter())
            {
                string[] p = { "-c", "config.xml", "-f", "file.txt" };
                Program.Main(p, writer);

                Assert.IsTrue(writer.ToString().Contains("Usage:"));
            }
        }
    }
}
