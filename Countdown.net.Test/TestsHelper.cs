using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Countdown.net.Test
{
    public class TestsHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }

        public static CountdownSettings GetApplicationConfiguration()
        {
            return GetIConfigurationRoot().GetSection("CountdownSettings").Get<CountdownSettings>();
        }
    }
}
