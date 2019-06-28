using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EntityData.Common
{
    public class ConfigUtil
    {
        private static IConfigurationRoot Configuration { get; set; }
        public static string GetConfigValue(string key)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration[key];
        }
    }
}
