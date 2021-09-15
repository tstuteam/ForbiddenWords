using System;
using System.Collections.Generic;
using ForbiddenWordsLib;
using Xunit;

namespace ForbiddenWordsLibUnitTests
{
    public class ForbiddenWordsTest
    {
        [Fact]
        public void TestLcmWithValidInput()
        {
            Assert.Equal(6, MathUtils.Lcm(2, 3));
        }

        [Fact]
        public void TestLcmWithBigValidInput()
        {
            Assert.Equal(864192, MathUtils.Lcm(123456, 42));
        }

        [Fact]
        public void TestLcmWithInvalidInput()
        {
            Assert.Throws<DivideByZeroException>(() => MathUtils.Lcm(0, 0));
        }

        [Fact]
        public void TestLcmWithEqualInput()
        {
            Assert.Equal(2, MathUtils.Lcm(2, 2));
        }

        [Fact]
        public void TestGetStringPenaltyWithValidInput()
        {
            var forbiddenWords = new List<ForbiddenWord>
            {
                new("I", 10),
                new("N", 30),
                new("W", 10)
            };

            const int expected = 50;
            var actual = StringUtils.GetStringPenalty("WIN", forbiddenWords);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestGetStringPenaltyWithBigValidInput()
        {
            var forbiddenWords = new List<ForbiddenWord>
            {
                new("I", 1),
                new("N", 1),
                new("W", 1)
            };


            for (var i = 0; i < 50 - 3; i++)
                forbiddenWords.Add(new ForbiddenWord("WINI", 1));


            var str = "WINI";

            for (var i = 0; i < 24; i++)
                str += "WINI";

            const int expected = 1275;
            var actual = StringUtils.GetStringPenalty(str, forbiddenWords);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestGetStringPenaltyWithOccurrencesInput()
        {
            var forbiddenWords = new List<ForbiddenWord>
            {
                new("I", 10),
                new("N", 30),
                new("W", 10),
                new("I", 50),
                new("N", 70),
                new("W", 120)
            };

            const int expected = 290;
            var actual = StringUtils.GetStringPenalty("WIN", forbiddenWords);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestGetStringPenaltyWithInvalidInput()
        {
            Assert.Throws<NullReferenceException>(() => StringUtils.GetStringPenalty(null, null));
        }

        [Fact]
        public void TestMakeBestWordWithValidInput()
        {
            var fWUtils = new ForbiddenWordUtils();

            var forbiddenWords = new List<ForbiddenWord>
            {
                new("I", 10),
                new("N", 30),
                new("W", 10),
                new("WI", 1),
                new("WW", 10),
                new("II", 11),
                new("WIW", 3),
                new("IWI", 2)
            };

            var bestWord = fWUtils.MakeBestWord(8, forbiddenWords.Count, forbiddenWords);

            Assert.Equal("IWIWIWIW", bestWord.Word);
            Assert.Equal(98, bestWord.Penalty);
        }

        [Fact]
        public void TestMakeBestWordBruteForceValidInput()
        {
            var fWUtils = new ForbiddenWordUtils();

            var forbiddenWords = new List<ForbiddenWord>
            {
                new("I", 10),
                new("N", 30),
                new("W", 10),
                new("WI", 1),
                new("WW", 10),
                new("II", 11),
                new("WIW", 3),
                new("IWI", 2)
            };

            var bestWord = fWUtils.MakeBestWordBruteForce(8, forbiddenWords);

            Assert.Equal("IWIWIWIW", bestWord.Word);
            Assert.Equal(98, bestWord.Penalty);
        }

        [Fact]
        public void TestMakeBestWordWithEmptyInput()
        {
            var fWUtils = new ForbiddenWordUtils();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                fWUtils.MakeBestWord(1, new List<ForbiddenWord>().Capacity, new List<ForbiddenWord>()));
        }

        [Fact]
        public void TestMakeBestWordWithInvalidInput()
        {
            var fWUtils = new ForbiddenWordUtils();

            Assert.Throws<NullReferenceException>(() => fWUtils.MakeBestWord(8, 0, null));
        }

        [Fact]
        public void TestReadFileValidData()
        {
            var forbiddenWords = new List<ForbiddenWord>
            {
                new("I", 10),
                new("N", 30),
                new("W", 10)
            };

            var lists = WorkFile.ReadFile("true.txt", out var m);
            Assert.Equal(8, m);
            Assert.True(CheckListWithForbiddenWords(lists, forbiddenWords));
        }

        private static bool CheckListWithForbiddenWords(List<ForbiddenWord> forbiddenWords1,
            List<ForbiddenWord> forbiddenWords2)
        {
            for (var i = 0; i < forbiddenWords1.Count; i++)
            {
                if (forbiddenWords1[i].Word != forbiddenWords2[i].Word)
                    return false;
                if (forbiddenWords1[i].Penalty != forbiddenWords2[i].Penalty)
                    return false;
            }

            return true;
        }

        [Fact]
        public void TestReadFileEmptyFile()
        {
            Assert.Throws<FormatException>(() => WorkFile.ReadFile("null.txt", out _));
        }

        [Fact]
        public void TestWorkReadFileIncorrectData()
        {
            Assert.Throws<FormatException>(() => WorkFile.ReadFile("Incorrect.txt", out _));
        }
    }
}
