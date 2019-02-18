using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerSolution
{
    /// <summary>
    /// Config是IdentityServer的配置文件,一会我们需要注册到DI层.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 这个ApiResource参数就是我们Api
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetSoluction()
        {
            return new[]
            {
                new ApiResource("api1","MY API")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId ="",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets={
                        new Secret("secret".Sha256()),
                    },
                    AllowedScopes = { "api1"}
                }
            };
        }
    }
}
