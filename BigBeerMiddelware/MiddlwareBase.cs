using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BigBeerMiddelwareSample
{
   public abstract class MiddlwareBase
    {
        protected RequestDelegate _next;
        public MiddlwareBase(RequestDelegate next)
        {
            _next = next;
        }
        public abstract Task Invoke(HttpContext context);
    }
}
