using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aoc_2020_test
{
    [TestClass]
    public class Day04Test : BaseTest
    {
        [TestMethod]
        public void FirstPuzzle_TestInput01()
        {
            Puzzle.Input.LoadFromFile("../../../inputs/day04/test01.txt");
            Assert.AreEqual("2", Puzzle.SolveFirstPuzzle());
        }

        [TestMethod]
        public void SecondPuzzle_TestInput02()
        {
            Puzzle.Input.LoadFromFile("../../../inputs/day04/test02.txt");
            Assert.AreEqual("0", Puzzle.SolveSecondPuzzle());
        }

        [TestMethod]
        public void SecondPuzzle_TestInput03()
        {
            Puzzle.Input.LoadFromFile("../../../inputs/day04/test03.txt");
            Assert.AreEqual("4", Puzzle.SolveSecondPuzzle());
        }

    }
}
