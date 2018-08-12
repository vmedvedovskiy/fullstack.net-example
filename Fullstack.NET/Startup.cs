using Fullstack.NET.Database;
using Fullstack.NET.Database.Authentication;
using Fullstack.NET.Services.Address;
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
    // TODO: Logging
    // Automapper
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services
                .AddResponseCompression()
                .AddResponseCaching()
                .AddCors()
                .Configure<TokenOptions>(this.Configuration.GetSection("Key"));

            const int TransactionsAreNotSupportedByInMemoryCtxWarningId = 30000;

            services
                .AddDbContext<StoreDbContext>(
                    _ => _
                        .UseInMemoryDatabase(nameof(StoreDbContext))
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                        .ConfigureWarnings(w => w
                            .Ignore(new EventId(TransactionsAreNotSupportedByInMemoryCtxWarningId))))
                .AddDbContext<AuthenticationDbContext>(
                    _ => _
                        .UseInMemoryDatabase(nameof(AuthenticationDbContext))
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                        .ConfigureWarnings(w => w
                            .Ignore(new EventId(TransactionsAreNotSupportedByInMemoryCtxWarningId))));

            services
                .AddScoped<IProductsQueryService, ProductsQueryService>()
                .AddScoped<IOrdersQueryService, OrdersQueryService>()
                .AddScoped<IOrdersCommandService, OrdersCommandService>()
                .AddScoped<IOrderOpsService, OrderOpsService>()
                .AddScoped<IAddressCommandService, AddressCommandService>()
                .AddScoped<IUsersQueryService, UsersQueryService>()
                .AddScoped<ITokenProvider, TokenProvider>();
            
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
