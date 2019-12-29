using System.Collections.Generic;
using System.Linq;
using Countdown.net.Controllers;
using Countdown.net.Model;
using Xunit;
using Xunit.Abstractions;

namespace Countdown.net.Test
{
    public class TestNumbersController
    {
        private readonly ITestOutputHelper _output;
        private readonly NumbersController _numbersController = new NumbersController(TestsHelper.GetApplicationConfiguration());

        public TestNumbersController(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Theory]
        [InlineData("100,3,5,10", 1, 20)]
        [InlineData("100,3,5,100", 1, 0)]
        [InlineData("80,3,5,10", 1, 0)]
        [InlineData("100,3,5,100,1,2,3,4", 1, 0)]
        public void TestBasicInput(string input, int minimumSolutions, int minClosenessOfBestSolution)
        {
            List<NumbersSolution> solutions = _numbersController.SolveNumbers(input).ToList();
            Assert.NotNull(solutions);
            
            Assert.True(solutions.Count() >= minimumSolutions);

            if (minimumSolutions > 0)
            {
                Assert.True(minClosenessOfBestSolution >= solutions.First().Closeness);
            }

            if (minimumSolutions > 0)
            {
                int numberOfInputs = input.Split(",").Count() - 1;
                CollectionsHelper collectionsHelper = new CollectionsHelper();
                int factorial = collectionsHelper.Factorial(numberOfInputs);
                var justTheStrings = solutions.Select((s) => s.Solution).ToList();
                int uniqueSolutions = justTheStrings.Distinct().Count();
                _output.WriteLine("Statistics: # of inputs {0}, max results {1}, unique results {2}, actual results {3}", numberOfInputs, factorial, uniqueSolutions, solutions.Count());
            }
            
        }

        
    }
}
