using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using Countdown.net.Model;
using Microsoft.AspNetCore.Mvc;

namespace Countdown.net.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WordSearchController : ControllerBase
    {
        private readonly Dictionary<string, WordCount> Words = new Dictionary<string, WordCount>();

        public CountdownSettings CountdownSettings { get; set; }

        public WordSearchController(CountdownSettings countdownSettings)
        {
            CountdownSettings = countdownSettings;

            WebRequest request = WebRequest.Create(CountdownSettings.WordListUrl);

            WebResponse response = request.GetResponse();

            string line;

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                while ((line = reader.ReadLine()) != null)
                {
                    string word = line.Trim();
                    if (word.Length >= CountdownSettings.MinimumWordSize && word.Length <= CountdownSettings.MaximumWordSize)
                    {
                        Words.Add(word, new WordCount(word));

                    }

                }
            }

            response.Close();

            Console.WriteLine("Ready");
        }

        // GET: api/WordSearch/
        [HttpGet()]
        public IEnumerable<string> GetAllWordCounts()
        {
            return Words.Keys;
        }

        private IEnumerable<string> SearchForWords(WordCount wc)
        {
            
            var words = Words;
            List<string> matchList = new List<string>();
            foreach (WordCount wordCount in Words.Values)
            {
                bool matched = true;
                foreach (char d in wordCount.LetterCount.Keys)
                {
                    if (!wc.LetterCount.ContainsKey(d) || wc.LetterCount[d] < wordCount.LetterCount[d])
                    {
                        matched = false;
                        break;
                    }
                }

                if (matched)
                {
                    matchList.Add(wordCount.TheWord);
                }
            }

            return matchList;

        }

        // GET: api/WordSearch/DOOF
        //[HttpGet("{letters}")]
        public WordSearchResultDto SearchForWords(string letters)
        {
            Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            WordSearchResultDto output = new WordSearchResultDto();

            if (letters == null || letters.Length < 4)
            {
                output.Words = Enumerable.Empty<IndividualWordResultDto>();
            }
            else
            {
                output.Words = SearchForWords(new WordCount(letters)).Select(s => new IndividualWordResultDto(s)).OrderByDescending(s => s.Length);
            }

            
            watch.Stop();
            output.TimeTaken = watch.ElapsedMilliseconds;

            return output;
        }
    }
}
