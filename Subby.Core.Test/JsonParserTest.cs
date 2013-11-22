using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Parser;
using Subby.Core.Parser.Impl;

namespace Subby.Core.Test
{
    [TestClass]
    public class JsonParserTest
    {
        IVariableParser parser = new JsonParser();

        [TestMethod]
        public void Parse_DictionaryContentWithSingleEntry_True()
        {
            var content = "{'foo': 'bar'}";
            var result = parser.Parse(content);

            Assert.IsTrue(result.Any());
            Assert.AreEqual("bar", result["foo"]);
        }

        [TestMethod]
        public void Parse_DictionaryContentWithMultipleEntry_True()
        {
            var content = "{'foo': 'bar', 'hello':'world', 'fat':'cat'}";
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
            var content = "{'#foo#': 'bar', '$hello':'world', '@fat@':'cat'}";
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
            var content = "{'foo': 'bar', 'foo':'world', 'fat':'cat'}";
            var result = parser.Parse(content);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Parse_DictionaryContentWithChildrenEntries_Fail()
        {
            var content = "{'foo': 'bar', 'hello':'world', 'fat':{'sub1':'cat', 'sub2':'dog'}}";
            parser.Parse(content);
        }
    }
}
