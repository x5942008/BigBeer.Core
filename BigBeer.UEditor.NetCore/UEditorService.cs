﻿using BigBeer.UEditor.NetCore.Handlers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.UEditor.NetCore
{
    public class UEditorService
    {
        private UEditorActionCollection actionList;

        public UEditorService(IHostingEnvironment env, UEditorActionCollection actions)
        {
            Config.WebRootPath = env.WebRootPath;
            actionList = actions;
        }

        public void DoAction(HttpContext context)
        {
            var action = context.Request.Query["action"];
            if (actionList.ContainsKey(action))
                actionList[action].Invoke(context);
            else
                new NotSupportedHandler(context).Process();
        }
    }
}
