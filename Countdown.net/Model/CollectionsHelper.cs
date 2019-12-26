using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Countdown.net.Model
{
    public class CollectionsHelper
    {
        // Attribution - inspired by https://stackoverflow.com/a/10630026
        public IEnumerable<IEnumerable<T>> Permute<T>(IEnumerable<T> input, int length)
        {
            if (length == 1)
            {
                return input.Select(t => new T[] {t});
            }
            else
            {
                return Permute<T>(input, length - 1)
                    .SelectMany(t => input.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new T[] {t2}));
            }
        }

        public IEnumerable<IEnumerable<T>> Permute<T>(IEnumerable<T> input)
        {
            return Permute(input, input.Count());
        }

        public int Factorial(int input)
        {
            if (input < 1)
            {
                throw new InvalidOperationException("Input must be an integer > 1");
            }
            else if (input == 1)
            {
                return 1;
            }
            else
            {
                return input * Factorial(input - 1);
            }
        }
    }
}
