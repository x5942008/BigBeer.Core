using System;
using System.Reflection;
using RSA.Security;
using BigBeer.Core.RedisSample;
using System.Threading;

namespace Reflect.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var redis = new RedisHelp("10.0.0.5:999");
            var a = redis.StringSet("2332", "No-Sql redis速度很快");
            Console.WriteLine(a);
            var b = redis.StringGet("2332");
            Console.WriteLine(b);
            Console.ReadLine();
            //Type t = typeof(RefClass);
            ////BindingFlags.NonPublic | BindingFlags.Instance |
            ////    BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static
            //MemberInfo[] minfos = t.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance |
            //    BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static);
            //foreach (MemberInfo item in minfos)
            //{
            //    Console.WriteLine(item.MemberType);
            //}
            //Console.WriteLine(t.Name+t.Namespace);
            //Console.ReadLine();
            //string[] arrayA = { "a", "b", "c" };
            //int lenth = 5;
            //int[] ints = new int[lenth];
            //for (int i = 0; i < lenth; i++)
            //{
            //    ints[i] = i+1;
            //}
            //foreach (var data in ints)
            //{
            //    Console.WriteLine(data);
            //}
            //Console.ReadLine();
            //var data = "测试";
            //var a = data.EnStr();
            //var b = a.DeStr();
            //var c = data.Sign();
            //var d = c.VerifyStr(data);
            //var e = new Tests("sd");
            //var f = new Tests();
            //var h =e.x;
            //var g = f.x;
            //Console.WriteLine(a);
            //Console.WriteLine(b);
            //Console.WriteLine(c);
            //Console.WriteLine(d);
            //Animal animal = new DogA();
            //Animal animals = new DogB();
            //animal.Jiao();
            //animals.Jiao();
            //CatBase cat = new CatA();
            //CatBase cats = new CatB();
            //cat.Jiao();
            //cats.Jiao();
            //var data = new Yes();
            //var data2 = new Yes("name",2);
            //var data3 = new Yes(3, "name");
            //Yes data4 = new Yes(666);
            //var no = new No();
            //no.stu.Name = "a";
            //Console.WriteLine("请按任意键进行确认");
            //ConsoleKeyInfo info = Console.ReadKey();
            //while (info!=null&&info.Key!= ConsoleKey.Enter)
            //{
            //    switch (info.Key)
            //    {
            //        case ConsoleKey.LeftArrow:
            //            Console.WriteLine("左");
            //            break;
            //        case ConsoleKey.UpArrow:
            //            Console.WriteLine("上");
            //            break;
            //        case ConsoleKey.RightArrow:
            //            Console.WriteLine("右");
            //            break;
            //        case ConsoleKey.DownArrow:
            //            Console.WriteLine("下");
            //            break;
            //        case ConsoleKey.A:
            //            Console.WriteLine("按下了按键A");
            //            break;
            //        case ConsoleKey.B:
            //            Console.WriteLine("按下了按键B");
            //            break;
            //        case ConsoleKey.C:
            //            Console.WriteLine("按下了按键C");
            //            break;
            //        case ConsoleKey.D:
            //            Console.WriteLine("按下了按键D");
            //            break;
            //        default:
            //            break;
            //    }
            //    info = Console.ReadKey();
            //}
            //data.Do();
            //data2.Do();
            //data3.Do();
            //data4.Do();

        }
    }
    public class RefClass
    {
        private int test1;
        private int test2 { get; set; }
        protected int test3 { get; set; }
        public int test4 { get; set; }
        public void Show()
        {

        }
        public static void Show2()
        {

        }
    }

    public class Tests
    {
        public string x;
        public Tests()
        {
            x = "test";
        }
        public Tests(string a)
        {
            x = a;
        }
    }
}

public class No
{
    public string name { get; set; }
    public uint age { get; set; }
    public Stu stu { get; set; }
}
public class Stu
{
    public string Name { get; set; }
}

public class Yes : No
{
    private int test;
    protected string path;
    public Yes(int t)
    {
        test = t;
    }
    public void Do()
    {
        Console.WriteLine(test);
    }
}