using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsBindSample.Controllers
{
    public class TestController:Controller
    {


        public JsonResult Dee()
        {
            var m = new[]{
                new S{Year = 2000, Month = 1, Day = 10},
                new S{Year = 2000, Month = 2, Day = 10},
                new S{Year = 2010, Month = 1, Day = 1},
                new S{Year = 2010, Month = 2, Day = 1},
                new S{Year = 2010, Month = 1, Day = 2},
                new S{Year = 2010, Month = 2, Day = 2},
                new S{Year = 2000, Month = 1, Day = 2},
                new S{Year = 2000, Month = 2, Day = 2}
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
            var result = new
            {
                a = q2,
                b = q
            };
            return Json(result);
        }
        /// <summary>
        /// 需要统计的类
        /// </summary>
        public class S
        {
            public int Year;
            public int Month;
            public int Day;
        }
    }
}
