using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Countdown.net.Model;
using Xunit;

namespace Countdown.net.Test
{
    public class TestCollectionsHelpers
    {
        private CollectionsHelper Helper = new CollectionsHelper();

        [Fact]
        public void TestPermute()
        {
            IEnumerable<int> input = new List<int>() {1,2,3};
            IEnumerable<IEnumerable<int>> output = Helper.Permute(input, 3);

            Assert.True(output.Count() == 6);

            IEnumerable<IEnumerable<int>> distinctOutput = output.Distinct();

            Assert.True(output.Count() == distinctOutput.Count());

        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 6)]
        [InlineData(10, 3628800)]
        public void TestFactorial(int input, int output)
        {
            int actualOutput = Helper.Factorial(input);
            Assert.Equal(output, actualOutput);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void TestDuffInputToFactorial(int input)
        {
            Assert.Throws<InvalidOperationException>(() => Helper.Factorial(input));
        }
    }
}
