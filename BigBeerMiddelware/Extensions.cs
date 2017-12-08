using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BigBeerMiddelwareSample
{
    public static class Extensions
    {
        //public static IApplicationBuilder UseBigbeerMiddlware(this IApplicationBuilder app,Type middlware,params object[] args)
        //{
        //    var applicationServices = app.ApplicationServices;
        //    return app.Use(next =>
        //    {
        //        var methods = middlware.GetMethods(BindingFlags.Instance|BindingFlags.Public);
        //        string InvokeMethodName = null;
        //        var invokeMethods = methods.Where(m =>string.Equals(m.Name, InvokeMethodName,StringComparison.Ordinal)).ToArray();
        //        if (invokeMethods.Lent)
        //        {

        //        }
        //    });
        //}
    }
}
