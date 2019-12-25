using System;
using System.Linq;
using Countdown.net.Controllers;
using Countdown.net.Model;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Countdown.net.Test
{
    public class TestWordSearchController
    {
       
        WordSearchController controller = new WordSearchController(TestsHelper.GetApplicationConfiguration());
        [Theory]
        [InlineData("BLAH")]
        [InlineData("blah")]
        [InlineData("Blah")]
        public void TestForSingleResponse(string searchString)
        {
            WordSearchResultDto result = controller.SearchForWords(searchString);
            Assert.Single(result.Words);
        }

        [Fact]
        public void TestForNullInput()
        {
            WordSearchResultDto result = controller.SearchForWords(null);
            Assert.Empty(result.Words);
        }
    }

}
