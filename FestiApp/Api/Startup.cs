using System;
using System.Text;
using System.Threading.Tasks;
using FestiAPI.Persistence;
using FestiDB.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag.AspNetCore;


namespace FestiAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
             Console.Write(arg);
            return  Task.CompletedTask;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(_ => new ApiContext(Configuration.GetConnectionString("FestiConnection")));
            
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ=="));
            
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.IncludeErrorDetails = true;
                    x.RequireHttpsMetadata = false;
                    x.Events = new JwtBearerEvents { OnAuthenticationFailed = AuthenticationFailed };
                  x.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Todo change to secure
                        ValidateLifetime = false,
                        ValidateActor = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                       
                        
                    };
                });
            services.AddOpenApiDocument(el => el.DocumentName = "openAPI");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
                app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
            {
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger((SwaggerDocumentMiddlewareSettings el )=> {});
            app.UseSwaggerUi3();
                
            app.UseCors(builder =>
            {
                //todo swith
//                builder.WithOrigins("https://festispa.z6.web.core.windows.net").AllowAnyHeader().AllowAnyMethod();
                builder.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod();
            });
            
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
            
        }
    }
}
