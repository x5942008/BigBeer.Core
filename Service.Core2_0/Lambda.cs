using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Core2_0
{
   public class Class
    {
        public static string Data(string a) => a + ":lambda";
        public static string Data1(string a, int b) => a + b.ToString() + ":lambda";
    }
}
