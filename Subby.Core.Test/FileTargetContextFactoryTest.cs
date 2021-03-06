﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Factory;
using Subby.Core.Factory.Impl;

namespace Subby.Core.Test
{
    [TestClass]
    public class FileTargetContextFactoryTest
    {
        IFileTargetContextFactory factory = new FileTargetContextFactory();

        [TestMethod]
        public void Build_HasPathValue_True()
        {
            var result = factory.Build("abc");

            Assert.IsTrue(!string.IsNullOrEmpty(result.FilePath));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Build_HasNoPathValue_False()
        {
            var result = factory.Build(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Build_HasEmptyPathValue_False()
        {
            var result = factory.Build(string.Empty);
        }
    }
}
