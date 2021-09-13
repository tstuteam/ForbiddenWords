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
            var forbiddenWords = new List<ForbiddenWord>();
            var wordRepetitions = new List<ForbiddenWord>();
            var usedLetters = new HashSet<char>();

            Console.WriteLine("Sequence length");
            var sequenceLength = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the number of forbidden words");
            var forbiddenWordsCount = int.Parse(Console.ReadLine());


            for (var i = 0; i < forbiddenWordsCount; ++i)
            {
                forbiddenWords.Add(new ForbiddenWord());
                wordRepetitions.Add(new ForbiddenWord());
            }

            Console.WriteLine("Enter forbidden words and their penalties");

            for (var i = 0; i < forbiddenWordsCount; ++i)
            {
                var line = Console.ReadLine();
                var splitLine = line.Split(' ');

                forbiddenWords[i].Word = splitLine[0].ToUpper();
                forbiddenWords[i].Penalty = int.Parse(splitLine[1]);

                if (forbiddenWords[i].Word.Length == 1)
                    usedLetters.Add(forbiddenWords[i].Word[0]);
            }

            var lcm = MathUtils.LCM(forbiddenWords[0].Word.Length, forbiddenWords[1].Word.Length);

            for (var i = 2; i < forbiddenWordsCount; ++i)
            {
                lcm = MathUtils.LCM(forbiddenWords[i].Word.Length, lcm);
            }

            for (var i = 0; i < forbiddenWordsCount; ++i)
            {
                while (wordRepetitions[i].Word.Length < lcm)
                    wordRepetitions[i].Word += forbiddenWords[i].Word;

                wordRepetitions[i].Penalty = strUtils.GetStringPenalty(wordRepetitions[i].Word, forbiddenWords);
            }


            var bestSequence = strUtils.BuildResult(0, sequenceLength, wordRepetitions);
            var minPenalty = strUtils.GetStringPenalty(bestSequence, forbiddenWords);

            for (var i = 1; i < forbiddenWordsCount; ++i)
            {
                var sequence = strUtils.BuildResult(i, sequenceLength, wordRepetitions);
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

            for (var i = 1; i <= bestSequence.Length - sequenceLength; ++i)
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
