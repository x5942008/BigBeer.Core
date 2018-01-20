using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBeer.Core.DependencyInjection
{
    /// <summary>
    /// 字典数据获取接口
    /// </summary>
    public interface IService
    {
        string GetData(string name);
    }
}
