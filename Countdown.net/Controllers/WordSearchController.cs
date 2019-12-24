﻿using System;
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

        private readonly string wordListUrl = "https://raw.githubusercontent.com/dwyl/english-words/master/words_alpha.txt";


        public WordSearchController()
        {
            WebRequest request = WebRequest.Create(wordListUrl);

            WebResponse response = request.GetResponse();

            string line;

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                while ((line = reader.ReadLine()) != null)
                {
                    string word = line.Trim();
                    if (word.Length > 3 && word.Length < 10)
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


        // GET: api/WordSearch/DOOF
        [HttpGet("{letters}")]
        public WordSearchResultDto SearchFordWords(string letters)
        {
            Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            WordCount wc = new WordCount(letters);
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

            WordSearchResultDto output = new WordSearchResultDto();
            output.Words = matchList.Select(s => new IndividualWordResultDto(s)).OrderByDescending(s => s.Length);

            watch.Stop();
            output.TimeTaken = watch.ElapsedMilliseconds;

            return output;
        }
    }
}
