using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BigBeerMiddelwareSample
{
    public class BigBeerMiddlware : MiddlwareBase
    {
        public BigBeerMiddlware(RequestDelegate next) : base(next)
        {
        }

        public override Task Invoke(HttpContext context)
        {
            return _next(context);
        }
    }
}
