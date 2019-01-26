//-----------------------------------------------------------------------
// <copyright file="Levenshtein.cs" company="Graham D Wardle">
//     The unit tests for Paired bracket converter sampler.
// </copyright>
//-----------------------------------------------------------------------

namespace LevenshteinDistance
{
    using System;

    /// <summary>
    /// The class to calculate the Levenshtein distance of two strings
    /// <see cref="https://en.wikipedia.org/wiki/Damerau–Levenshtein_distance"/>
    /// </summary>
    public class Levenshtein
    {
        /// <summary>
        /// The method to measure the Levenshtein distance between to words
        /// </summary>
        /// <param name="word1">The first word.</param>
        /// <param name="word2">The second word.</param>
        /// <returns>The calculated distance.</returns>
        public static int Distance(string word1, string word2)
        {
            word1 = word1.ToLower();
            word2 = word2.ToLower();
            int len1 = word1.Length;
            int len2 = word2.Length;
            int[,] lookup = new int[len1 + 1, len2 + 1];
           for (int i = 0; i <= len1; i++)
            {
                lookup[i, 0] = i;
            }

            for (int j = 0; j <= len2; j++)
            {
                lookup[0, j] = j;
            }

            for (int i = 0; i < len1; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    int diff = word1.Substring(i, 1).Equals(word2.Substring(j, 1)) ? 0 : 1;
                    int value = Math.Min(
                                    Math.Min(
                                        lookup[i, j + 1] + 1, 
                                        lookup[i + 1, j] + 1),
                                    lookup[i, j] + diff);
                    lookup[i + 1, j + 1] = value;
                }
            }

            return lookup[len1, len2];
        }
    }
}