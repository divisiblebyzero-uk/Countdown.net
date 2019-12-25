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
            return null;
        }
    }
}
