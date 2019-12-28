using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Countdown.net.Model;
using Xunit;

namespace Countdown.net.Test
{
    public class TestNodes
    {
        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(74)]
        public void TestIntegerNodes(int number)
        {
            INode node = new IntegerNode(number);
            Assert.Equal(1, node.Complexity);
            Assert.Equal(number, node.Value);
        }

        private INode GetIntegerNode(int number)
        {
            return new IntegerNode(number);
        }

        [Theory]
        [InlineData(10, 5, Operator.Plus, 15)]
        [InlineData(10, 5, Operator.Minus, 5)]
        [InlineData(10, 5, Operator.Times, 50)]
        [InlineData(10, 5, Operator.Divide, 2)]
        [InlineData(10, 5, Operator.IgnoreLeft, 5)]
        [InlineData(10, 5, Operator.IgnoreRight, 10)]
        public void TestCalculationNodes(int leftNumber, int rightNumber, Operator o, int value)
        {
            int i = 10 / 5;
            INode node = new CalculationNode(GetIntegerNode(leftNumber), GetIntegerNode(rightNumber), o);
            Assert.Equal(3, node.Complexity);
            Assert.Equal(value, node.Value);
        }

        [Theory]
        [InlineData(3,2,true)]
        [InlineData(3,3,false)]
        public void TestNonIntegerDivision(int left, int right, bool shouldThrowException)
        {
            INode node = new CalculationNode(GetIntegerNode(left), GetIntegerNode(right), Operator.Divide);
            Assert.Equal(3, node.Complexity);
            if (shouldThrowException)
            {
                Assert.Equal(-1, node.Value);
            }
            else
            {
                Assert.NotEqual(-1, node.Value);
            }
            
        }

        [Fact]
        public void TestZeros()
        {
            int leftNumber = 10;
            int rightNumber = 0;
            CalculationNode calculationNode = new CalculationNode(GetIntegerNode(leftNumber), GetIntegerNode(rightNumber), Operator.Divide);
            Assert.False(calculationNode.GivesIntegerResult);

        }
    }
}
