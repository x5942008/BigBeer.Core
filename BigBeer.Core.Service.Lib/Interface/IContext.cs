using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.Core.Service.Lib
{
    /// <summary>
    /// 传入上下文对象
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// 上下文
        /// </summary>
        IDictionary<string, string> Context { get; set; }
        /// <summary>
        /// 当前服务器名称
        /// </summary>
        string ComputerName { get; set; }
        /// <summary>
        /// 父级appdomain名称
        /// </summary>
        string AppDomanName { get; set; }
    }
}
