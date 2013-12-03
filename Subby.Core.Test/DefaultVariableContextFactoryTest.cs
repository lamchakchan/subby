using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Enum.Impl;
using Subby.Core.Factory;
using Subby.Core.Factory.Impl;
using Subby.Core.Model;
using Subby.Core.Service.Impl;

namespace Subby.Core.Test
{
    [TestClass]
    public class DefaultVariableContextFactoryTest
    {
        ICompositeVariablesContextFactory factory = new DefaultVariableContextFactory(new FileVariableContextFactory(new DefaultContentTypeService()), new HttpVariableContextFactory(new DefaultContentTypeService()));

        [TestMethod]
        public void Build_HasSingleFilePathValueWithContentType_True()
        {
            var result = factory.Build(SourceType.Json, new List<string> { "file1.json" });

            Assert.IsTrue(result.Count == 1);
            Assert.IsNotNull(result.First() as FileSourceContext);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasSingleUrlPathValueWithContentType_True()
        {
            var result = factory.Build(SourceType.Json, new List<string> { "http://localhost:10500/file1.json" });

            Assert.IsTrue(result.Count == 1);
            Assert.IsNotNull(result.First() as HttpSourceContext);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasFileAndUrlPathValueWithContentType_True()
        {
            var result = factory.Build(SourceType.Json, new List<string> { "http://localhost:10500/file1.json", "file2.json" });

            Assert.IsTrue(result.Count == 2);
            Assert.IsNotNull(result.First() as HttpSourceContext);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsNotNull(result.Skip(1).First() as FileSourceContext);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasTwoFilePathValueWithContentType_True()
        {
            var result = factory.Build(SourceType.Json, new List<string> { "file1.json", "file2.json" });

            Assert.IsTrue(result.Count == 2);
            Assert.IsNotNull(result.First() as FileSourceContext);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsNotNull(result.Skip(1).First() as FileSourceContext);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasTwoUrlPathValueWithContentType_True()
        {
            var result = factory.Build(SourceType.Json, new List<string> { "http://localhost:10500/file1.json", "http://localhost:10500/file2.json" });

            Assert.IsTrue(result.Count == 2);
            Assert.IsNotNull(result.First() as HttpSourceContext);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsNotNull(result.Skip(1).First() as HttpSourceContext);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasSingleFilePathValueWithNoContentType_True()
        {
            var result = factory.Build(null, new List<string> { "file1.json" });

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasSingleUrlePathValueWithNoContentType_True()
        {
            var result = factory.Build(null, new List<string> { "http://localhost:10500/file1.json" });

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasTwoFilePathWithDifferentExtensionValueWithNoContentType_True()
        {
            var result = factory.Build(null, new List<string> { "file1.json", "file2.xml" });

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Xml.Value);
        }

        [TestMethod]
        public void Build_HasTwoUrlPathWithDifferentExtensionValueWithNoContentType_True()
        {
            var result = factory.Build(null, new List<string> { "http://localhost:10500/file1.json", "http://localhost:10500/file2.xml" });

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Xml.Value);
        }

        [TestMethod]
        public void Build_HasThreeFilePathWithDifferentExtensionValueWithNoContentType_True()
        {
            var result = factory.Build(null, new List<string> { "file1.json", "file2.xml", "file2.txt" });

            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Xml.Value);
            Assert.IsTrue(result.Skip(2).First().Type.Value == SourceType.NewLineDelimited.Value);
        }

        [TestMethod]
        public void Build_HasThreeUrlPathWithDifferentExtensionValueWithNoContentType_True()
        {
            var result = factory.Build(null, new List<string> { "http://localhost:10500/file1.json", "http://localhost:10500/file2.xml", "http://localhost:10500/file2.txt" });

            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Xml.Value);
            Assert.IsTrue(result.Skip(2).First().Type.Value == SourceType.NewLineDelimited.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Build_HasNoPathValue_True()
        {
            var result = factory.Build(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Build_HasEmptyPathValue_True()
        {
            var result = factory.Build(null, new List<string>());
        }
    }
}