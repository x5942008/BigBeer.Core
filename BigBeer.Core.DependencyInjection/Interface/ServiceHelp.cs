using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBeer.Core.DependencyInjection
{
    /// <summary>
    /// 字典类
    /// 添加数据并使用data获取数据
    /// </summary>
    public class ServiceHelp
    {
        public  static IDictionary<string, string> Data { get; set; } = new Dictionary<string, string>();

        public ServiceHelp Add(string key, string value) {
            if (Data.ContainsKey(key)) return this;
            Data.Add(key, value);
            return this;
        }
    }
}
