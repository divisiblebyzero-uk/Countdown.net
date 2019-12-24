using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countdown.net.Model
{
    public class IndividualWordResultDto
    {
        public string Word { get; set; }
        public int Length { get; set; }

        public IndividualWordResultDto(string word)
        {
            Word = word;
            Length = word.Length;
        }
    }

    public class WordSearchResultDto
    {
        public IEnumerable<IndividualWordResultDto> Words { get; set; }
        public long TimeTaken { get; set; }

    }
}
