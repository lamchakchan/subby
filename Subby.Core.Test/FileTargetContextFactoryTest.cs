using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Enum.Impl;
using Subby.Core.Factory.Impl;
using Subby.Core.Service.Impl;

namespace Subby.Core.Test
{
    [TestClass]
    public class FileTargetContextFactoryTest
    {
        [TestMethod]
        public void Build_HasPathValue_True()
        {
            var factory = new FileTargetContextFactory();
            var result = factory.Build("abc");

            Assert.IsTrue(!string.IsNullOrEmpty(result.FilePath));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Build_HasNoPathValue_False()
        {
            var factory = new FileTargetContextFactory();
            var result = factory.Build(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Build_HasEmptyPathValue_False()
        {
            var factory = new FileTargetContextFactory();
            var result = factory.Build(string.Empty);
        }
    }

    [TestClass]
    public class FileVariableContextFactoryTest
    {
        [TestMethod]
        public void Build_HasSinglePathValueWithContentType_True()
        {
            var factory = new FileVariableContextFactory(new DefaultContentTypeService());
            var result = factory.Build(SourceType.Json, new List<string>{"file1.json"});

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasTwoPathValueWithContentType_True()
        {
            var factory = new FileVariableContextFactory(new DefaultContentTypeService());
            var result = factory.Build(SourceType.Json, new List<string> { "file1.json", "file2.json" });

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasSinglePathValueWithNoContentType_True()
        {
            var factory = new FileVariableContextFactory(new DefaultContentTypeService());
            var result = factory.Build(null, new List<string> { "file1.json" });

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasTwoPathWithDifferentExtensionValueWithNoContentType_True()
        {
            var factory = new FileVariableContextFactory(new DefaultContentTypeService());
            var result = factory.Build(null, new List<string> { "file1.json", "file2.xml" });

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Xml.Value);
        }

        [TestMethod]
        public void Build_HasThreePathWithDifferentExtensionValueWithNoContentType_True()
        {
            var factory = new FileVariableContextFactory(new DefaultContentTypeService());
            var result = factory.Build(null, new List<string> { "file1.json", "file2.xml", "file2.txt" });

            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Xml.Value);
            Assert.IsTrue(result.Skip(2).First().Type.Value == SourceType.NewLineDelimited.Value);
        }

        [TestMethod]
        public void Build_HasSinglePathValueWithUnknownContentType_True()
        {
            var factory = new FileVariableContextFactory(new DefaultContentTypeService());
            var result = factory.Build(SourceType.Unknown, new List<string> { "file1.json" });

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Build_HasNoPathValue_True()
        {
            var factory = new FileVariableContextFactory(new DefaultContentTypeService());
            var result = factory.Build(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Build_HasEmptyPathValue_True()
        {
            var factory = new FileVariableContextFactory(new DefaultContentTypeService());
            var result = factory.Build(null, new List<string>());
        }
    }
}
