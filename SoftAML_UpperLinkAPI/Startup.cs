using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SoftAML_UpperLinkAPI.Startup))]

namespace SoftAML_UpperLinkAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
            RegisterAuth(app);
        }
    }
}
