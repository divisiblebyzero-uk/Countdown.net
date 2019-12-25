using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Countdown.net.Test
{
    public class TestCountdownSettings
    {
        private CountdownSettings CountdownSettings = TestsHelper.GetApplicationConfiguration();

        [Fact]
        public void TestMaximumWordSize()
        {
            Assert.True(10 > CountdownSettings.MinimumWordSize);
            Assert.True(10 > CountdownSettings.MaximumWordSize);
            Assert.NotNull(CountdownSettings.WordListUrl);
            Assert.NotEmpty(CountdownSettings.WordListUrl);
        }
    }
}
