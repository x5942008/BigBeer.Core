﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RSA.Security
{
    public static class Statikey
    {
        //RSA2密钥 由工具生成

        public static readonly IConfiguration builder = new ConfigurationBuilder().AddJsonFile("key.json").Build();
        /// <summary>
        /// 公钥
        /// </summary>
        public static string PublicKey
        {
            get
            {
                return builder["publicKey"];

                /*"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAscIdjkAPpxnsq3egkhcXQZ+Ph6w+lYSJ3qe3ASThCNMOa65nytlu2S4mpIqgGXU2rO+ZZNSgwOCuLANTm3raFYitt2jjVM2fpuhA48grbKY3IKER1/1Qu/0SFdVZEps47ORPlP6rX2rszwGB4mzosfU3Hlv7D3h59un0SqLjbmYlz25I6myX81os0j8RJA32HJXwYbti27XGeS1rXYPGNM7v+DP9Aa2judWv9NzeS9tm5WYasm23VXuGcko2kMWF2MxXtsTjD5CCK/8B2LilkqRcj7lkty2D7x03jEDJIKaQL71Dhq3kXdcgkYG8/F7qga4XE4RMWTCMK1gzNTSGSQIDAQAB";*/
            }
        }


        /// <summary>
        /// 私钥
        /// </summary>
        public static string PrivateKey
        {
            get
            {
                return builder["privateKey"];

                //"MIIEowIBAAKCAQEAscIdjkAPpxnsq3egkhcXQZ+Ph6w+lYSJ3qe3ASThCNMOa65nytlu2S4mpIqgGXU2rO+ZZNSgwOCuLANTm3raFYitt2jjVM2fpuhA48grbKY3IKER1/1Qu/0SFdVZEps47ORPlP6rX2rszwGB4mzosfU3Hlv7D3h59un0SqLjbmYlz25I6myX81os0j8RJA32HJXwYbti27XGeS1rXYPGNM7v+DP9Aa2judWv9NzeS9tm5WYasm23VXuGcko2kMWF2MxXtsTjD5CCK/8B2LilkqRcj7lkty2D7x03jEDJIKaQL71Dhq3kXdcgkYG8/F7qga4XE4RMWTCMK1gzNTSGSQIDAQABAoIBAHsPsxx29x8tUG7Iy8431C7nQxufQFiMwFH39DcDjBNq4jHkNRD3BMmwLKp/GiVlw2toGN74YS4Gni30Q56BJ5f+3pz6LV/ZVuzbH4lSW9XFIKcjO1I3mfv7UNjysc6yzW8bBIlW4deWE8mf9oaF9Xa2F+mZri7grjclcq11JVSYlGebsA4MTLUOmYEbGpI5E+3988efTNl98V9t/+RW/AEpC2Xn6DyQmYzMrvFBqFOkysOhtJ/ijo1OZYkilZn1LWx+B38dzX7jkIMVIfosdejFFE6VVGb3vWZCnH1+HgRkrakuU62Qe8NbkFJvlSEekXdt/0bo2rM6pgjrTwVXX50CgYEA6m1ai7exYx69W4uPtdQ6MkvhqPmkThGzefyCerxTELRAWqBOoU+/xmVuS0I44F7d4LMuVdRt4JUI8EbEdaupqxhOb43sJNHWqi/iFPpNE+w0pbVGkjX+FLqxFRtl7jT/JctvE9BIl0bN33GTfXg7WxxPNN4FmYfpba1pdSNtSscCgYEAwh3AzFDJQ00aiaGGEsKoNejDki/1m6YIziEvlarFjvVUyRE2VL8iG0wv8QvmRnIH90c14Jt94Z6mbIguSXW350TAiDRfUNwftuTxGFGpw6sgEO4WBbUceVU0Wv/XJqCBRGh9+9NrTYInm0RqLfnItvRkDb77WKhXJzqPjOR8Fm8CgYEA38zCEnytxnkEQa885VPUs0uqBU0+xKE1fJHKZy3/BwVuIpbEOlAOP3N6FjMEZX9rxyaIZ7xDoZHmVKzaxZO6iPLNfsY42PXTP+oypeBHUWvA3ynuU4tkI5oPkJz1dLH3m7dZNcs0Yedgh57ANZpg4BxoqYoEQox0FxbkhMXrguUCgYBDaF2ZFbyuOEos3QBX52zOO6QeUbUydbe9DN0fVgwAlsT4hZeeWjkXzZ8gT4eJkvOdNQdKlfRistsL+UZJkC1qi/9nzPgEdkw1EM+AKGuRXQ1nk6XmGXxnzeS+bPVjnn0FKlwFZOdwJPBoBJvylzjR/4/3DgjuQpbxMm7C+Fz21QKBgBm5zkJVq7eYOCZ48e2Gw+NL3HlJbWDznpSjfQ6IJ9uYis792f5Bh+oYDThGBr77bE+d4N7NB/037L1dvb0RH5smc0afsJmwkdXwyhkPGtucj2cXRcsUpVkXtP5qx7t/14YaHjDFV8s8WEQNzFjLErQVKiIz+LyVB3tKnBeIt7XZ";
            }
        }
    }
}
