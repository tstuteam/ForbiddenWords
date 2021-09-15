using System;
using ForbiddenWordsLib;

namespace ForbiddenWordsConsole
{
    internal static class Program
    {
        private static void Main()
        {
            var fWUtils = new ForbiddenWordUtils();
            var strName = "test.txt";
            var forbiddenWords = WorkFile.ReadFile(strName, out var m);
            var bestWord = fWUtils.MakeBestWord(m, forbiddenWords.Count, forbiddenWords);
            Console.WriteLine($"Best: {bestWord.Word}\nPenalty: {bestWord.Penalty}");
        }
    }
}
