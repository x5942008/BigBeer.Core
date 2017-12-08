using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBeerServiceSample
{
    public interface IBigBeerService
    {
        IBigBeerResult Do(string value);
        Task<IBigBeerResult> DoAsync(string value);
    }

    public interface IBigBeerResult
    {
        BigStatus Status { get; set; }
        (string name, string message, string value, BigType type) Redata { get; set; }
    }

    public enum BigStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        success,
        /// <summary>
        /// 失败
        /// </summary>
        faild
    }

    public enum BigType
    {
        /// <summary>
        /// 第一个
        /// </summary>
        A,
        /// <summary>
        /// 第二个
        /// </summary>
        B,
        /// <summary>
        /// 第三个
        /// </summary>
        C
    }
}
