using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fullstack.NET.Controllers;
using Fullstack.NET.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fullstack.NET
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services
                .AddResponseCompression()
                .AddResponseCaching()
                .AddCors();

            services.AddScoped<IStoreQueryService, StoreQueryService>();

            services.AddLogging(loggingConfig =>
            {
                loggingConfig
                    .AddDebug()
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Trace);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .UseDatabaseErrorPage();
            }

            app.UseCors(policy =>
            {
                if (env.IsDevelopment())
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                }
            });

            app
                .UseMvc()
                .UseResponseCaching()
                .UseResponseCompression();
        }
    }
}
