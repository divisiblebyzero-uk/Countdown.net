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
        private readonly CollectionsHelper Helper = new CollectionsHelper();

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 6)]
        [InlineData(new int[] { 1, 1, 1 }, 6)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, 720)]

        public void TestPermute(IEnumerable<int> input, int expectedCount)
        {
//            IEnumerable<int> input = new List<int>() {1,2,3};
            List<int> numbersList = input.ToList();
            IEnumerable<IList<int>> output = Helper.Permute(numbersList, numbersList.Count);

            Assert.True(output.Count() == expectedCount);


        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 6)]
        [InlineData(6, 720)]
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
