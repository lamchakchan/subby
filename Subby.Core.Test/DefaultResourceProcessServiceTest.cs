using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Enum.Impl;
using Subby.Core.Model;
using Subby.Core.Parser.Impl;
using Subby.Core.Service;
using Subby.Core.Service.Impl;
using Subby.Core.Test.Common;

namespace Subby.Core.Test
{
    [TestClass]
    public class DefaultResourceProcessServiceTest : BaseIISTest
    {
        static IFileResourceProcessService fileService = new DefaultFileResourceProcessService(new JsonParser(), new XmlParser(), new NewLineParser());
        static IHttpResourceProcessService httpService = new DefaultHttpResourceProcessService(new JsonParser(), new XmlParser(), new NewLineParser());

        ICompositeResourceProcessService service = new DefaultResourceProcessService(fileService, httpService);

        [TestMethod]
        public void ReadTarget_LoadFileHasContent_True()
        {
            var content = service.Read(new FileTargetContext { FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\target.txt") });
            Assert.IsTrue(!string.IsNullOrEmpty(content));
        }

        [TestMethod]
        public void ReadTarget_LoadUrlHasContent_True()
        {
            var content = service.Read(new HttpTargetContext
                                       {
                                           HttpResource =
                                               new HttpResource { Url = "http://localhost:10500/target.txt" }
                                       });
            Assert.IsTrue(!string.IsNullOrEmpty(content));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadTarget_LoadNonExistentFile_Fail()
        {
            var content = service.Read(new FileTargetContext { FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\nofile.txt") });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadTarget_LoadNonExistentUrl_Fail()
        {
            var content =
                service.Read(new HttpTargetContext
                             {
                                 HttpResource =
                                     new HttpResource { Url = "http://localhost:10500/nofile.txt" }
                             });
        }

        [TestMethod]
        public void ReadSource_LoadJsonFileHasContent_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.Json, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.js")
                    }
                });
            Assert.IsTrue(content.Any());
        }

        [TestMethod]
        public void ReadSource_LoadJsonUrlHasContent_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new HttpSourceContext
                    {
                        Type = SourceType.Json, 
                        HttpResource = new HttpResource{Url = "http://localhost:10500/variables.js"}
                    }
                });
            Assert.IsTrue(content.Any());
        }

        [TestMethod]
        public void ReadSource_LoadJsonFileAndUrlHasContent_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {new FileSourceContext
                 {
                     Type = SourceType.Json, 
                     FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.js")
                 },
                 new HttpSourceContext
                 {
                     Type = SourceType.Json, 
                     HttpResource = new HttpResource{Url = "http://localhost:10500/variables.js"}
                 }
                });
            Assert.IsTrue(content.Any());
        }

        [TestMethod]
        public void ReadSource_LoadXmlFileHasContent_True()
        {
            var content = service.Read(
                new List<SourceContext>
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
        public void ReadSource_LoadXmlUrlHasContent_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new HttpSourceContext
                    {
                        Type = SourceType.Xml, 
                        HttpResource = new HttpResource{Url = "http://localhost:10500/variables.xml"}
                    }
                });
            Assert.IsTrue(content.Any());
        }

        [TestMethod]
        public void ReadSource_LoadXmlFileAndUrlHasContent_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {new FileSourceContext
                 {
                     Type = SourceType.Xml, 
                     FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.xml")
                 },
                 new HttpSourceContext
                 {
                     Type = SourceType.Xml, 
                     HttpResource = new HttpResource{Url = "http://localhost:10500/variables.xml"}
                 }
                });
            Assert.IsTrue(content.Any());
        }

        [TestMethod]
        public void ReadSource_LoadNewLineFileHasContent_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.NewLineDelimited, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.txt")
                    }
                });
            Assert.IsTrue(content.Any());
        }

        [TestMethod]
        public void ReadSource_LoadNewLineUrlHasContent_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new HttpSourceContext
                    {
                        Type = SourceType.NewLineDelimited, 
                        HttpResource = new HttpResource{Url = "http://localhost:10500/variables.txt"}
                    }
                });
            Assert.IsTrue(content.Any());
        }

        [TestMethod]
        public void ReadSource_LoadNewLineFileAndUrlHasContent_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.NewLineDelimited, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.txt")
                    },
                    new HttpSourceContext
                    {
                        Type = SourceType.NewLineDelimited, 
                        HttpResource = new HttpResource{Url = "http://localhost:10500/variables.txt"}
                    }
                });
            Assert.IsTrue(content.Any());
        }

        [TestMethod]
        public void ReadSource_LoadJsonFileCountCheck_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.Json, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.js")
                    }
                });
            Assert.IsTrue(content.Count == 9);
        }

        [TestMethod]
        public void ReadSource_LoadJsonUrlCountCheck_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new HttpSourceContext
                    {
                        Type = SourceType.Json, 
                        HttpResource = new HttpResource{Url = "http://localhost:10500/variables.js"}
                    }
                });
            Assert.IsTrue(content.Count == 9);
        }

        [TestMethod]
        public void ReadSource_LoadXmlFileCountCheck_True()
        {
            var content = service.Read(
                new List<SourceContext>
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
        public void ReadSource_LoadXmlUrlCountCheck_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new HttpSourceContext
                    {
                        Type = SourceType.Xml, 
                        HttpResource = new HttpResource{Url = "http://localhost:10500/variables.xml"}
                    }
                });
            Assert.IsTrue(content.Count == 9);
        }

        [TestMethod]
        public void ReadSource_LoadNewLineFileCountCheck_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.NewLineDelimited, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.txt")
                    }
                });
            Assert.IsTrue(content.Count == 9);
        }

        [TestMethod]
        public void ReadSource_LoadNewLineUrlCountCheck_True()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new HttpSourceContext
                    {
                        Type = SourceType.NewLineDelimited, 
                        HttpResource = new HttpResource{Url = "http://localhost:10500/variables.txt"}
                    }
                });
            Assert.IsTrue(content.Count == 9);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadSource_LoadFileWithUnavailableType_Fail()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.Unknown, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\variables.js")
                    }
                });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadSource_LoadUrlWithUnavailableType_Fail()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new HttpSourceContext
                    {
                        Type = SourceType.Unknown, 
                        HttpResource = new HttpResource{Url = "http://localhost:10500/variables.js"}
                    }
                });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadSource_LoadNonExistentFile_Fail()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new FileSourceContext
                    {
                        Type = SourceType.Json, 
                        FilePath = Path.Combine(TestContext.TestDeploymentDir, @"Files\nofile")
                    }
                });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadSource_LoadNonExistentUrl_Fail()
        {
            var content = service.Read(
                new List<SourceContext>
                {
                    new HttpSourceContext
                    {
                        Type = SourceType.Unknown, 
                        HttpResource = new HttpResource{Url = "http://localhost:10500/nofile.txt"}
                    }
                });
        }
    }
}