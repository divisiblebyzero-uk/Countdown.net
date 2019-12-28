using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Countdown.net.Model
{
    public interface INode
    {
        int Complexity { get;  }
        int Value { get; } 
    }

    public enum Operator
    {
        Plus,
        Minus,
        Times,
        Divide,
        IgnoreLeft,
        IgnoreRight
    }

    public class IntegerNode : INode
    {
        public IntegerNode(int value)
        {
            Value = value;
            Complexity = 1;
        }

        public int Value { get; }
        public int Complexity { get; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class CalculationNode : INode
    {
        public INode Left { get; set; }
        public INode Right { get; set; }
        public Operator Operator { get; set; }
        public int Value { get; }
        public int Complexity { get; }

        public bool GivesIntegerResult { get; set; }

        public CalculationNode(INode left, INode right, Operator o)
        {
            if ((o == Operator.Plus || o == Operator.Times) && left.Value <= right.Value)
            {
                Left = right;
                Right = left;
                Operator = o;
            }
            else
            {
                Left = left;
                Right = right;
                Operator = o;
            }

            GivesIntegerResult = Operator != Operator.Divide || Right.Value != 0 && (Left.Value % Right.Value == 0);
            if (GivesIntegerResult)
            {
                Value = CalculateValue();
            }
            else
            {
                Value = -1;
            }

            Complexity = 1 + left.Complexity + right.Complexity;

        }

        private int CalculateValue()
        {
            switch (Operator)
            {
                case Operator.Plus:
                    return Left.Value + Right.Value;
                case Operator.Minus:
                    return Left.Value - Right.Value;
                case Operator.Times:
                    return Left.Value * Right.Value;
                case Operator.Divide:
                    if (GivesIntegerResult)
                    {
                        return Left.Value / Right.Value;
                    }
                    else
                    {
                        throw new InvalidOperationException("Division gives non-integer result");
                    }
                case Operator.IgnoreLeft:
                    return Right.Value;
                case Operator.IgnoreRight:
                    return Left.Value;
                default:
                    throw new NotImplementedException("Operator not implemented");
            }
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "(" + Left.ToString() + " " + Operator.ToString() +" " + Right.ToString() + ")";
        }
    }
}
