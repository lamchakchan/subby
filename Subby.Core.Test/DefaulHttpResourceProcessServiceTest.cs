﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
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
    public class DefaulHttpResourceProcessServiceTest : BaseIISTest
    {
        IHttpResourceProcessService service = new DefaultHttpResourceProcessService(new JsonParser(), new XmlParser(), new NewLineParser());
        
        [TestMethod]
        public void ReadTarget_LoadFileHasContent_True()
        {
            var content =
                service.Read(new HttpTargetContext
                             {
                                 HttpResource =
                                     new HttpResource {Url = "http://localhost:10500/target.txt"}
                             });
            Assert.IsTrue(!string.IsNullOrEmpty(content));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadTarget_LoadNonExistentFile_Fail()
        {
            var content =
                service.Read(new HttpTargetContext
                             {
                                 HttpResource =
                                     new HttpResource {Url = "http://localhost:10500/nofile.txt"}
                             });
        }

        [TestMethod]
        public void ReadSource_LoadJsonFileHasContent_True()
        {
            var content = service.Read(
                new List<HttpSourceContext>
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
        public void ReadSource_LoadXmlFileHasContent_True()
        {
            var content = service.Read(
                new List<HttpSourceContext>
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
        public void ReadSource_LoadNewLineFileHasContent_True()
        {
            var content = service.Read(
                new List<HttpSourceContext>
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
        public void ReadSource_LoadJsonFileCountCheck_True()
        {
            var content = service.Read(
                new List<HttpSourceContext>
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
                new List<HttpSourceContext>
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
                new List<HttpSourceContext>
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
                new List<HttpSourceContext>
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
                new List<HttpSourceContext>
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