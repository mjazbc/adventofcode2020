using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aoc_2020_test
{
    [TestClass]
    public class Day01Test : BaseTest
    {
        [TestMethod]
        public void FirstPuzzle_TestInput01()
        {
            Puzzle.Input.Load(2);
            Assert.AreEqual("4", Puzzle.SolveFirstPuzzle());
        }
    }
}
