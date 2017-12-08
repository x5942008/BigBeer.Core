using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsBindSample
{
    public static class Extensions
    {
        public static IConfiguration GetJsonData(this string @this)
        {
            return new ConfigurationBuilder().AddJsonFile(@this).Build();
        }
    }
}
