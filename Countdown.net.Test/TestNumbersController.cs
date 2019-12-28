using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Countdown.net.Controllers;
using Countdown.net.Model;
using Xunit;

namespace Countdown.net.Test
{
    public class TestNumbersController
    {
        private readonly NumbersController _numbersController = new NumbersController(TestsHelper.GetApplicationConfiguration());

        [Theory]
        [InlineData("100,3,5,10", 1, 20)]
        [InlineData("100,3,5,100", 1, 0)]
        [InlineData("80,3,5,10", 1, 0)]
        [InlineData("100,3,5,100,1,2,3", 1, 0)]
        public void TestBasicInput(string input, int minimumSolutions, int minClosenessOfBestSolution)
        {
            List<NumbersSolution> solutions = _numbersController.SolveNumbers(input).ToList();
            Assert.NotNull(solutions);
            
            Assert.True(solutions.Count() >= minimumSolutions);

            if (minimumSolutions > 0)
            {
                int closeness = solutions.First().Closeness;
                Assert.True(minClosenessOfBestSolution >= solutions.First().Closeness);
            }
        }

        
    }
}
