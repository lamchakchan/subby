using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Model;
using Subby.Core.Parser.Impl;
using Subby.Core.Service;
using Subby.Core.Service.Impl;

namespace Subby.Core.Test
{
    [TestClass]
    [DeploymentItem("Files", "Files")]
    public class DefaulHttpProcessServiceTest
    {
        IHttpResourceProcessService service = new DefaultHttpProcessService(new JsonParser(), new XmlParser(), new NewLineParser());

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void ReadTarget_LoadFileHasContent_True()
        {
            var content = service.Read(new HttpTargetContext { HttpResource = new HttpResource{Url = "http://www.google.com"}});
            Assert.IsTrue(!string.IsNullOrEmpty(content));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadTarget_LoadNonExistentFile_Fail()
        {
            var content = service.Read(new HttpTargetContext { HttpResource = new HttpResource { Url = "http://www.google.com/abc" } });
        }

        //[TestMethod]
        //public void ReadSource_LoadJsonFileHasContent_True()
        //{
        //    var content = service.Read(
        //        new List<FileSourceContext>
        //        {
        //            new FileSourceContext
        //            {
        //                Type = SourceType.Json, 
        //                FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.json")
        //            }
        //        });
        //    Assert.IsTrue(content.Any());
        //}

        //[TestMethod]
        //public void ReadSource_LoadXmlFileHasContent_True()
        //{
        //    var content = service.Read(
        //        new List<FileSourceContext>
        //        {
        //            new FileSourceContext
        //            {
        //                Type = SourceType.Xml, 
        //                FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.xml")
        //            }
        //        });
        //    Assert.IsTrue(content.Any());
        //}

        //[TestMethod]
        //public void ReadSource_LoadNewLineFileHasContent_True()
        //{
        //    var content = service.Read(
        //        new List<FileSourceContext>
        //        {
        //            new FileSourceContext
        //            {
        //                Type = SourceType.NewLineDelimited, 
        //                FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.txt")
        //            }
        //        });
        //    Assert.IsTrue(content.Any());
        //}

        //[TestMethod]
        //public void ReadSource_LoadJsonFileCountCheck_True()
        //{
        //    var content = service.Read(
        //        new List<FileSourceContext>
        //        {
        //            new FileSourceContext
        //            {
        //                Type = SourceType.Json, 
        //                FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.json")
        //            }
        //        });
        //    Assert.IsTrue(content.Count == 9);
        //}

        //[TestMethod]
        //public void ReadSource_LoadXmlFileCountCheck_True()
        //{
        //    var content = service.Read(
        //        new List<FileSourceContext>
        //        {
        //            new FileSourceContext
        //            {
        //                Type = SourceType.Xml, 
        //                FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.xml")
        //            }
        //        });
        //    Assert.IsTrue(content.Count == 9);
        //}

        //[TestMethod]
        //public void ReadSource_LoadNewLineFileCountCheck_True()
        //{
        //    var content = service.Read(
        //        new List<FileSourceContext>
        //        {
        //            new FileSourceContext
        //            {
        //                Type = SourceType.NewLineDelimited, 
        //                FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.txt")
        //            }
        //        });
        //    Assert.IsTrue(content.Count == 9);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        //public void ReadSource_LoadFileWithUnavailableType_Fail()
        //{
        //    var content = service.Read(
        //        new List<FileSourceContext>
        //        {
        //            new FileSourceContext
        //            {
        //                Type = SourceType.Unknown, 
        //                FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.json")
        //            }
        //        });
        //}

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        //public void ReadSource_LoadNonExistentFile_Fail()
        //{
        //    var content = service.Read(
        //        new List<FileSourceContext>
        //        {
        //            new FileSourceContext
        //            {
        //                Type = SourceType.Json, 
        //                FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\nofile")
        //            }
        //        });
        //}
    }
}