using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace ForbiddenWordsLib
{
    /// <summary>
    ///     The class for storing the forbidden word and its penalty
    /// </summary>
    public class ForbiddenWord
    {
        public int Penalty;
        public string Word;

        public ForbiddenWord()
        {
            Word = "";
            Penalty = 0;
        }

        public ForbiddenWord(string word, int penalty)
        {
            Word = word;
            Penalty = penalty;
        }
    }

    /// <summary>
    ///     Class for working with a file
    /// </summary>
    public static class WorkFile
    {
        /// <summary>
        ///     Read the file.
        ///     Parse each line according to the problem statement.
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="m">Output word length M</param>
        /// <returns>Array of strings of forbidden words</returns>
        public static List<ForbiddenWord> ReadFile(string fileName, out int m)
        {
            var fileContent = new List<string>();
            var file = new StreamReader(fileName);

            m = int.Parse(file.ReadLine() ?? string.Empty);
            var lsw = int.Parse(file.ReadLine() ?? string.Empty);

            for (var i = 0; i < lsw; i++)
                fileContent.Add(file.ReadLine());

            file.Close();

            return ConvertFileToForbiddenWords(fileContent);
        }

        /// <summary>
        ///     Converts an array of strings to a list of forbidden words.
        /// </summary>
        /// <param name="fileContent">File content</param>
        /// <returns>List of forbidden words</returns>
        private static List<ForbiddenWord> ConvertFileToForbiddenWords(List<string> fileContent)
        {
            var forbiddenWords = new List<ForbiddenWord>();

            foreach (var line in fileContent)
            {
                var buffer = line.Split(' ');

                forbiddenWords.Add(new ForbiddenWord(buffer[0], int.Parse(buffer[1])));
            }

            return forbiddenWords;
        }
    }


    /// <summary>
    ///     Class for working with forbidden words.
    /// </summary>
    public class ForbiddenWordUtils
    {
        /// <summary>
        ///     In the three-letter alphabet `WIN`,
        ///     it is required to write the word `Best`
        ///     of a given length `M`, minimizing
        ///     the penalty for the occurrence of
        ///     forbidden words in the written word.
        /// </summary>
        /// <param name="sequenceLength">Output word length</param>
        /// <param name="forbiddenWordsCount">List length</param>
        /// <param name="forbiddenWords">List of forbidden words</param>
        /// <returns>The word `Best`</returns>
        public ForbiddenWord MakeBestWord(int sequenceLength, int forbiddenWordsCount,
            List<ForbiddenWord> forbiddenWords)
        {
            var wordRepetitions = new List<ForbiddenWord>();
            for (var i = 0; i < forbiddenWordsCount; ++i)
                wordRepetitions.Add(new ForbiddenWord());

            ExpandWord(forbiddenWords, wordRepetitions);

            var bestWord = findBestSequenceWithShift(forbiddenWords,
                FindBestSequence(forbiddenWords,
                    wordRepetitions,
                    sequenceLength),
                sequenceLength);

            return bestWord;
        }

        /// <summary>
        ///     Brute-force implementation of `MakeBestWord`.
        ///     
        ///     In the three-letter alphabet `WIN`,
        ///     it is required to write the word `Best`
        ///     of a given length `M`, minimizing
        ///     the penalty for the occurrence of
        ///     forbidden words in the written word.
        /// </summary>
        /// <param name="sequenceLength">Output word length</param>
        /// <param name="forbiddenWords">List of forbidden words</param>
        /// <returns>The word `Best`</returns>
        public ForbiddenWord MakeBestWordBruteForce(int sequenceLength, List<ForbiddenWord> forbiddenWords)
		{
            var builder = new StringBuilder(sequenceLength, sequenceLength);
            builder.Length = sequenceLength;

            var bestWord = new ForbiddenWord(string.Empty, int.MaxValue);

            MakeBestWordBruteForceRecursive(builder, sequenceLength, forbiddenWords, ref bestWord);

            return bestWord;
		}

        /// <summary>
        ///     Goes through all possible combinations of `W`, `I` and `N`
        ///     and outputs a sequence with the smallest penalty.
        /// </summary>
        /// <param name="builder">String builder of length <paramref name="sequenceLength"/></param>
        /// <param name="sequenceLength">Length of the generated sequence</param>
        /// <param name="forbiddenWords">List of forbidden words</param>
        /// <param name="bestWord">`ForbiddenWord` object to output to</param>
        private void MakeBestWordBruteForceRecursive(StringBuilder builder, int sequenceLength,
            List<ForbiddenWord> forbiddenWords, ref ForbiddenWord bestWord)
		{
            if (sequenceLength == 0)
            {
                var word = builder.ToString();
                var penalty = StringUtils.GetStringPenalty(word, forbiddenWords);

                if (penalty < bestWord.Penalty)
				{
                    bestWord.Word = word;
                    bestWord.Penalty = penalty;
				}

                return;
            }

            builder[--sequenceLength] = 'W';
            MakeBestWordBruteForceRecursive(builder, sequenceLength, forbiddenWords, ref bestWord);

            builder[sequenceLength] = 'I';
            MakeBestWordBruteForceRecursive(builder, sequenceLength, forbiddenWords, ref bestWord);

            builder[sequenceLength] = 'N';
            MakeBestWordBruteForceRecursive(builder, sequenceLength, forbiddenWords, ref bestWord);
        }

        /// <summary>
        ///     Finding the Best Sequence by Shifting the Double Sequence
        /// </summary>
        /// <param name="forbiddenWords">List of forbidden words</param>
        /// <param name="sequence">Best sequence at the current time</param>
        /// <param name="sequenceLength">Output word length</param>
        /// <returns>The word `Best`</returns>
        private ForbiddenWord findBestSequenceWithShift(List<ForbiddenWord> forbiddenWords, ForbiddenWord sequence,
            int sequenceLength)
        {
            var minWord = new ForbiddenWord(sequence.Word, sequence.Penalty);
            var bestSequence = sequence.Word + sequence.Word;

            for (var i = 1; i <= bestSequence.Length - sequenceLength; ++i)
            {
                var shiftSequence = bestSequence.Substring(i, sequenceLength);
                var sequencePenalty = StringUtils.GetStringPenalty(shiftSequence, forbiddenWords);

                if (sequencePenalty < minWord.Penalty)
                {
                    minWord.Word = shiftSequence;
                    minWord.Penalty = sequencePenalty;
                }
            }

            return minWord;
        }

        /// <summary>
        ///     Adds missing characters to the resulting string up to <paramref name="sequenceLength" />
        /// </summary>
        /// <param name="wordIndex">The index of the word</param>
        /// <param name="sequenceLength">Output word length</param>
        /// <param name="wordRepetitions">Repeated forbidden words</param>
        /// <returns>The repeated string</returns>
        private string CompleteSequence(int wordIndex, int sequenceLength, List<ForbiddenWord> wordRepetitions)
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

        /// <summary>
        ///     Finding the best sequence by adding characters up to sequenceLength
        /// </summary>
        /// <param name="forbiddenWords">List of forbidden words</param>
        /// <param name="wordRepetitions">Repeated forbidden words</param>
        /// <param name="sequenceLength">Output word length</param>
        /// <returns>Best sequence that can be improved</returns>
        private ForbiddenWord FindBestSequence(List<ForbiddenWord> forbiddenWords, List<ForbiddenWord> wordRepetitions,
            int sequenceLength)
        {
            var bestSequence = CompleteSequence(0, sequenceLength, wordRepetitions);
            var minPenalty = StringUtils.GetStringPenalty(bestSequence, forbiddenWords);

            for (var i = 1; i < forbiddenWords.Count; ++i)
            {
                var sequence = CompleteSequence(i, sequenceLength, wordRepetitions);
                var penalty = StringUtils.GetStringPenalty(sequence, forbiddenWords);

                if (penalty < minPenalty)
                {
                    bestSequence = sequence;
                    minPenalty = penalty;
                }
            }

            return new ForbiddenWord(bestSequence, minPenalty);
        }

        /// <summary>
        ///     Expanding all words to the desired length.
        ///     Changing wordRepetitions.
        /// </summary>
        /// <param name="forbiddenWords">List of forbidden words</param>
        /// <param name="wordRepetitions">Output of repeated forbidden words</param>
        private void ExpandWord(List<ForbiddenWord> forbiddenWords, List<ForbiddenWord> wordRepetitions)
        {
            var indexes = findIndexes(forbiddenWords);

            var lcm = MathUtils.Lcm(forbiddenWords[indexes.Item1].Word.Length,
                forbiddenWords[indexes.Item2].Word.Length);

            for (var i = 0; i < forbiddenWords.Count; ++i)
            {
                while (wordRepetitions[i].Word.Length < lcm)
                    wordRepetitions[i].Word += forbiddenWords[i].Word;

                wordRepetitions[i].Penalty = StringUtils.GetStringPenalty(wordRepetitions[i].Word, forbiddenWords);
            }
        }

        /// <summary>
        ///     We find the indices of the two largest penalties
        /// </summary>
        /// <param name="forbiddenWords">List of forbidden words</param>
        /// <returns>A named tuple of two indexes of the two largest penalties</returns>
        private (int, int) findIndexes(List<ForbiddenWord> forbiddenWords)
        {
            var firstIndex = 0;
            var secondIndex = 0;
            for (var i = forbiddenWords.Count - 1; i >= 1; i--)
            {
                if (forbiddenWords[i].Word.Length == forbiddenWords[i - 1].Word.Length)
                    continue;

                firstIndex = i;
                secondIndex = i - 1;
                break;
            }

            return (firstIndex, secondIndex);
        }
    }

    /// <summary>
    ///     A class for working with strings
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        ///     Finds the number of occurrences of <paramref name="substr" /> in <paramref name="str" />
        /// </summary>
        /// <param name="str">The string to search in</param>
        /// <param name="substr">The substring to search for</param>
        /// <returns>The number of occurrences</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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
        /// <param name="forbiddenWords">List of forbidden words</param>
        /// <returns>The string penalty</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int GetStringPenalty(string str, List<ForbiddenWord> forbiddenWords)
        {
            var penalty = 0;

            foreach (var sequence in forbiddenWords)
                penalty += sequence.Penalty * Occurrences(str, sequence.Word);

            return penalty;
        }
    }

    /// <summary>
    ///     A class for working with mathematics
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        ///     Swaps the parameter values using a ref
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
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
        private static int Gcd(int a, int b)
        {
            // Stein's binary GCD algorithm
            // Base cases: gcd(n, 0) = gcd(0, n) = n
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            // Extract common factor-2: gcd(2ⁱ n, 2ⁱ m) = 2ⁱ gcd(n, m)
            // and reducing until odd gcd(2ⁱ n, m) = gcd(n, m) if m is odd
            var kA = BitOperations.TrailingZeroCount(a);
            var kB = BitOperations.TrailingZeroCount(b);
            a >>= kA;
            b >>= kB;
            var k = Math.Min(kA, kB);

            while (true)
            {
                if (a > b)
                    Swap(ref a, ref b);
                b -= a;

                if (b == 0)
                    return a << k;

                b >>= BitOperations.TrailingZeroCount(b);
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
