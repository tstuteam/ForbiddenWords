using System;
using System.Collections.Generic;
using ForbiddenWordsLib;

namespace ForbiddenWordsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var strUtils = new StringUtils();
            var mathUtils = new MathUtils();
            var forbiddenWords = new List<ForbiddenWord>();
            var wordRepetitions = new List<ForbiddenWord>();

            Console.WriteLine("Sequence length");
            int sequenceLength = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the number of forbidden words");
            int forbiddenWordsCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < sequenceLength; i++)
            {
                forbiddenWords.Add(new ForbiddenWord());
                wordRepetitions.Add(new ForbiddenWord());
            }

            Console.WriteLine("Enter forbidden words and their penalties");

            var usedLetters = new HashSet<char>();

            for (int i = 0; i < forbiddenWordsCount; ++i)
            {
                var line = Console.ReadLine();
                var splitLine = line.Split(' ');

                forbiddenWords[i].forbiddenWord = splitLine[0].ToUpper();
                forbiddenWords[i].penalty = int.Parse(splitLine[1]);

                if (forbiddenWords[i].forbiddenWord.Length == 1)
                    usedLetters.Add(forbiddenWords[i].forbiddenWord[0]);
            }

            int lcm = mathUtils.LCM(forbiddenWords[0].forbiddenWord.Length, forbiddenWords[1].forbiddenWord.Length);

            for (int i = 2; i < forbiddenWordsCount; ++i)
            {
                lcm = mathUtils.LCM(forbiddenWords[i].forbiddenWord.Length, lcm);
            }

            for (int i = 0; i < forbiddenWordsCount; ++i)
            {
                while (wordRepetitions[i].forbiddenWord.Length < lcm)
                    wordRepetitions[i].forbiddenWord += forbiddenWords[i].forbiddenWord;

                wordRepetitions[i].penalty = strUtils.GetStringPenalty(wordRepetitions[i].forbiddenWord, forbiddenWords);
            }


            string bestSequence = strUtils.BuildString(0, sequenceLength, wordRepetitions);
            int minPenalty = strUtils.GetStringPenalty(bestSequence, forbiddenWords);

            for (int i = 1; i < forbiddenWordsCount; ++i)
            {
                var sequence = strUtils.BuildString(i, sequenceLength, wordRepetitions);
                var penalty = strUtils.GetStringPenalty(sequence, forbiddenWords);

                if (penalty < minPenalty)
                {
                    bestSequence = sequence;
                    minPenalty = penalty;
                }
            }

            bestSequence += bestSequence;

            var minSequence = bestSequence.Substring(0, sequenceLength);
            minPenalty = strUtils.GetStringPenalty(minSequence, forbiddenWords);

            for (int i = 1; i <= bestSequence.Length - sequenceLength; ++i)
            {
                var sequence = bestSequence.Substring(i, sequenceLength);
                var sequencePenalty = strUtils.GetStringPenalty(sequence, forbiddenWords);

                if (sequencePenalty < minPenalty)
                {
                    minSequence = sequence;
                    minPenalty = sequencePenalty;
                }
            }

            Console.WriteLine("Penalty: {0}", minPenalty);
            Console.WriteLine("Sequence: {0}", minSequence);
        }
    }
}
