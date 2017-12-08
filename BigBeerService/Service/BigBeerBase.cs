using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBeerServiceSample
{
    /// <summary>
    /// 通过抽象重写来实现接口
    /// </summary>
    public abstract class BigBeerBase : IBigBeerService
    {
        public abstract IBigBeerResult Do(string value);
        public abstract Task<IBigBeerResult> DoAsync(string value);
    }
    /// <summary>
    /// 自定义返回对象接口具体到类
    /// </summary>
    public class BigBeerResult : IBigBeerResult
    {
        public BigStatus Status { get; set; } = BigStatus.success;
        public (string name, string message, string value, BigType type) Redata { get; set; }
    }
}
