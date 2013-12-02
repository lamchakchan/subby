using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Factory.Impl;

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
}
