using System;
using System.Threading.Tasks;

namespace BigBeerServiceSample
{
    /// <summary>
    /// 通过键值来默认实例化
    /// </summary>
    public class BigbeerFunctionOption : IBigBeerService
    {
        public BigBeerHelp Help { get; set; }

        public BigbeerFunctionOption(BigBeerHelp help)
        {
            Help = help;
        }

        public IBigBeerResult Do(string value)
        {
            var typename = Help.FunctionHelp[value];
            IBigBeerService service = (IBigBeerService)Activator.CreateInstance(typename);
            return service.Do(value);
        }

        public Task<IBigBeerResult> DoAsync(string value)
        {
            throw new NotImplementedException();
        }
    }
}
