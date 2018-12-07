using System;
using Microsoft.Extensions.Configuration;

namespace Test.Core
{
    class Program
    {

        static void Main(string[] args)
        {
            var build = new ConfigurationBuilder()
                .AddJsonFile("config.json", false, true)
                .Build();
            Console.WriteLine(build["test"]);
            Console.ReadLine();
            //var redis = RedisHelp.Defalut("10.0.0.5:999");
            //Console.WriteLine(redis.StringSet("1", "2"));
            //Console.WriteLine(redis.StringGet("1"));
            //Console.Read();
            //LinqDo.GroupBy();
            //Console.ReadLine();

        }
    }
}