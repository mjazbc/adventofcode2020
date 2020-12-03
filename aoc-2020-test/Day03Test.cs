using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aoc_2020_test
{
    [TestClass]
    public class Day03Test : BaseTest
    {
        [TestMethod]
        public void FirstPuzzle_TestInput01()
        {
            Puzzle.Input.LoadFromFile("../../../inputs/day03/test01.txt");
            Assert.AreEqual("7", Puzzle.SolveFirstPuzzle());
        }

        [TestMethod]
        public void SecondPuzzle_TestInput01()
        {
            Puzzle.Input.LoadFromFile("../../../inputs/day03/test01.txt");
            Assert.AreEqual("336", Puzzle.SolveSecondPuzzle());
        }

    }
}
