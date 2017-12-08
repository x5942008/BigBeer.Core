using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBeerServiceSample
{
    public class BigBeerBService : BigBeerBase
    {
        public override IBigBeerResult Do(string value)
        {
            var data = new BigBeerResult()
            {
                Redata = ("第二个", "正确调用", value, BigType.B)
            };
            return data;
        }

        public override Task<IBigBeerResult> DoAsync(string value)
        {
            throw new NotImplementedException();
        }
    }
}
