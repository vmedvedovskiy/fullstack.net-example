using Fullstack.NET.Database;
using Fullstack.NET.Database.Authentication;
using Fullstack.NET.Services.Authentication;
using Fullstack.NET.Services.Authentication.Tokens;
using Fullstack.NET.Services.Orders;
using Fullstack.NET.Services.Products;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fullstack.NET
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services
                .AddResponseCompression()
                .AddResponseCaching()
                .AddCors()
                .Configure<TokenOptions>(this.Configuration.GetSection("Key"));

            services.AddDbContext<StoreDbContext>(
                _ => _
                    .UseInMemoryDatabase(nameof(StoreDbContext))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddDbContext<AuthenticationDbContext>(
                _ => _
                    .UseInMemoryDatabase(nameof(AuthenticationDbContext))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddScoped<IProductsQueryService, ProductsQueryService>();
            services.AddScoped<IOrdersQueryService, OrdersQueryService>();
            services.AddScoped<IUsersQueryService, UsersQueryService>();

            services.AddScoped<ITokenProvider, TokenProvider>();

            services.AddLogging(loggingConfig =>
            {
                loggingConfig
                    .AddDebug()
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Trace);
            });
        }
        
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
