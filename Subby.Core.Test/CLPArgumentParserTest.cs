using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Repl.Impl.CLP;
using Subby.Core.Test.Common;

namespace Subby.Core.Test
{
    [TestClass]
    public class CLPArgumentParserTest : BaseTest
    {
        [TestMethod]
        public void Parse_TypeArgument_True()
        {
            var testString = "json";

            var parser = new CLPArgumentParser();
            var argumentResult = parser.Parse(new[] { "-f", testString });

            Assert.AreEqual(testString, argumentResult.SourceType);
        }

        [TestMethod]
        public void Parse_DestinationArgument_True()
        {
            var testString = @"C:\Fake\Path\target.file";

            var parser = new CLPArgumentParser();
            var argumentResult = parser.Parse(new[] { "-d", testString });

            Assert.AreEqual(testString, argumentResult.DestinationFilePath);
        }

        [TestMethod]
        public void Parse_TargetArgument_True()
        {
            var testString = @"C:\Fake\Path\target.file";

            var parser = new CLPArgumentParser();
            var argumentResult = parser.Parse(new[] { "-t", testString });

            Assert.AreEqual(testString, argumentResult.TargetFilePath);
        }

        [TestMethod]
        public void Parse_SourceArgument_True()
        {
            var testString = @"C:\Fake\Path\target.file";

            var parser = new CLPArgumentParser();
            var argumentResult = parser.Parse(new[] { "-s", testString });

            Assert.IsTrue(argumentResult.SourceFilePaths.Any());
        }

        [TestMethod]
        public void Parse_SourceArgumentWithTwoValue_True()
        {
            var testString1 = @"C:\Fake\Path\target1.file";
            var testString2 = @"C:\Fake\Path\target2.file";

            var parser = new CLPArgumentParser();
            var argumentResult = parser.Parse(new[] { "-s", testString1 + ',' + testString2 });

            Assert.IsTrue(argumentResult.SourceFilePaths.Count == 2);
        }

        [TestMethod]
        public void Parse_SourceArgumentWithPrint_True()
        {
            var testString1 = @"C:\Fake\Path\target1.file";
            var testString2 = @"C:\Fake\Path\target2.file";

            var parser = new CLPArgumentParser();
            var argumentResult = parser.Parse(new[] { "-p", "-s", testString1 + ',' + testString2 });

            Assert.IsTrue(argumentResult.SourceFilePaths.Count == 2);
            Assert.IsTrue(argumentResult.Print);
        }
    }
}
