using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aoc_2020_test
{
    [TestClass]
    public class Day01Test : BaseTest
    {
        [TestMethod]
        public void FirstPuzzle_TestInput01()
        {
            Puzzle.Input.LoadFromFile("../../../inputs/day01/test01.txt");
            Assert.AreEqual("514579", Puzzle.SolveFirstPuzzle());
        }

        
        [TestMethod]
        public void SecondPuzzle_TestInput01()
        {
            Puzzle.Input.LoadFromFile("../../../inputs/day01/test01.txt");
            Assert.AreEqual("241861950", Puzzle.SolveSecondPuzzle());
        }
    }
}
