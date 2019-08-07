using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FestiAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
