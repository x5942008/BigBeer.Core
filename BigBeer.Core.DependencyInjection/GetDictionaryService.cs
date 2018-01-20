using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBeer.Core.DependencyInjection
{
    public class GetDictionaryService : IService
    {
        public string GetData(string name)
        {
            return ServiceHelp.Data[name];
        }
    }
}
