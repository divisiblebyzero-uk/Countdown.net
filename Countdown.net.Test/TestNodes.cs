using System;
using System.Collections.Generic;
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
            Assert.Equal(1, node.CalculateComplexity());
            Assert.Equal(number, node.CalculateValue());
        }

        private INode GetIntegerNode(int number)
        {
            return new IntegerNode(number);
        }

        [Theory]
        [InlineData(10,5,Operator.Plus,15)]
        public void TestCalculationNodes(int leftNumber, int rightNumber, Operator o, int value)
        {

            INode node = new CalculationNode(GetIntegerNode(leftNumber), GetIntegerNode(rightNumber), o);
            Assert.Equal(3, node.CalculateComplexity());
            Assert.Equal(value, node.CalculateValue());
        }

        [Fact]
        public void TestNonIntegerDivision()
        {
            INode node = new CalculationNode(GetIntegerNode(3), GetIntegerNode(2), Operator.Divide);
            Assert.Equal(3, node.CalculateComplexity()); 
            Assert.Throws<InvalidOperationException>(() => node.CalculateValue());
        }
    }
}
