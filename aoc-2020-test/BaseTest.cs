using System;
using System.Linq;
using System.Reflection;
using aoc_2020;
using aoc_core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aoc_2020_test
{
    public abstract class BaseTest
    {
        public AdventPuzzle Puzzle {get; private set;}
        
        private const string SolutionsAssemblyName = "aoc-2020";
        [TestInitialize]
        public void Initialize()
        {
            string className = this.GetType().Name;
            string dayName = className.Replace("Test", "");

            var t = GetDayType(dayName);
            Puzzle = Activator.CreateInstance(t) as AdventPuzzle;
        }

        private Type GetDayType(string dayName)
        {
            var _ = typeof(Day01).Assembly; // Force aoc-2020 assembly loading

            var referenced = Assembly
            .GetExecutingAssembly()
            .GetReferencedAssemblies()
            .First(x => x.Name == SolutionsAssemblyName);
            
            var loaded = Assembly.Load(referenced);
            var t = loaded.GetTypes().First(x => x.Name == dayName);

            return t;
        }
    }

}