using System;
using System.Collections.Generic;
using System.Text;

namespace Countdown.net.Model
{
    public class NumbersSolution : IEquatable<NumbersSolution>
    {
        public int Target { get; }
        public int Actual { get; }
        public int Complexity { get; }
        public int Closeness { get; }
        public string Solution { get; }

        public NumbersSolution(int target, INode node)
        {
            Target = target;
            Actual = node.Value;
            Complexity = node.Complexity;
            Closeness = Math.Abs(Target - Actual);
            Solution = node.ToString();
        }

        public bool Equals(NumbersSolution other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Target == other.Target && Actual == other.Actual && Complexity == other.Complexity && Closeness == other.Closeness && Solution == other.Solution;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NumbersSolution) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Target, Actual, Complexity, Closeness, Solution);
        }
    }
}
