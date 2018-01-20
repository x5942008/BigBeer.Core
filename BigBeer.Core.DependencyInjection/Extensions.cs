using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBeer.Core.DependencyInjection
{
    public static class Extensions
    {
        /// <summary>
        /// 依赖注入扩展
        /// </summary>
        /// <param name="service"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddDictionary(this IServiceCollection service,Action<ServiceHelp>config)
        {
            var help = Activator.CreateInstance(typeof(ServiceHelp));
            config.Invoke((ServiceHelp)help);
            service.AddSingleton(typeof(ServiceHelp));
            return service;
        }
    }
}
