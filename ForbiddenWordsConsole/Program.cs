using System;
using System.Collections.Generic;
using ForbiddenWordsLib;

namespace ForbiddenWordsConsole
{
    internal static class Program
    {
        private static void Main()
        {
            var fWUtils = new ForbiddenWordUtils();
            //var forbiddenWords = new List<ForbiddenWord>
            //{
            //    new ForbiddenWord("I", 10),
            //    new ForbiddenWord("N", 30),
            //    new ForbiddenWord("W", 10),
            //    new ForbiddenWord("WI", 1),
            //    new ForbiddenWord("WW", 10),
            //    new ForbiddenWord("II", 11),
            //    new ForbiddenWord("WIW", 3),
            //    new ForbiddenWord("IWI", 2)
            //};

            //var bestWord = fWUtils.MakeBestWord(10, forbiddenWords.Count, forbiddenWords);
            //Console.WriteLine($"Best: {bestWord.Word}\nPenalty: {bestWord.Penalty}");
            int M;
            var strName = "test.txt";
            var forbiddenWords = ForbiddenWordsLib.WorkFile.ReadFile(strName, out M);

            var bestWord = fWUtils.MakeBestWord(M, forbiddenWords.Count, forbiddenWords);
            Console.WriteLine($"Best: {bestWord.Word}\nPenalty: {bestWord.Penalty}");
        }
    }
}
