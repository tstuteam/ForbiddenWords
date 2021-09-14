using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Linq;

namespace ForbiddenWordsLib
{
    public class ForbiddenWord
    {
        public string Word;
        public int Penalty;

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
    /// working with a file
    /// </summary>
    public class WorkFile
    {
        /// <summary>
        /// Reading a file
        /// </summary>
        /// <param name="strName">file name</param>
        /// <param name="M">Длина выходного слова М</param>
        /// <returns>Array of strings of forbidden words</returns>
        public static List<ForbiddenWord> ReadFile(string strName, out int M)
        {
            var fileContent = new List<string>();
            var file = new StreamReader(strName);

            M = int.Parse(file.ReadLine() ?? string.Empty);
            var lsw = int.Parse(file.ReadLine() ?? string.Empty);

            for (int i = 0; i < lsw; i++)
                fileContent.Add(file.ReadLine());

            file.Close();

            return ConvertFileToForbiddenWords(fileContent);
        }

        /// <summary>
        /// Converts an array to a list
        /// </summary>
        /// <param name="fileContent">array</param>
        /// <returns>list</returns>
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
    /// 
    /// </summary>
    public class ForbiddenWordUtils
    {

        // TODO(andreymlv): Исправить комментарии

        /// <summary>
        ///     В трёхбуквенном алфавите `WIN` требуется написать слово Best заданной длины `М`,
        ///     минимизируя штраф за вхождение запрещенных слов в написанное слово.
        /// </summary>
        /// <param name="sequenceLength">Длина выходного слова</param>
        /// <param name="forbiddenWordsCount">Длина списка</param>
        /// <param name="forbiddenWords">Список из запрещённых слов</param>
        /// <returns> Слово `Best` </returns>
        public ForbiddenWord MakeBestWord(int sequenceLength, int forbiddenWordsCount,
            List<ForbiddenWord> forbiddenWords)
        {
            // заготовка для слова
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
        ///     Находим лучшую последовательность с помощью сдвига двойной последовательности
        /// </summary>
        /// <param name="forbiddenWords"></param>
        /// <param name="sequence"></param>
        /// <param name="sequenceLength"></param>
        /// <returns></returns>
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
        ///     Добавляет к результирующей строке недостастающие символы до <paramref name="sequenceLength" />
        /// </summary>
        /// <param name="wordIndex">The index of the word</param>
        /// <param name="sequenceLength"></param>
        /// <param name="wordRepetitions"></param>
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
        ///     Находим лучшую последовательность, добавляя символы до sequenceLength
        /// </summary>
        /// <param name="forbiddenWords"></param>
        /// <param name="wordRepetitions"></param>
        /// <param name="sequenceLength"></param>
        /// <returns></returns>
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
        ///     Expanding all words to the desired length
        /// </summary>
        /// <param name="forbiddenWords">Словарь со штрафами</param>
        /// <param name="wordRepetitions">Пустой список</param>
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
        ///     Находим индексы двух самых больших штрафов
        /// </summary>
        /// <param name="forbiddenWords">Словарь со штрафами</param>
        /// <returns>!НЕ ПОНЯТНО!5 и 6?</returns>
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
    /// 
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
        /// <param name="forbiddenWords">Словарь запрещённых слов</param>
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
    /// 
    /// </summary>
    public static class MathUtils
    {
        // TODO(andreymlv): Исправить комментарии
        /// <summary>
        /// перестановка a = b, и наоборот        
        /// </summary>
        /// <param name="a">Число которое надо поменять</param>
        /// <param name="b">Число на которое надо поменять</param>
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
