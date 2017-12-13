using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
namespace BigBeer.Core.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// 显示枚举名称
        /// Description/Display.Name
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Display(this Enum @this)
        {
            try
            {
                var attr = @this.GetType()
               .GetRuntimeField(@this.ToString())
               .GetCustomAttributes(typeof(DisplayAttribute), false)
               .FirstOrDefault();
                if (attr != null)
                    return (attr as DisplayAttribute).Name;
                return string.Empty;
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }
        /// <summary>
        /// 判断值是否在枚举中
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool In(this Enum @this, params Enum[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }
        /// <summary>
        /// 判断值是否没有在枚举中
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool NotIn(this Enum @this, params Enum[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
        /// <summary>
        /// 将枚举转换为DisplayAttribute/DescriptionAttribute int的字典
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IDictionary<string, int> ToDispayDictionary(this Enum @this)
        {
            Type t = @this.GetType();
            FieldInfo[] info = t.GetRuntimeFields().ToArray();
            var result = new Dictionary<string, int>();
            for (int i = 0; i < info.Length; i++)
            {

                var key = string.Empty;
                var att = info[i].GetCustomAttributes(false);
                foreach (Attribute a in att)
                {
                    if (a is DisplayAttribute)
                    {
                        key = (a as DisplayAttribute).Name;
                    }
                }
                if (!string.IsNullOrEmpty(key))
                    result.Add(key, (int)info[i].GetValue(null));
            }
            return result;
        }
        /// <summary>
        /// 统计枚举数量
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int EnumCount(this Enum @this)
        {
            Type t = @this.GetType();
            FieldInfo[] info = t.GetRuntimeFields().ToArray();
            var num = info.Count() - 1;
            return num;
        }
    }
}
