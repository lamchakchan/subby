using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Parser;
using Subby.Core.Parser.Impl;
using Subby.Core.Test.Common;

namespace Subby.Core.Test
{
    [TestClass]
    public class NewLineParserTest : BaseTest
    {
        IVariableParser parser = new NewLineParser();

        [TestMethod]
        public void Parse_DictionaryContentWithSingleEntry_True()
        {
            var content = "foo" + Environment.NewLine +
                          "bar";
            var result = parser.Parse(content);

            Assert.IsTrue(result.Any());
            Assert.AreEqual("bar", result["foo"]);
        }

        [TestMethod]
        public void Parse_DictionaryContentWithMultipleEntry_True()
        {
            var content = "foo" + Environment.NewLine +
                          "bar" + Environment.NewLine +
                          "hello" + Environment.NewLine +
                          "world" + Environment.NewLine +
                          "fat" + Environment.NewLine +
                          "cat";
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
            var content = "#foo#" + Environment.NewLine +
                          "bar" + Environment.NewLine +
                          "$hello" + Environment.NewLine +
                          "world" + Environment.NewLine +
                          "@fat@" + Environment.NewLine +
                          "cat";
            var result = parser.Parse(content);

            Assert.IsTrue(result.Any());
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("bar", result["#foo#"]);
            Assert.AreEqual("world", result["$hello"]);
            Assert.AreEqual("cat", result["@fat@"]);
        }

        [TestMethod]
        public void Parse_DictionaryContentUnevenKeyValueLines_True()
        {
            var content = "#foo#" + Environment.NewLine +
                          "bar" + Environment.NewLine +
                          "$hello" + Environment.NewLine +
                          "world" + Environment.NewLine +
                          "@fat@" + Environment.NewLine +
                          "cat" + Environment.NewLine +
                          "@uneven";
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
            var content = "foo" + Environment.NewLine +
                          "bar" + Environment.NewLine +
                          "foo" + Environment.NewLine +
                          "world" + Environment.NewLine +
                          "fat" + Environment.NewLine +
                          "cat";
            var result = parser.Parse(content);
        }
    }
}