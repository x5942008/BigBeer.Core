using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsBindSample
{
    public class Class
    {
        public int ClassNo { get; set; }
        public string ClassDesc { get; set; }
        public List<Students> Students { get; set; }
    }

    public class Students
    {
        public string Name { get; set; }
        public string Age { get; set; }
    }
}
