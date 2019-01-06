//-----------------------------------------------------------------------
// <copyright file="UnitTestTextToNumber.cs" company="Graham D Wardle">
//     The Number text to number converter sampler.
// </copyright>
//-----------------------------------------------------------------------

namespace VerifyTextToNumber
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NumberConverter;
    
    /// <summary>
    /// Collection of unit tests for the Text to number converter.
    /// </summary>
    [TestClass]
    public class UnitTestTextToNumber
    {
        /// <summary>
        /// Verify that a blank text is invalid.
        /// </summary>
        [TestMethod]
        public void TestBlankTextInvalid()
        {
            Assert.IsFalse(TextToNumber.IsValid(string.Empty));
        }

        /// <summary>
        /// Verify that a single digit word valid.
        /// </summary>
        [TestMethod]
        public void TestSingleDigit()
        {
            string test = "One";
            Assert.IsTrue(TextToNumber.IsValid(test));
            Assert.AreEqual(string.Empty, TextToNumber.Error);
            Assert.AreEqual(1, TextToNumber.DoConvert(test));
        }

        /// <summary>
        /// Verify that multiple digit words are invalid.
        /// </summary>
        [TestMethod]
        public void TestMultipleDigit()
        {
            string test = "One two";
            Assert.IsFalse(TextToNumber.IsValid(test));
            Assert.AreEqual("Second digit is not valid two\r\n", TextToNumber.Error);
            Assert.AreEqual(0, TextToNumber.DoConvert(test));
            Assert.AreEqual("Second digit is not valid two\r\n", TextToNumber.Error);
        }

        /// <summary>
        /// Verify that a multiple teens or tens word are invalid.
        /// </summary>
        [TestMethod]
        public void TestMultipleTeens()
        {
            string test = "Fourteen twelve";
            Assert.IsFalse(TextToNumber.IsValid(test));
            Assert.AreEqual("Did not expect the word type of teens or tens in the position of second digit for twelve\r\n", TextToNumber.Error);
            Assert.AreEqual(0, TextToNumber.DoConvert(test));
            Assert.AreEqual("Did not expect the word type of teens or tens in the position of second digit for twelve\r\n", TextToNumber.Error);
        }

        /// <summary>
        /// Verify that a single teen word is valid.
        /// </summary>
        [TestMethod]
        public void TestTeenDigit()
        {
            string test = "eleven";
            Assert.IsTrue(TextToNumber.IsValid(test));
            Assert.AreEqual(string.Empty, TextToNumber.Error);
            Assert.AreEqual(11, TextToNumber.DoConvert(test));
            Assert.AreEqual(string.Empty, TextToNumber.Error);
        }

        /// <summary>
        /// Verify that a single ten word is valid.
        /// </summary>
        [TestMethod]
        public void TestTenDigit()
        {
            string test = "forty";
            Assert.IsTrue(TextToNumber.IsValid(test));
            Assert.AreEqual(string.Empty, TextToNumber.Error);
            Assert.AreEqual(40, TextToNumber.DoConvert(test));
            Assert.AreEqual(string.Empty, TextToNumber.Error);
        }

        /// <summary>
        /// Verify that a ten word followed by a digit word is valid.
        /// </summary>
        [TestMethod]
        public void Test57Digit()
        {
            string test = "Fifty seven";
            Assert.IsTrue(TextToNumber.IsValid(test), TextToNumber.Error);
            Assert.AreEqual(string.Empty, TextToNumber.Error);
            Assert.AreEqual(57, TextToNumber.DoConvert(test));
            Assert.AreEqual(string.Empty, TextToNumber.Error);
        }

        /// <summary>
        /// Verify that a digit word followed by a ten word is invalid.
        /// </summary>
        [TestMethod]
        public void Test7_50Digit()
        {
            string test = "seven Fifty";
            Assert.IsFalse(TextToNumber.IsValid(test), TextToNumber.Error);
            Assert.AreEqual("Did not expect the word type of teens or tens in the position of second digit for fifty\r\n", TextToNumber.Error);
            Assert.AreEqual(0, TextToNumber.DoConvert(test));
            Assert.AreEqual("Did not expect the word type of teens or tens in the position of second digit for fifty\r\n", TextToNumber.Error);
        }

        /// <summary>
        /// Verify that a hundred ten word followed by a digit word is valid.
        /// </summary>
        [TestMethod]
        public void Test157Digit()
        {
            string test = "One Hundred and Fifty seven";
            Assert.IsTrue(TextToNumber.IsValid(test), TextToNumber.Error);
            Assert.AreEqual(string.Empty, TextToNumber.Error);
            Assert.AreEqual(157, TextToNumber.DoConvert(test));
            Assert.AreEqual(string.Empty, TextToNumber.Error);
        }

        /// <summary>
        /// Verify that if a digit is missing from a hundred, ten word followed by a digit word is invalid.
        /// </summary>
        [TestMethod]
        public void Test157DigitWithNoFirstDigit()
        {
            string test = "Hundred and Fifty seven";
            Assert.IsFalse(TextToNumber.IsValid(test), TextToNumber.Error);
            Assert.AreEqual("Expecting a number word rather than : hundred\r\nExpecting a number word rather than : and\r\n", TextToNumber.Error);
            Assert.AreEqual(0, TextToNumber.DoConvert(test));
            Assert.AreEqual("Expecting a number word rather than : hundred\r\nExpecting a number word rather than : and\r\n", TextToNumber.Error);
        }

        /// <summary>
        /// Verify that a hundred followed by multipliers is valid.
        /// </summary>
        [TestMethod]
        public void Test10010001000000Digit()
        {
            string test = "one Hundred thousand million";
            Assert.IsTrue(TextToNumber.IsValid(test));
            Assert.AreEqual(string.Empty, TextToNumber.Error);
            Assert.AreEqual(100000000000, TextToNumber.DoConvert(test));
            Assert.AreEqual(string.Empty, TextToNumber.Error);
        }

        /// <summary>
        /// Verify that a hundred followed by multipliers proceeded by more than a digit is invalid.
        /// </summary>
        [TestMethod]
        public void TestHundredWith71()
        {
            string test = "seventy one Hundred thousand million";
            Assert.IsFalse(TextToNumber.IsValid(test));
            Assert.AreEqual("The Hundred can only follow a digit word\r\n", TextToNumber.Error);
            Assert.AreEqual(0, TextToNumber.DoConvert(test));
            Assert.AreEqual("The Hundred can only follow a digit word\r\n", TextToNumber.Error);
        }

        /// <summary>
        /// Verify that a hundred followed by multipliers proceeded by more than a digit is invalid.
        /// </summary>
        [TestMethod]
        public void TestHundredWithTeens()
        {
            string test = "seventeen Hundred thousand million";
            Assert.IsFalse(TextToNumber.IsValid(test));
            Assert.AreEqual("The Hundred can only follow a digit word\r\n", TextToNumber.Error);
            Assert.AreEqual(0, TextToNumber.DoConvert(test));
            Assert.AreEqual("The Hundred can only follow a digit word\r\n", TextToNumber.Error);
        }

        /// <summary>
        /// Verify that a hundred and fifty nine followed by multipliers is valid.
        /// </summary>
        [TestMethod]
        public void Test159000Digit()
        {
            string test = "one Hundred and fifty nine thousand";
            Assert.IsTrue(TextToNumber.IsValid(test), TextToNumber.Error);
            Assert.AreEqual(string.Empty, TextToNumber.Error);
            Assert.AreEqual(159000, TextToNumber.DoConvert(test));
            Assert.AreEqual(string.Empty, TextToNumber.Error);
        }

        /// <summary>
        /// Verify that a double and is invalid.
        /// </summary>
        [TestMethod]
        public void Test159000DoubleAndDigit()
        {
            string test = "one Hundred and and fifty nine thousand";
            Assert.IsFalse(TextToNumber.IsValid(test), TextToNumber.Error);
            Assert.AreEqual("Can not repeat and\r\nExpecting a number word rather than : and\r\n", TextToNumber.Error);
        }

        /// <summary>
        /// Verify that an And at the end is invalid.
        /// </summary>
        [TestMethod]
        public void Test159000AndAtEndNoDigit()
        {
            string test = "one Hundred and fifty nine thousand and";
            Assert.IsFalse(TextToNumber.IsValid(test), TextToNumber.Error);
            Assert.AreEqual("The last word can not be 'and' most be followed by a number word\r\n", TextToNumber.Error);
        }
    }
}