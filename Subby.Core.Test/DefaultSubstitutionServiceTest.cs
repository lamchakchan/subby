using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subby.Core.Model;
using Subby.Core.Service;
using Subby.Core.Service.Impl;
using Subby.Core.Test.Common;

namespace Subby.Core.Test
{
    [TestClass]
    public class DefaultSubstitutionServiceTest : BaseTest
    {
        ISubstitutionService substitutionService = new DefaultSubstitutionService();

        [TestMethod]
        public void Replace_SingleVariable_True()
        {
            var context = new SubstitutionContext
            {
                Variables = new Dictionary<string, string>() {{"@foo", "bar"}},
                Target = "a b c @foo"
            };

            substitutionService.Process(context);

            Assert.AreEqual(context.Result, "a b c bar");
        }

        [TestMethod]
        public void Replace_MultipleVariable_True()
        {
            var context = new SubstitutionContext
            {
                Variables = new Dictionary<string, string>() { { "@foo", "bar" }, {"@hello", "world"} },
                Target = "a b @hello c @foo"
            };

            substitutionService.Process(context);

            Assert.AreEqual(context.Result, "a b world c bar");
        }

        [TestMethod]
        public void Replace_SingleVariableMultipleTimes_True()
        {
            var context = new SubstitutionContext
            {
                Variables = new Dictionary<string, string>() {{"@foo", "bar"}},
                Target = "a @foo b @foo c @foo"
            };

            substitutionService.Process(context);

            Assert.AreEqual(context.Result, "a bar b bar c bar");
        }

        [TestMethod]
        public void Replace_MultipleVariableMultipleTimes_True()
        {
            var context = new SubstitutionContext
            {
                Variables = new Dictionary<string, string>() { { "@foo", "bar" }, { "@hello", "world" } },
                Target = "@hello @foo a b @hello c @foo"
            };

            substitutionService.Process(context);

            Assert.AreEqual(context.Result, "world bar a b world c bar");
        }

        [TestMethod]
        public void Replace_SingleInstanceWithMultipleVariables_True()
        {
            var context = new SubstitutionContext
            {
                Variables = new Dictionary<string, string>() { { "@foo", "bar" }, { "@hello", "world" } },
                Target = "a b c @foo"
            };

            substitutionService.Process(context);

            Assert.AreEqual(context.Result, "a b c bar");
        }

        [TestMethod]
        public void Replace_WithDifferentTokenTypes_True()
        {
            var context = new SubstitutionContext
            {
                Variables = new Dictionary<string, string>() { { "@foo@", "bar" }, { "#hello#", "world" }, {"!foo!", "bar"} },
                Target = "a #hello#b !foo!c @foo@"
            };

            substitutionService.Process(context);

            Assert.AreEqual(context.Result, "a worldb barc bar");
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Replace_NullVariableParameter_False()
        {
            var context = new SubstitutionContext
            {
                Variables = null,
                Target = "a"
            };
            substitutionService.Process(context);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Replace_NullTargetParameter_False()
        {
            var context = new SubstitutionContext
            {
                Variables = new Dictionary<string, string>(),
                Target = null
            };
            substitutionService.Process(context);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Replace_NullContext_False()
        {
            substitutionService.Process(null);
        }
    }
}
