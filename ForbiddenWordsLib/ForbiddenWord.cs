using System;
using System.Collections.Generic;

namespace ForbiddenWordsLib
{
    public class ForbiddenWord
    {
        public int Penalty;
        public string Word;

        public ForbiddenWord()
        {
            Word = "";
            Penalty = 0;
        }
    }

    public class StringUtils
    {
        /// <summary>
        ///     Finds the number of occurrences of <paramref name="substr" /> in <paramref name="str" />
        /// </summary>
        /// <param name="str">The string to search in</param>
        /// <param name="substr">The substring to search for</param>
        /// <returns>The number of occurrences</returns>
        private static int Occurrences(string str, string substr)
        {
            var occurrences = 0;
            var pos = 0;

            while ((pos = str.IndexOf(substr, pos, StringComparison.Ordinal)) != -1)
            {
                ++occurrences;
                ++pos;
            }

            return occurrences;
        }

        /// <summary>
        ///     Calculates the string penalty
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>The string penalty</returns>
        public int GetStringPenalty(string str, List<ForbiddenWord> forbiddenWords)
        {
            var penalty = 0;

            foreach (var sequence in forbiddenWords)
                penalty += sequence.Penalty * Occurrences(str, sequence.Word);

            return penalty;
        }

        /// <summary>
        ///     Repeats the word under index <paramref name="wordIndex" /> and
        ///     writes it to wordRepetitions
        /// </summary>
        /// <param name="wordIndex">The index of the word</param>
        /// <returns>The repeated string</returns>
        public string BuildResult(int wordIndex, int sequenceLength, List<ForbiddenWord> wordRepetitions)
        {
            var charIndex = 0;
            var result = "";

            while (result.Length < sequenceLength)
            {
                var sequenceText = wordRepetitions[wordIndex].Word;

                result += sequenceText[charIndex];
                charIndex = (charIndex + 1) % sequenceText.Length;
            }

            return result;
        }
    }

    public static class MathUtils
    {
        private static readonly int[] Lookup =
        {
            32, 0, 1, 26, 2, 23, 27, 0, 3, 16, 24, 30, 28, 11, 0, 13, 4, 7, 17,
            0, 25, 22, 31, 15, 29, 10, 12, 6, 0, 21, 14, 9, 5, 20, 8, 19, 18
        };

        private static int TrailingZeros(int i)
        {
            return Lookup[(i & -i) % 37];
        }

        private static void Swap(ref int a, ref int b)
        {
            (a, b) = (b, a);
        }

        /// <summary>
        ///     Calculates the greatest common divisor of two numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Greatest common divisor</returns>
        public static int Gcd(int a, int b)
        {
            // Stein's binary GCD algorithm
            // Base cases: gcd(n, 0) = gcd(0, n) = n
            if (a == 0) {
                return b;
            } else if (b == 0) {
                return a;
            }

            // Extract common factor-2: gcd(2ⁱ n, 2ⁱ m) = 2ⁱ gcd(n, m)
            // and reducing until odd gcd(2ⁱ n, m) = gcd(n, m) if m is odd
            var kA = TrailingZeros(a);
            var kB = TrailingZeros(b);
            a >>= kA;
            b >>= kB;
            var k = Math.Min(kA, kB);
            
            while (true)
            {
                if (a > b) {
                    Swap(ref a, ref b);
                }
                b -= a;

                if (b == 0) {
                    return a << k;
                }

                b >>= TrailingZeros(b);
            }
        }

        /// <summary>
        ///     Calculates the least common multiple of two numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Least common multiple</returns>
        public static int Lcm(int a, int b)
        {
            return a * b / Gcd(a, b);
        }
    }
}
