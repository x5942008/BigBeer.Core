using Microsoft.Extensions.DependencyInjection;
using System;

namespace BigBeerServiceSample
{
    public static class Extensions
    {
        /// <summary>
        /// 服务注入扩展
        /// </summary>
        /// <param name="service"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddBigBeerService(this IServiceCollection service,Action<BigBeerHelp>config)
        {
            BigBeerHelp help = new BigBeerHelp();
            config.Invoke(help);
            service.AddSingleton(help.GetType(),help);
            service.AddTransient(typeof(BigbeerFunctionOption));
            return service;
            
        }
    }
}
