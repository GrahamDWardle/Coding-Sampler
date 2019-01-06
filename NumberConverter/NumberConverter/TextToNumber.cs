//-----------------------------------------------------------------------
// <copyright file="TextToNumber.cs" company="Graham D Wardle">
//     The Number words to number converter sampler.
// </copyright>
//-----------------------------------------------------------------------

namespace NumberConverter
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The class to convert number words to number.
    /// The class will parse words such as.
    /// One Hundred and fifty five thousand and thirteen
    /// The valid pattern as follows:
    /// For First word : Only a Digit, ten or teen are valid
    /// For second Digit : either Digit of multiplier.
    /// For Multiplier any number followed on after each other
    /// Every Multiplier must be followed by either the word 'and' then more digits or Nothing.
    /// If the multiplier is Hundred then it can be followed by tens, teens or digits.
    /// This could be followed by another multiplier that is not hundred.
    /// </summary>
    public class TextToNumber
    {
        /// <summary>
        /// The number word for hundred.
        /// </summary>
        private static readonly string Hundred = "hundred";

        /// <summary>
        /// The separator for the word and
        /// </summary>
        private static readonly string And = "and";

        /// <summary>
        /// The separator for the word only
        /// </summary>
        private static readonly string Only = "only";

        /// <summary>
        /// The dictionary of words and their values for digits 1-9
        /// </summary>
        private static Dictionary<string, long> digits = new Dictionary<string, long>();

        /// <summary>
        /// The dictionary of words and their value for 11-19
        /// </summary>
        private static Dictionary<string, long> teens = new Dictionary<string, long>();

        /// <summary>
        /// The dictionary of the words and their values for multiples of ten from 10-90
        /// </summary>
        private static Dictionary<string, long> tens = new Dictionary<string, long>();

        /// <summary>
        /// The dictionary of the words and their values for 
        /// </summary>
        private static Dictionary<string, int> multiples = new Dictionary<string, int>();

        /// <summary>
        /// The list of valid separators.
        /// </summary>
        private static List<string> seperators = new List<string>();

        /// <summary>
        /// The last error text.
        /// </summary>
        private static StringBuilder errorText = new StringBuilder();

        /// <summary>
        /// Initializes static members of the <see cref="TextToNumber" /> class.
        /// Initialize all of the static data for the conversion.
        /// </summary>
        static TextToNumber()
        {
            // Allowed digit words such as one or two.
            digits.Add("one", 1);
            digits.Add("two", 2);
            digits.Add("three", 3);
            digits.Add("four", 4);
            digits.Add("five", 5);
            digits.Add("six", 6);
            digits.Add("seven", 7);
            digits.Add("eight", 8);
            digits.Add("nine", 9);

            // Allowed words for teens such as eleven or nineteen.
            teens.Add("eleven", 11);
            teens.Add("twelve", 12);
            teens.Add("thirteen", 13);
            teens.Add("fourteen", 14);
            teens.Add("fifteen", 15);
            teens.Add("sixteen", 16);
            teens.Add("seventeen", 17);
            teens.Add("eighteen", 18);
            teens.Add("nineteen", 19);

            // Allow numbers such as 
            tens.Add("ten", 10);
            tens.Add("twenty", 20);
            tens.Add("thirty", 30);
            tens.Add("forty", 40);
            tens.Add("fifty", 50);
            tens.Add("sixty", 60);
            tens.Add("seventy", 70);
            tens.Add("eighty", 80);
            tens.Add("ninety", 90);

            // Allow multiplier such as five hundred or three hundred thousand million
            multiples.Add(Hundred, 100);
            multiples.Add("thousand", 1000);
            multiples.Add("million", 1000000);

            seperators.Add(And);
            seperators.Add("dollar");
        }

        /// <summary>
        /// The State machine values for the text parsing of words.
        /// </summary>
        private enum ParseState
        {
            /// <summary>
            /// No parse state for a blank item.
            /// </summary>
            None,

            /// <summary>
            /// State machine phase the first word for number field.
            /// </summary>
            FirstWord,

            /// <summary>
            /// The second digit in after a tens.
            /// </summary>
            SecondDigit,

            /// <summary>
            /// The Hundred multiplier.
            /// </summary>
            HundredSeparators,

            /// <summary>
            /// The And separator after a multiplier.
            /// </summary>
            And,

            /// <summary>
            /// The Multiplier separator.
            /// </summary>
            Multiplier,

            /// <summary>
            /// The separator types.
            /// </summary>
            Separator,

            /// <summary>
            /// The Only word (Not Yet Implemented.)
            /// </summary>
            Only
        }

        /// <summary>
        /// Gets the error of any processing.
        /// </summary>
        public static string Error
        {
            get
            {
                return errorText.ToString();
            }
        }

        /// <summary>
        /// The method to determine if the text contains valid text elements.
        /// </summary>
        /// <param name="text">The text to be processed. Word items such as ten, seventeen, or eighty four.</param>
        /// <returns>True if the space separated number words are valid: False if no text, blank items.</returns>
        public static bool IsValid(string text)
        {
            bool seenAnd = false;
            ParseState parseState = ParseState.FirstWord;
            errorText.Clear();
            string[] split = text.ToLower().Split(' ');
            if (split.Length == 0)
            {
                errorText.AppendLine("Blank text is not valid");
            }

            bool lastWasDigit = false;
            bool lastWasAnd = false;
            int digitCount = 0;
            foreach (string item in split)
            {
                if (string.IsNullOrEmpty(item))
                {
                    errorText.AppendLine(string.Format("Blank items are not valid"));
                }
                else
                {
                    lastWasAnd = false;
                    if (item.Equals(And))
                    {
                        if (seenAnd)
                        {
                            errorText.AppendLine("Can not repeat and");
                        }

                        seenAnd = true;
                    }
                    else
                    {
                        seenAnd = false;
                        if (!(digits.ContainsKey(item) ||
                            teens.ContainsKey(item) ||
                            tens.ContainsKey(item) ||
                            multiples.ContainsKey(item) ||
                            seperators.Contains(item)))
                        {
                            errorText.AppendLine(string.Format("The item: {0} is not valid", item));
                        }
                    }

                    ParseState wordsType = GetWordType(item);
                    switch (parseState)
                    {
                        case ParseState.FirstWord:
                            if (wordsType == ParseState.FirstWord || wordsType == ParseState.SecondDigit)
                            {
                                parseState = ParseState.SecondDigit;
                                lastWasAnd = false;
                                if (wordsType.Equals(ParseState.SecondDigit))
                                {
                                    lastWasDigit = true;
                                    digitCount++;
                                }
                                else
                                {
                                    lastWasDigit = false;
                                    digitCount += 2;
                                }
                            }
                            else
                            {
                                errorText.AppendLine(string.Format("Expecting a number word rather than : {0}", item));
                            }

                            break;
                        case ParseState.SecondDigit:
                            if (wordsType == ParseState.SecondDigit)
                            {
                                digitCount++;
                                lastWasAnd = false;
                                parseState = ParseState.Multiplier;
                                if (lastWasDigit)
                                {
                                    errorText.AppendLine(string.Format("Second digit is not valid {0}", item));
                                }
                            }
                            else if (wordsType == ParseState.Multiplier)
                            {
                                parseState = ParseState.And;
                            }
                            else if (wordsType == ParseState.HundredSeparators)
                            {
                                parseState = ParseState.And;
                                if (!lastWasDigit)
                                {
                                    errorText.AppendLine("The Hundred can only follow a digit word");
                                }
                            }
                            else
                            {
                                string type = wordsType.Equals(ParseState.FirstWord) ? "teens or tens" : wordsType.ToString();
                                errorText.AppendLine(string.Format("Did not expect the word type of {0} in the position of second digit for {1}", type, item));
                            }

                            break;
                        case ParseState.And:
                            if (wordsType == ParseState.And)
                            {
                                digitCount = 0;
                                parseState = ParseState.FirstWord;
                                lastWasAnd = true;
                            }
                            else
                            {
                                if (wordsType != ParseState.Multiplier)
                                {
                                    errorText.AppendLine(string.Format("A multiplier must be followed by the word 'And' rather than {0}", item));
                                }
                            }

                            break;
                        case ParseState.Multiplier:
                            if (wordsType.Equals(ParseState.HundredSeparators))
                            {
                                if (digitCount > 2 || !lastWasDigit)
                                {
                                    errorText.AppendLine("The Hundred can only follow a digit word");
                                }
                            }

                            if (wordsType.Equals(ParseState.Multiplier))
                            {
                                parseState = ParseState.And;
                            }
                            else if (wordsType.Equals(ParseState.SecondDigit))
                            {
                                parseState = ParseState.SecondDigit;
                            }

                            break;
                        default:
                            errorText.AppendLine(string.Format("Did not expect the word type of {0}", item));
                            break;
                    }
                }
            }

            if (lastWasAnd)
            {
                errorText.AppendLine("The last word can not be 'and' most be followed by a number word");
            }

            return errorText.Length == 0;
        }

        /// <summary>
        /// The method to convert number words from text to number
        /// </summary>
        /// <param name="text">The text to be converted.</param>
        /// <returns>Zero if the text is not valid; otherwise the converted value;</returns>
        public static long DoConvert(string text)
        {
            long result = 0;
            long section = 0;
            ParseState parseState = ParseState.FirstWord;
            string[] split = text.ToLower().Split(' ');
            errorText.Clear();

            if (IsValid(text))
            {
                for (int index = 0; index < split.Length; index++)
                {
                    string item = split[index];
                    string nextItem = index < split.Length - 1 ? split[index + 1] : string.Empty;
                    ParseState nextState = GetWordType(nextItem);
                    switch (parseState)
                    {
                        case ParseState.FirstWord:
                            section = GetValueForFirstDigit(item);
                            parseState = nextState;
                            break;
                        case ParseState.SecondDigit:
                            section += GetValueForDigit(item);
                            break;
                        case ParseState.Multiplier:
                            section *= multiples[item];
                            break;
                        case ParseState.HundredSeparators:
                            section *= 100;
                            switch (nextState)
                            {
                                case ParseState.Multiplier:
                                    section *= multiples[nextItem];
                                    index++;
                                    parseState = ParseState.Multiplier;
                                    break;
                                case ParseState.And:
                                    index += 2;
                                    nextItem = split[index];
                                    nextState = GetWordType(nextItem);
                                    switch (nextState)
                                    {
                                        case ParseState.FirstWord:
                                            section += GetValueForFirstDigit(nextItem);
                                            if (index < split.Length - 1)
                                            {
                                                nextItem = split[++index];
                                                nextState = GetWordType(nextItem);
                                                if (nextState.Equals(ParseState.SecondDigit))
                                                {
                                                    section += GetValueForDigit(nextItem);
                                                    if (index + 1 < split.Length)
                                                    {
                                                        nextItem = split[index + 1];
                                                        parseState = GetWordType(nextItem);
                                                    }
                                                    else
                                                    {
                                                        nextState = ParseState.None;
                                                    }
                                                }
                                            }

                                            break;
                                        case ParseState.SecondDigit:
                                            section += GetValueForDigit(nextItem);
                                            break;
                                        default:
                                            break;
                                    }

                                    break;
                            }

                            break;
                    }

                    if (nextState.Equals(ParseState.None) || nextState.Equals(ParseState.And))
                    {
                        result += section;
                        section = 0;
                    }
                }
            }

            return errorText.Length > 0 ? 0 : result;
        }

        /// <summary>
        /// The method to extract the value for a digit word.
        /// </summary>
        /// <param name="word">The number word to convert.</param>
        /// <returns>0 if no match is found; otherwise the value of the digit word.</returns>
        private static long GetValueForDigit(string word)
        {
            long value = 0;
            if (digits.ContainsKey(word))
            {
                value = digits[word];
            }

            return value;
        }

        /// <summary>
        /// The method to extract the value for either digits, teens or tens.
        /// </summary>
        /// <param name="word">The number word to convert</param>
        /// <returns>0 if no match is found; otherwise the value of the number word.</returns>
        private static long GetValueForFirstDigit(string word)
        {
            long section = 0;
            if (digits.ContainsKey(word))
            {
                section = digits[word];
            }
            else if (teens.ContainsKey(word))
            {
                section = teens[word];
            }
            else if (tens.ContainsKey(word))
            {
                section = tens[word];
            }

            return section;
        }

        /// <summary>
        /// The method to determine the type of number word
        /// </summary>
        /// <param name="word">The number word to match.</param>
        /// <returns>None if no match is found;otherwise the classification of the number words.</returns>
        private static ParseState GetWordType(string word)
        {
            ParseState parseState = ParseState.None;
            if (word.Equals(Hundred))
            {
                parseState = ParseState.HundredSeparators;
            }
            else if (word.Equals(And))
            {
                parseState = ParseState.And;
            }
            else if (word.Equals(Only))
            {
                parseState = ParseState.Only;
            }
            else if (multiples.ContainsKey(word))
            {
                parseState = ParseState.Multiplier;
            }
            else if (seperators.Contains(word))
            {
                parseState = ParseState.Separator;
            }
            else if (tens.ContainsKey(word) || teens.ContainsKey(word))
            {
                parseState = ParseState.FirstWord;
            }
            else if (digits.ContainsKey(word))
            {
                parseState = ParseState.SecondDigit;
            }

            return parseState;
        }
    }
}
