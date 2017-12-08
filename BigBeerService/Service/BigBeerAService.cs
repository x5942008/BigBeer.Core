using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBeerServiceSample
{
    public class BigBeerAService : BigBeerBase
    {
        public override IBigBeerResult Do(string value)
        {
           return DoAsync(value).Result;
        }

        public override async Task<IBigBeerResult> DoAsync(string value)
        {
            return  await Task.FromResult(new BigBeerResult() {

            });
        }
    }
}
