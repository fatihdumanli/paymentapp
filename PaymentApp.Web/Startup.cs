﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaymentApp.Web.Startup))]
namespace PaymentApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
