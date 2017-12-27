
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBeer.Core.RedisSample
{
    /// <summary>
    /// No-Sql(Redis)
    /// </summary>
    public class RedisHelp:IDisposable
    {
        private static string redisAddress { get; set; }
        private static ConnectionMultiplexer Redis { get; set; }

        /// <summary>
        /// 获取Redis实例
        /// </summary>
        /// <param name="address">Redis地址</param>
        /// <returns></returns>
        public static RedisHelp Defalut(string address)
        {
            var redis = new RedisHelp();
            redisAddress = address;
            redis.Connecting();
            return redis;
        }

        /// <summary>
        /// 链接
        /// </summary>
        /// <returns></returns>
        public bool Connecting()
        {
            try
            {
                if (Redis != null)
                {
                    Redis.Dispose();
                }
                Redis = ConnectionMultiplexer.Connect(redisAddress);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region 准备动作(懒加载)
        //参数可加：abortConnect =false
        //private Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        //{
        //    return ConnectionMultiplexer.Connect($"{path},abortConnect =false");
        //});

        //public ConnectionMultiplexer Connection
        //{
        //    get
        //    {
        //        return lazyConnection.Value;
        //    }
        //}
        #endregion


        /// <summary>
        /// redis读取操作
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            try
            {
                    var db = Redis.GetDatabase();
                    var result = db.StringGet(key);
                    return result;
            }
            catch (Exception e)
            {
                return "false:" + e.Message;
            }
        }

        /// <summary>
        /// redis存储动作
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool StringSet(string key, string value, int? time = null)
        {
            try
            {
                    var db = Redis.GetDatabase();
                    var result = db.StringSet(key, value);
                    var date = DateTime.Now.AddMinutes(5);
                    if (time != null)
                    {
                        date = DateTime.Now.AddMinutes(time.Value);
                    }
                    db.KeyExpire(key, date);
                    return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public void Dispose()
        {
            Redis.Dispose();
            GC.Collect();
        }
    }

    /// <summary>
    /// 使用范例
    /// </summary>
    public class RedisTest
    {
        public void DoSomeThing()
        {
          var redis =  RedisHelp.Defalut("10.0.0.5:999");
            redis.StringSet("1","2");
            redis.StringGet("1");
        }
    }
}