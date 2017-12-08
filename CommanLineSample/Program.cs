using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CommanLineSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //var settings = new Dictionary<string, string>
            //{
            //    { "name", "bigbeer" },
            //    { "age", "13" }
            //};

            var build = new ConfigurationBuilder()
                //.AddInMemoryCollection(settings)
                .AddJsonFile("Class.json")
                .AddCommandLine(args)
                .Build();

            //Console.WriteLine(config["name"]);
            //Console.WriteLine(config["age"]);
            Console.WriteLine(build["ClassDesc"]);
            Console.WriteLine(build["Students:0:Name"]);
            Console.WriteLine(build["Students:0:Age"]);
            Console.WriteLine(build["age"]);
            Console.ReadLine();
        }
    }
}
