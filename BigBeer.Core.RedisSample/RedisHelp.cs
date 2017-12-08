
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
    public class RedisHelp
    {

        private static string redisAddress { get; set; }

        public RedisHelp(string RedisAddress)
        {
            redisAddress = RedisAddress;
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
                using (var redis= ConnectionMultiplexer.Connect(redisAddress))
                {
                    var db = redis.GetDatabase();
                    var result = db.StringGet(key);
                    return result;
                }
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
                using (var redis = ConnectionMultiplexer.Connect(redisAddress))
                {
                    var db = redis.GetDatabase();
                    var result = db.StringSet(key, value);
                    var date = DateTime.Now.AddMinutes(5);
                    if (time != null)
                    {
                        date = DateTime.Now.AddMinutes(time.Value);
                    }
                    db.KeyExpire(key, date);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}