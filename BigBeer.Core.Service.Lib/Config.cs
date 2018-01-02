using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.Core.Service.Lib
{
    public class Config : IConfig
    {
        public string Name { get; set; }
        public string Discription { get; set; }
        public string LogoPath { get; set; }
        public IDictionary<string, string> Dictionary { get; set; } = new Dictionary<string, string>();
    }
}
