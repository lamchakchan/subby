using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Parser;
using Subby.Core.Parser.Impl;
using Subby.Core.Test.Common;

namespace Subby.Core.Test
{
    [TestClass]
    public class XmlParserTest : BaseTest
    {
        IVariableParser parser = new XmlParser();

        [TestMethod]
        public void Parse_DictionaryContentWithSingleEntry_True()
        {
            var content = "<items>" +
                          " <item name=\"foo\" value=\"bar\" />" +
                          "</items>";
            var result = parser.Parse(content);

            Assert.IsTrue(result.Any());
            Assert.AreEqual("bar", result["foo"]);
        }

        [TestMethod]
        public void Parse_DictionaryContentWithMultipleEntry_True()
        {
            var content = "<items>" +
                          " <item name=\"foo\" value=\"bar\" />" +
                          " <item name=\"hello\" value=\"world\" />" +
                          " <item name=\"fat\" value=\"cat\" />" +
                          "</items>";
            var result = parser.Parse(content);

            Assert.IsTrue(result.Any());
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("bar", result["foo"]);
            Assert.AreEqual("world", result["hello"]);
            Assert.AreEqual("cat", result["fat"]);
        }

        [TestMethod]
        public void Parse_DictionaryContentWithDifferentTokenTypes_True()
        {
            var content = "<items>" +
                          " <item name=\"#foo#\" value=\"bar\" />" +
                          " <item name=\"$hello\" value=\"world\" />" +
                          " <item name=\"@fat@\" value=\"cat\" />" +
                          "</items>";
            var result = parser.Parse(content);

            Assert.IsTrue(result.Any());
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("bar", result["#foo#"]);
            Assert.AreEqual("world", result["$hello"]);
            Assert.AreEqual("cat", result["@fat@"]);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Parse_DictionaryContentWithDuplicateKeys_Fail()
        {
            var content = "<items>" +
                          " <item name=\"foo\" value=\"bar\" />" +
                          " <item name=\"foo\" value=\"world\" />" +
                          " <item name=\"fat\" value=\"cat\" />" +
                          "</items>";
            var result = parser.Parse(content);
        }

        [TestMethod]
        public void Parse_DictionaryContentWithChildrenEntries_True()
        {
            var content = "<items>" +
                          " <item name=\"foo\" value=\"bar\" />" +
                          " <item name=\"hello\" value=\"world\" />" +
                          " <item name=\"fat\" value=\"cat\">" +
                          "  <object name=\"sub1\" value=\"dog\" />" +
                          " </item>" +
                          "</items>";
            var result = parser.Parse(content);

            Assert.IsTrue(result.Any());
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Parse_DictionaryContentWithBadAttributes_Fail()
        {
            var content = "<items>" +
                          " <item attrib1=\"foo\" attrib2=\"bar\" />" +
                          "</items>";
            var result = parser.Parse(content);
        }
    }
}