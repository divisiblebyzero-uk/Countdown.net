using System;
using System.Collections.Generic;
using System.Text;

namespace Countdown.net.Model
{
    public class NumbersSolution
    {
        public int Target { get; }
        public int Complexity { get; }
        public int Closeness { get; }
        public string Solution { get; }

        public NumbersSolution(int target, INode node)
        {
            Target = target;
            Complexity = node.CalculateComplexity();
            Closeness = Math.Abs(target - node.CalculateValue());
            Solution = node.ToString();
        }
    }
}
