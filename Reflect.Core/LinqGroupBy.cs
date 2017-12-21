using System;
using System.Linq;

namespace Test.Core
{
    /// <summary>
    /// 需要统计的类
    /// </summary>
    public class S
    {
        public int Year;
        public int Month;
        public int Day;
    }
    /// <summary>
    /// 统计方法实现
    /// </summary>
    public class LinqDo
    {
        #region 分组统计方法范例
        /// <summary>
        ///  Group分组统计方法
        /// </summary>
        public static void GroupBy()
        {
            var m = new[]{
                new S{Year = 2000, Month = 1, Day = 10},
                new S{Year = 2000, Month = 2, Day = 10},
                new S{Year = 2010, Month = 1, Day = 1},
                new S{Year = 2010, Month = 2, Day = 1},
                new S{Year = 2010, Month = 1, Day = 2},
                new S{Year = 2010, Month = 2, Day = 2},
                new S{Year = 2000, Month = 1, Day = 2},
                new S{Year = 2000, Month = 2, Day = 2},
            };

            var q2 = from s in m
                     group s by s.Year into YearGroup
                     select new
                     {
                         Year = YearGroup.Key,
                         MonthGroups = from s2 in YearGroup
                                       group s2 by s2.Month into MonthGroup
                                       select new
                                       {
                                           Month = MonthGroup.Key,
                                           Days = from s3 in MonthGroup
                                                  orderby s3.Day
                                                  select s3.Day
                                       }
                     };
            var q = m.GroupBy(
                    s => s.Year,
                    (Year, YearGroup) => new
                    {
                        Year,
                        MonthGroups = YearGroup.GroupBy(
                                s2 => s2.Month,
                                (Month, MonthGroup) => new
                                {
                                    Month,
                                    Days = MonthGroup.OrderBy(s3 => s3.Day).Select(s3 => s3.Day)
                                }
                            )
                    }
                );
            foreach (var elem in q)
            //foreach (var elem in q2) 
            {
                Console.WriteLine("Year = {0}", elem.Year);
                foreach (var elem2 in elem.MonthGroups)
                {
                    Console.WriteLine("\tMonth = {0}", elem2.Month);
                    foreach (var day in elem2.Days)
                        Console.WriteLine("\t\tDay = {0}", day);
                }
            }
        }
        #endregion
    }
}