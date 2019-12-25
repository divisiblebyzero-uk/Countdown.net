using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Countdown.net.Model
{
    public interface INode
    {
        int CalculateValue();
        int CalculateComplexity();
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
        }

        public int Value { get; set; }
        public int CalculateValue()
        {
            return Value;
        }

        public int CalculateComplexity()
        {
            return 1;
        }
    }

    public class CalculationNode : INode
    {
        public INode Left { get; set; }
        public INode Right { get; set; }
        public Operator Operator { get; set; }

        public bool GivesIntegerResult { get; set; }

        public CalculationNode(INode left, INode right, Operator o)
        {
            Left = left;
            Right = right;
            Operator = o;
            GivesIntegerResult = Operator != Operator.Divide || (Left.CalculateValue() % Right.CalculateValue() == 0);
        }

        public int CalculateValue()
        {
            switch (Operator)
            {
                case Operator.Plus:
                    return Left.CalculateValue() + Right.CalculateValue();
                case Operator.Minus:
                    return Left.CalculateValue() - Right.CalculateValue();
                case Operator.Times:
                    return Left.CalculateValue() * Right.CalculateValue();
                case Operator.Divide:
                    if (GivesIntegerResult)
                    {
                        return Left.CalculateValue() / Right.CalculateValue();
                    }
                    else
                    {
                        throw new InvalidOperationException("Division gives non-integer result");
                    }
                case Operator.IgnoreLeft:
                    return Right.CalculateValue();
                case Operator.IgnoreRight:
                    return Left.CalculateValue();
                default:
                    throw new NotImplementedException("Operator not implemented");
            }
            throw new NotImplementedException();
        }

        public int CalculateComplexity()
        {
            return 1 + Left.CalculateComplexity() + Right.CalculateComplexity();
        }
    }
}
