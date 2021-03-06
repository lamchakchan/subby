﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Enum.Impl;
using Subby.Core.Factory;
using Subby.Core.Factory.Impl;
using Subby.Core.Service.Impl;
using Subby.Core.Test.Common;

namespace Subby.Core.Test
{
    [TestClass]
    public class HttpVariableContextFactoryTest : BaseTest
    {
        IHttpVariableContextFactory factory = new HttpVariableContextFactory(new DefaultContentTypeService());

        [TestMethod]
        public void Build_HasSinglePathValueWithContentType_True()
        {
            var result = factory.Build(SourceType.Json, new List<string> { "http://localhost:10500/file1.json" });

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasTwoPathValueWithContentType_True()
        {
            var result = factory.Build(SourceType.Json, new List<string> { "http://localhost:10500/file1.json", "http://localhost:10500/file2.json" });

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasSinglePathValueWithNoContentType_True()
        {
            var result = factory.Build(null, new List<string> { "http://localhost:10500/file1.json" });

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void Build_HasTwoPathWithDifferentExtensionValueWithNoContentType_True()
        {
            var result = factory.Build(null, new List<string> { "http://localhost:10500/file1.json", "http://localhost:10500/file2.xml" });

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Xml.Value);
        }

        [TestMethod]
        public void Build_HasThreePathWithDifferentExtensionValueWithNoContentType_True()
        {
            var result = factory.Build(null, new List<string> { "http://localhost:10500/file1.json", "http://localhost:10500/file2.xml", "http://localhost:10500/file2.txt" });

            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
            Assert.IsTrue(result.Skip(1).First().Type.Value == SourceType.Xml.Value);
            Assert.IsTrue(result.Skip(2).First().Type.Value == SourceType.NewLineDelimited.Value);
        }

        [TestMethod]
        public void Build_HasSinglePathValueWithUnknownContentType_True()
        {
            var result = factory.Build(SourceType.Unknown, new List<string> { "http://localhost:10500/file1.json" });

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Type.Value == SourceType.Json.Value);
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