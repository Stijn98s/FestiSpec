using FestiMS.Util;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;
using System.Web.Http;
using System.Web.Http.Cors;
﻿using System.Configuration;

namespace FestiMS
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute("custom", ".auth/login/custom", new { controller = "Auth" });

            new MobileAppConfiguration()
                .AddMobileAppHomeController()
                .MapApiControllers()
                .AddTablesWithEntityFramework()
                .ApplyTo(config);


            // Use Entity Framework Code First to create database tables based on your DbContext
            var settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    
                    SigningKey = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==",
                    ValidAudiences = new[] { "https://festims.azurewebsites.net/", "http://localhost:60423/", "http://localhost:63511/", "https://festiapi.azurewebsites.net/" },
                    ValidIssuers = new[] { "https://festims.azurewebsites.net/", "http://localhost:60423/", "http://localhost:63511/", "https://festiapi.azurewebsites.net/" },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            config.DependencyResolver = DiFactory.Create(config);
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            app.UseWebApi(config);
        }

    }
}