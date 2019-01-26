//-----------------------------------------------------------------------
// <copyright file="UnitTestBrackets.cs" company="Graham D Wardle">
//     The unit tests for Paired bracket converter sampler.
// </copyright>
//-----------------------------------------------------------------------

namespace UnitTestBrackets
{
    using Brackets;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The unit test for the Balanced brackets pairs.
    /// </summary>
    [TestClass]
    public class UnitTestBrackets
    {
        /// <summary>
        /// Verification of a single elements fails.
        /// </summary>
        [TestMethod]
        public void TestMethodSingle()
        {
            Assert.IsFalse(Brackets.Validate("["));
            Assert.IsFalse(Brackets.Validate("{"));
            Assert.IsFalse(Brackets.Validate("("));

            // Test the second parameter boolean does not change that
            Assert.IsFalse(Brackets.Validate("[", true));
            Assert.IsFalse(Brackets.Validate("{", false));
            Assert.IsFalse(Brackets.Validate("(", false));
        }

        /// <summary>
        /// Verification for the Single valid pairs of brackets
        /// </summary>
        [TestMethod]
        public void TestMethodSinglePair()
        {
            Assert.IsTrue(Brackets.Validate("[]"));
            Assert.IsTrue(Brackets.Validate("{}"));
            Assert.IsTrue(Brackets.Validate("()"));

            // Verify the second boolean does not change that.
            Assert.IsTrue(Brackets.Validate("[]", false));
            Assert.IsTrue(Brackets.Validate("{}", true));
            Assert.IsTrue(Brackets.Validate("()", true));
        }

        /// <summary>
        /// Verification of the results of a empty string.
        /// True if just the string passed.
        /// True if string plus true passed for BlankValid
        /// False if the string plus false passed for BlankValid
        /// </summary>
        [TestMethod]
        public void TestMethodEmpty()
        {
            Assert.IsTrue(Brackets.Validate(string.Empty));
            Assert.IsFalse(Brackets.Validate(string.Empty, false));
            Assert.IsTrue(Brackets.Validate(string.Empty, true));
        }

        /// <summary>
        /// Verification of the nested pairs being balance 
        /// </summary>
        [TestMethod]
        public void TestMethodNestedValid()
        {
            Assert.IsTrue(Brackets.Validate("{[]}"));
            Assert.IsTrue(Brackets.Validate("{()}"));
            Assert.IsTrue(Brackets.Validate("({[]})"));
            Assert.IsTrue(Brackets.Validate("[]()"));
        }

        /// <summary>
        /// Verification that the Nested brackets are invalid when not balanced pairs.
        /// </summary>
        [TestMethod]
        public void TestMethodNextedInValid()
        {
            Assert.IsFalse(Brackets.Validate("{[}]"));
            Assert.IsFalse(Brackets.Validate("{([)}"));
            Assert.IsFalse(Brackets.Validate("({[]})]"));
            Assert.IsFalse(Brackets.Validate("[(])"));
            Assert.IsFalse(Brackets.Validate("}][{)"));
        }
    }
}