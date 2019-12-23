using System;
using System.Collections.Generic;
using System.Linq;

namespace Countdown.net.Model
{
    public class WordCount
    {
        public string TheWord { get; set; }
        public int Length { get; set; }
        public Dictionary<char, int> LetterCount { get; set; }

        public WordCount(String theWord)
        {
            TheWord = theWord.ToLower();
            Length = TheWord.Length;
            LetterCount = new Dictionary<char, int>();
            var charList = TheWord.GroupBy(x => x).OrderByDescending(x => x.Count());
            foreach (var letter in charList)
            {
                LetterCount.Add(letter.Key, letter.Count());
            }
        }
    }
}
