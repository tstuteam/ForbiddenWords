using System.Collections.Generic;

namespace ForbiddenWordsLib
{
    public class ForbiddenWord
    {
        public string forbiddenWord;
        public int penalty;

        public ForbiddenWord()
        {
            forbiddenWord = "";
            penalty = 0;
        }
    }

    public class StringUtils
    {
        /// <summary>
        /// Finds the number of occurrences of <paramref name="substr"/> in <paramref name="str"/>
        /// </summary>
        /// <param name="str">The string to search in</param>
        /// <param name="substr">The substring to search for</param>
        /// <returns>The number of occurrences</returns>
        public int Occurrences(string str, string substr)
        {
            int occurrences = 0;
            int pos = 0;

            while ((pos = str.IndexOf(substr, pos)) != -1)
            {
                ++occurrences;
                ++pos;
            }

            return occurrences;
        }

        /// <summary>
        /// Calculates the string penalty
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>The string penalty</returns>
        public int GetStringPenalty(string str, List<ForbiddenWord> forbiddenWords)
        {
            // TODO(andreymlv): Make propper comments (arguments)
            int penalty = 0;

            foreach (var sequence in forbiddenWords)
                penalty += sequence.penalty * Occurrences(str, sequence.forbiddenWord);

            return penalty;
        }

        /// <summary>
        /// Repeats the word under index <paramref name="wordIndex"/> and
        /// writes it to wordRepetitions
        /// </summary>
        /// <param name="wordIndex">The index of the word</param>
        /// <returns>The repeated string</returns>
        public string BuildString(int wordIndex, int sequenceLength, List<ForbiddenWord> wordRepetitions)
        {
            // TODO(andreymlv): Make propper comments (arguments)
            int charIndex = 0;
            string result = "";

            while (result.Length < sequenceLength)
            {
                var sequenceText = wordRepetitions[wordIndex].forbiddenWord;

                result += sequenceText[charIndex];
                charIndex = (charIndex + 1) % sequenceText.Length;
            }

            return result;
        }
    }

    public class MathUtils
    {
        /// <summary>
        /// Calculates the greatest common divisor of two numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Greatest common divisor</returns>
        public int GCD(int a, int b)
        {
            if (b == 0)
                return a;
            return GCD(b, a % b);
        }

        /// <summary>
        /// Calculates the least common multiple of two numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Least common multiple</returns>
        public int LCM(int a, int b)
        {
            return a * b / GCD(a, b);
        }
    }
}
