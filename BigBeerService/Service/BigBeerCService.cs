using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBeerServiceSample
{
    public class BigBeerCService : BigBeerBase
    {
        public override IBigBeerResult Do(string value)
        {
            var data = new BigBeerResult()
            {
                Redata = ("第三个", "正确调用", value, BigType.C)
            };
            return data;
        }

        public override Task<IBigBeerResult> DoAsync(string value)
        {
            throw new NotImplementedException();
        }
    }

}
