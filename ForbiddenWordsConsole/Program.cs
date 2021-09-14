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
            var strName = "test.txt";
            var forbiddenWords = ForbiddenWordsLib.WorkFile.ReadFile(strName, out int M);
            var bestWord = fWUtils.MakeBestWord(M, forbiddenWords.Count, forbiddenWords);
            Console.WriteLine($"Best: {bestWord.Word}\nPenalty: {bestWord.Penalty}");
        }
    }
}
