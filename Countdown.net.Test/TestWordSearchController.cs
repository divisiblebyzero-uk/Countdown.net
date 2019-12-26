using System;
using System.Collections.Generic;
using System.Linq;
using Countdown.net.Controllers;
using Countdown.net.Model;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Countdown.net.Test
{
    public class TestWordSearchController
    {
       
        WordSearchController Controller = new WordSearchController(TestsHelper.GetApplicationConfiguration());
        [Theory]
        [InlineData("BLAH")]
        [InlineData("blah")]
        [InlineData("Blah")]
        public void TestForSingleResponse(string searchString)
        {
            WordSearchResultDto result = Controller.SearchForWords(searchString);
            Assert.Single(result.Words);
        }

        [Fact]
        public void TestForNullInput()
        {
            WordSearchResultDto result = Controller.SearchForWords(null);
            Assert.Empty(result.Words);
        }

        [Fact]
        public void TestGetAllWords()
        {
            IEnumerable<string> words = Controller.GetAllWords();
            Assert.True(words.Count() > 1000);
        }
    }

}
