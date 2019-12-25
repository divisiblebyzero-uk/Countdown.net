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
        NumbersController NumbersController = new NumbersController(TestsHelper.GetApplicationConfiguration());

        [Theory]
        [InlineData("100,3,5,10", 1)]
        public void TestBasicInput(string input, int minimumSolutions)
        {
            IEnumerable<NumbersSolution> solutions = NumbersController.SolveNumbers(input);
            //Assert.NotNull(solutions);
            
            //Assert.True(solutions.Count() >= minimumSolutions);
        }
    }
}
