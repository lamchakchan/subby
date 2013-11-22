using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Enum.Impl;
using Subby.Core.Service.Impl;

namespace Subby.Core.Test
{
    [TestClass]
    public class DefaultContentTypeServiceTest
    {
        [TestMethod]
        public void ProcessFilePath_JsonFilePathLowerCase_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessFilePath(@"c:\folder\path\file.json");

            Assert.IsTrue(result.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void ProcessFilePath_JsonFilePathUpperCase_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessFilePath(@"C:\FOLDER\PATH\FILE.JSON");

            Assert.IsTrue(result.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void ProcessFilePath_JsFilePathLowerCase_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessFilePath(@"c:\folder\path\file.js");

            Assert.IsTrue(result.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void ProcessFilePath_JsFilePathUpperCase_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessFilePath(@"C:\FOLDER\PATH\FILE.JS");

            Assert.IsTrue(result.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void ProcessFilePath_XmlFilePathLowerCase_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessFilePath(@"c:\folder\path\file.xml");

            Assert.IsTrue(result.Value == SourceType.Xml.Value);
        }

        [TestMethod]
        public void ProcessFilePath_XmlFilePathUpperCase_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessFilePath(@"C:\FOLDER\PATH\FILE.XML");

            Assert.IsTrue(result.Value == SourceType.Xml.Value);
        }

        [TestMethod]
        public void ProcessFilePath_TxtFilePathLowerCase_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessFilePath(@"c:\folder\path\file.txt");

            Assert.IsTrue(result.Value == SourceType.NewLineDelimited.Value);
        }

        [TestMethod]
        public void ProcessFilePath_TxtFilePathUpperCase_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessFilePath(@"C:\FOLDER\PATH\FILE.TXT");

            Assert.IsTrue(result.Value == SourceType.NewLineDelimited.Value);
        }

        [TestMethod]
        public void ProcessFilePath_UnknownExtension_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessFilePath(@"C:\FOLDER\PATH\FILE.SQL");

            Assert.IsTrue(result.Value == SourceType.Unknown.Value);
        }

        [TestMethod]
        public void ProcessCode_JsonLowerCase_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessCode("json");

            Assert.IsTrue(result.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void ProcessCode_JsonUpperCase_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessCode("JSON");

            Assert.IsTrue(result.Value == SourceType.Json.Value);
        }

        [TestMethod]
        public void ProcessCode_EmptyValue_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessCode(string.Empty);

            Assert.IsTrue(result.Value == SourceType.Unknown.Value);
        }

        [TestMethod]
        public void ProcessCode_NullValue_True()
        {
            var service = new DefaultContentTypeService();
            var result = service.ProcessCode(null);

            Assert.IsTrue(result.Value == SourceType.Unknown.Value);
        }
    }
}
