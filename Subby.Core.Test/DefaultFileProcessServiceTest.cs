using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Enum.Impl;
using Subby.Core.Model;
using Subby.Core.Parser.Impl;
using Subby.Core.Service.Impl;

namespace Subby.Core.Test
{
    [TestClass]
    [DeploymentItem("Files", "Files")]
    public class DefaultFileProcessServiceTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void ReadTarget_LoadFileHasContent_True()
        {
            var service = new DefaultFileProcessService(new JsonParser(), new XmlParser());
            var content = service.Read(new FileTargetContext { FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\target.txt") });
            Assert.IsTrue(!string.IsNullOrEmpty(content));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadTarget_LoadNonExistentFile_Fail()
        {
            var service = new DefaultFileProcessService(new JsonParser(), new XmlParser());
            var content = service.Read(new FileTargetContext { FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\nofile.txt") });
        }

        [TestMethod]
        public void ReadSource_LoadJsonFileHasContent_True()
        {
            var service = new DefaultFileProcessService(new JsonParser(), new XmlParser());
            var content = service.Read(
                new List<FileSourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.Json, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.json")
                    }
                });
            Assert.IsTrue(content.Any());
        }

        [TestMethod]
        public void ReadSource_LoadXmlFileHasContent_True()
        {
            var service = new DefaultFileProcessService(new JsonParser(), new XmlParser());
            var content = service.Read(
                new List<FileSourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.Xml, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.xml")
                    }
                });
            Assert.IsTrue(content.Any());
        }

        [TestMethod]
        public void ReadSource_LoadJsonFileCountCheck_True()
        {
            var service = new DefaultFileProcessService(new JsonParser(), new XmlParser());
            var content = service.Read(
                new List<FileSourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.Json, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.json")
                    }
                });
            Assert.IsTrue(content.Count == 9);
        }

        [TestMethod]
        public void ReadSource_LoadXmlFileCountCheck_True()
        {
            var service = new DefaultFileProcessService(new JsonParser(), new XmlParser());
            var content = service.Read(
                new List<FileSourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.Xml, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.xml")
                    }
                });
            Assert.IsTrue(content.Count == 9);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadSource_LoadFileWithUnavailableType_Fail()
        {
            var service = new DefaultFileProcessService(new JsonParser(), new XmlParser());
            var content = service.Read(
                new List<FileSourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.Unknown, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.json")
                    }
                });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadSource_LoadNonExistentFile_Fail()
        {
            var service = new DefaultFileProcessService(new JsonParser(), new XmlParser());
            var content = service.Read(
                new List<FileSourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.Json, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\nofile")
                    }
                });
        }
    }
}
