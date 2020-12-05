using aoc_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aoc_2020_test
{
    [TestClass]
    public class Day05Test : BaseTest
    {
        [TestMethod]
        public void GetNumber_GetsRowNumber()
        {
            int number = ((Day05)Puzzle).GetNumber('B', "FBFBBFF".ToCharArray());
            Assert.AreEqual(44, number);
        }

        [TestMethod]
        public void GetNumber_GetsColumnNumber()
        {
            int number = ((Day05)Puzzle).GetNumber('R', "RLR".ToCharArray());
            Assert.AreEqual(5, number);
        }

        [TestMethod]
        public void CalcSeatId_CalculatesSeatId()
        {
            int id = ((Day05)Puzzle).CalcSeatId(44, 5);
            Assert.AreEqual(357, id);
        }

        [TestMethod]
        public void GetBoardingPassSeatId_GetsSeatId01()
        {
            int id = ((Day05)Puzzle).GetBoardingPassSeatId("BFFFBBFRRR");
            Assert.AreEqual(567, id);
        }

        [TestMethod]
        public void GetBoardingPassSeatId_GetsSeatId02()
        {
            int id = ((Day05)Puzzle).GetBoardingPassSeatId("FFFBBBFRRR");
            Assert.AreEqual(119, id);
        }

        [TestMethod]
        public void GetBoardingPassSeatId_GetsSeatId03()
        {
            int id = ((Day05)Puzzle).GetBoardingPassSeatId("BBFFBBFRLL");
            Assert.AreEqual(820, id);
        }

        [TestMethod]
        public void SecondPuzzle_TestInput01()
        {
            Puzzle.Input.LoadFromFile("../../../inputs/day05/test01.txt");
            Assert.AreEqual("820", Puzzle.SolveFirstPuzzle());
        }
    }
}
