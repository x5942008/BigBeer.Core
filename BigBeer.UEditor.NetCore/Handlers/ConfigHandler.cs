using Microsoft.AspNetCore.Http;

namespace BigBeer.UEditor.NetCore.Handlers
{
    public class ConfigHandler:Handler
    {
        public ConfigHandler(HttpContext context) : base(context){}
        public override void Process()
        {
            WriteJson(Config.Items);
        }
    }
}