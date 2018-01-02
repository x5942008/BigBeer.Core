using BigBeer.Core.Service.Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.Core.Service
{
    /// <summary>
    /// 默认上下文对象
    /// </summary>
    [Serializable]
    public class DefaultContext : MarshalByRefObject, IContext
    {
        /// <summary>
        /// 上下文
        /// </summary>
        public IDictionary<string, string> Context { get; set; } = new Dictionary<string, string>();


        public string AppDomanName { get; set; }

        public string ComputerName { get; set; } = Environment.MachineName;
    }
}
