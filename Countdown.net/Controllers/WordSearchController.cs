using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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
        private bool DownloadComplete = false;

        public WordSearchController(CountdownSettings countdownSettings)
        {
            CountdownSettings = countdownSettings;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void CheckDownloaded()
        {
            if (!DownloadComplete)
            {
                System.Diagnostics.Debug.WriteLine("Words have not been downloaded, so doing that now");

                DownloadWords();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private  void DownloadWords()
        {
            System.Diagnostics.Debug.WriteLine("Beginning download");
            WebRequest request = WebRequest.Create(CountdownSettings.WordListUrl);
            WebResponse response = request.GetResponse();

            string line;

            StreamReader reader = new StreamReader(response.GetResponseStream());
            while ((line = reader.ReadLine()) != null)
            {
                string word = line.Trim();
                if (word.Length >= CountdownSettings.MinimumWordSize && word.Length <= CountdownSettings.MaximumWordSize)
                {
                    Words.Add(word, new WordCount(word));

                }

            }

            response.Close();
            DownloadComplete = true;

            System.Diagnostics.Debug.WriteLine("Download complete");
        }

        // GET: api/WordSearch/
        [HttpGet()]
        public IEnumerable<string> GetAllWords()
        {
            CheckDownloaded();
            return Words.Keys;
        }

        private IEnumerable<string> SearchForWords(WordCount wc)
        {
            System.Diagnostics.Debug.WriteLine($"Checking input {wc.TheWord}");
            CheckDownloaded();

            
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
        [HttpGet("{letters}")]
        public WordSearchResultDto SearchForWords(string letters)
        {
            System.Diagnostics.Debug.WriteLine($"Searching for {letters}");
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
