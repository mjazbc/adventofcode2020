using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aoc_2020_test
{
    [TestClass]
    public class Day02Test : BaseTest
    {
        [TestMethod]
        public void FirstPuzzle_TestInput01()
        {
            Puzzle.Input.LoadFromFile("../../../inputs/day02/test01.txt");
            Assert.AreEqual("2", Puzzle.SolveFirstPuzzle());
        }

        [TestMethod]
        public void SecondPuzzle_TestInput01()
        {
            Puzzle.Input.LoadFromFile("../../../inputs/day02/test01.txt");
            Assert.AreEqual("1", Puzzle.SolveSecondPuzzle());
        }

    }
}
