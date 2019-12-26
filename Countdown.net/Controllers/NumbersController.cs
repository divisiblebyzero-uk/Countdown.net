using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using Countdown.net.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Countdown.net.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        private readonly Dictionary<string, WordCount> Words = new Dictionary<string, WordCount>();

        public CountdownSettings CountdownSettings { get; set; }

        public NumbersController(CountdownSettings countdownSettings)
        {
            CountdownSettings = countdownSettings;

            Console.WriteLine("Ready");
        }

        [HttpGet("{numbersString}")]
        public IEnumerable<NumbersSolution> SolveNumbers(string numbersString)
        {
            List<int> allNumbers = numbersString.Split(",").Select(Int32.Parse).ToList();
            int target = allNumbers.First();
            IEnumerable<int> numbers = allNumbers.Skip(1);
            return GetSolutions(target, numbers).Take(10);
        }

        private IEnumerable<NumbersSolution> GetSolutions(int target, IEnumerable<int> numbers)
        {
            List<INode> nodes = new List<INode>();
            CollectionsHelper collectionsHelper = new CollectionsHelper();
            IEnumerable<IEnumerable<int>> permutations = collectionsHelper.Permute(numbers);

            foreach (IEnumerable<int> permutation in permutations)
            {
                nodes.AddRange(GetNodes(permutation.ToList()));
            }

            IEnumerable<NumbersSolution> solutions = nodes.Select(n => new NumbersSolution(target, n)).OrderBy(solution => solution.Closeness);
            return solutions;
        }

        private List<INode> GetNodes(List<int> numbers)
        {
            if (numbers.Count == 1)
            {
                return new List<INode>() {new IntegerNode(numbers.First())};
            }
            else
            {
                List<INode> results = new List<INode>();
                int first = numbers.First();
                INode left = new IntegerNode(first);
                List<int> theRest = numbers.Skip(1).ToList();
                List<INode> backNodes = GetNodes(theRest);
                foreach (INode node in backNodes)
                {
                    foreach (Operator o in Enum.GetValues(typeof(Operator)))
                    {
                        INode n = ConditionallyCreateCalculationNode(left, node, o);
                        if (n != null)
                        {
                            results.Add(n);
                        }
                    }
                }
                return results;
            }
        }

        // Optimisation (to avoid stupid sums like 10 + (10*10/10/10) & skip non-integer results
        private INode ConditionallyCreateCalculationNode(INode left, INode right, Operator o)
        {
            switch (o)
            {
                case Operator.IgnoreLeft:
                    return right;
                case Operator.IgnoreRight:
                    return left;
                case Operator.Times:
                    if (left.CalculateValue() == 1)
                    {
                        return right;
                    }

                    if (right.CalculateValue() == 1)
                    {
                        return left;
                    }

                    return CreateCalculationNode(left, right, o);
                case Operator.Divide:
                    if (right.CalculateValue() == 1)
                    {
                        return left;
                    }

                    return CreateCalculationNode(left, right, o);
                default:
                    return CreateCalculationNode(left, right, o);
            }
        }

        private INode CreateCalculationNode(INode left, INode right, Operator o)
        {
            CalculationNode node = new CalculationNode(left, right, o);
            if (node.GivesIntegerResult)
            {
                return node;
            }
            else
            {
                return null;
            }
        }
    }
}
