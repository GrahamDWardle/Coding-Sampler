//-----------------------------------------------------------------------
// <copyright file="Brackets.cs" company="Graham D Wardle">
//     The unit tests for Paired bracket converter sampler.
// </copyright>
//-----------------------------------------------------------------------

namespace Brackets
{
    using System.Collections.Generic;

    /// <summary>
    /// The class to validate a string for balance bracket pairs
    /// </summary>
    public class Brackets
    {
        /// <summary>
        /// The list of valid bracket pairs.
        /// </summary>
        private static List<string> validPairs = new List<string> { "{}", "[]", "()" };

        /// <summary>
        /// The method to validate if a piece of text has balanced bracket pairs.
        /// </summary>
        /// <param name="text">The text to validate.</param>
        /// <param name="blankValid">True if a blank is considered valid; False if there must be a</param>
        /// <returns>True if balance brackets are found; False if one unbalanced bracket is found.</returns>
        public static bool Validate(string text, bool blankValid = true)
        {
            bool result = blankValid || !string.IsNullOrEmpty(text);
            while (!string.IsNullOrEmpty(text) && result == true)
            {
                result = false;
                foreach (string item in validPairs)
                {
                    if (text.Contains(item))
                    {
                        text = text.Replace(item, string.Empty);
                        result = true;
                    }
                }
            }

            return result;
        }
    }
}