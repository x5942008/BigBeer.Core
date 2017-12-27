using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace BigBeer.Core.HelperSample
{
    public static class ObjectHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static string JsonSerialize(object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        static T JsonDeserialize<T>(string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);

            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
            /// 序列化
            /// </summary>
            /// <param name="type">类型</param>
            /// <param name="obj">对象</param>
            /// <returns></returns>
        public static string XmlSerializer<T>(object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(typeof(T));
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }
        /// <summary>
            /// 反序列化
            /// </summary>
            /// <param name="type">类型</param>
            /// <param name="xml">XML字符串</param>
            /// <returns></returns>
        public static object XmlDeserialize<T>(string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(typeof(T));
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
            /// 反序列化
            /// </summary>
            /// <param name="type"></param>
            /// <param name="xml"></param>
            /// <returns></returns>
        public static object XmlDeserialize<T>(Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(typeof(T));
            return xmldes.Deserialize(stream);
        }

        /// <summary>
        /// 对象序列化为json
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJson(this object value)
        {
            if (value == null) return string.Empty;
            try
            {
                return JsonSerialize(value);
            }
            catch
            {
                return string.Empty;
            }

        }
        /// <summary>
        /// Json序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T ToObjectJson<T>(this string @this)
        {
            if (string.IsNullOrEmpty(@this)) return default(T);
            return JsonDeserialize<T>(@this);
        }

        /// <summary>
        /// 对象序列化为xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToXml<T>(this object value)
        {
            var result = XmlSerializer<T>(value);
            return result;
        }
        /// <summary>
        /// Xml序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T ToObjectXml<T>(this string @this)
        {
            var result = XmlDeserialize<T>(@this);
            return (T)result;
        }
        /// <summary>
        /// Xml序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T ToObjectXml<T>(this Stream @this)
        {
            var result = XmlDeserialize<T>(@this);
            return (T)result;
        }

        /// <summary>
        /// 对象转换成字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary<T>(this object @this)
        {
            var temp = (T)@this;
            PropertyInfo[] pro = temp.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (var index in pro)
            {
                result.Add(index.Name.ToString(), (string)index.GetValue(temp));
            }
            return result;
        }
    }
}
