//-----------------------------------------------------------------------
// <copyright file="UnitTestLevenshteinDistance.cs" company="Graham D Wardle">
//     The Levenshtein distance calculator sampler.
// </copyright>
//-----------------------------------------------------------------------

namespace UnitTestLevenshteinDistance
{
    using LevenshteinDistance;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The unit tests for the Levenshtein Distance of two strings.
    /// </summary>
    [TestClass]
    public class UnitTestLevenshteinDistance
    {
        /// <summary>
        /// Verification of a letter add.
        /// </summary>
        [TestMethod]
        public void TestMethodAdd()
        {
            Assert.AreEqual(1, Levenshtein.Distance("a", "at"));
            Assert.AreEqual(1, Levenshtein.Distance("rat", "rats"));
        }

        /// <summary>
        /// Verification of the words the same distance is zero
        /// </summary>
        [TestMethod]
        public void TestMethodSame()
        {
            Assert.AreEqual(0, Levenshtein.Distance("at", "at"));
            Assert.AreEqual(0, Levenshtein.Distance("Honda", "Honda"));
        }

        /// <summary>
        /// Verification of a letter removal.
        /// </summary>
        [TestMethod]
        public void TestMethodRemove()
        {
            Assert.AreEqual(1, Levenshtein.Distance("at", "a"));
            Assert.AreEqual(1, Levenshtein.Distance("Hands", "Hand"));
        }

        /// <summary>
        /// Verification of a letter Substitution
        /// </summary>
        [TestMethod]
        public void TestMethodSubtitute()
        {
            Assert.AreEqual(1, Levenshtein.Distance("at", "as"));
            Assert.AreEqual(1, Levenshtein.Distance("Rats", "rate"));
        }

        /// <summary>
        /// Verification of multiple add, sub, or remove of a letter
        /// </summary>
        [TestMethod]
        public void TestMethodMultiple()
        {
            Assert.AreEqual(3, Levenshtein.Distance("Honda", "Hyundai"));
            Assert.AreEqual(3, Levenshtein.Distance("Hyundai", "Honda"));
            Assert.AreEqual(7, Levenshtein.Distance("Camel", "Cigarettes"));
        }
    }
}