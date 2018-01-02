using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.Core.Service.Lib
{
    [Serializable]
    public class RunStatus : MarshalByRefObject, IRunStatus
    {
        public DateTime StartTime { get; set; }
        public AppStatus Status { get; set; }
        public IDictionary<DateTime, string> Error { get; set; } = new Dictionary<DateTime, string>();
        public IDictionary<DateTime, string> Message { get; set; } = new Dictionary<DateTime, string>();
    }
}
