using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Countdown.net.Model
{
    public class CollectionsHelper
    {
        /*// Attribution - inspired by https://stackoverflow.com/a/10630026
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
        }*/

        // Attribution - inspired by https://www.codeproject.com/Articles/43767/A-C-List-Permutation-Iterator
        public IEnumerable<IList<T>> Permute<T>(IList<T> sequence, int count)
        {
            if (count == 1)
            {
                yield return sequence;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    foreach (var perm in Permute(sequence, count - 1))
                    {
                        yield return perm;
                    }

                    RotateRight(sequence, count);
                }
            }
        }

        private void RotateRight<T>(IList<T> sequence, int count)
        {
            T temp = sequence[count - 1];
            sequence.RemoveAt(count - 1);
            sequence.Insert(0, temp);
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
