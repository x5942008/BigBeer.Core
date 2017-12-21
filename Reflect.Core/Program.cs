using System;
using System.Reflection;
using RSA.Security;
using BigBeer.Core.RedisSample;
using System.Threading;
using BigBeer.Core.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Test.Core
{
    /// <summary>
    /// LINQ分组嵌套统计案例
    /// </summary>
    class Program
    {

        static void Main(string[] args)
        {
            LinqDo.GroupBy();
            Console.ReadLine();
        }
    }
}