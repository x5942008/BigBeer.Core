using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BigBeer.UEditor.NetCore
{
    public static class Config
    {
        public static string WebRootPath { get; set; }
        public static string ConfigFile { get; set; } = "config.json";
        public static bool noCache { get; set; } = true;
        public static JObject BuildItems()
        {
            var json = File.ReadAllText(ConfigFile);
            return JObject.Parse(json);
        }
        public static JObject Items
        {
            get
            {
                if (noCache||_Items == null)
                {
                    _Items = BuildItems();
                }
                return _Items;
            }
        }
        private static JObject _Items;
        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }
        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }
        public static string GetString(string key)
        {
            return GetValue<String>(key);
        }
        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}
