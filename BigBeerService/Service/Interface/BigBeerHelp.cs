using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeerServiceSample
{
    public class BigBeerHelp
    {
        public IDictionary<string, Type> FunctionHelp { get; set; } = new Dictionary<string, Type>();

        public BigBeerHelp Use(string value, Type type)
        {
            if (!FunctionHelp.ContainsKey(value)) return this;
            FunctionHelp.Add(value, type);
            return this;
        }

        public BigBeerHelp Use<T>(string value) where T : IBigBeerService
        {
            if (FunctionHelp.ContainsKey(value)) return this;
            FunctionHelp.Add(value, typeof(T));
            return this;
        }

    }
}
