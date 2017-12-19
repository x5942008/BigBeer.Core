using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBeer.Core.PayNotifys
{
    public static class Statikey
    {
        public static IConfigurationRoot ConfigRoot { get; private set; }
        public static IConfigurationRoot Config
        {
            get
            {
                if (ConfigRoot == null)
                {
                    ConfigRoot = new ConfigurationBuilder()
           .AddJsonFile("Appsettings.json").Build();
                    return ConfigRoot;
                }
                return ConfigRoot;
            }
        }
        public static string LogPath
        {
            get
            {
                return Config["LogPath"];
            }
        }
    }
}
