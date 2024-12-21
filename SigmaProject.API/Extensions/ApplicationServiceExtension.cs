using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SigmaProject.API.Options;
using SigmaProject.Data;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SigmaProject.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteConvention());
            });

            services.AddHttpContextAccessor();

            services.AddEndpointsApiExplorer();

            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });

            services.ConfigureOptions<DatabaseOptionSetup>();

            services.AddDbContext<DataContext>((serviceProvider, options) =>
            {
                var databaseOptions = serviceProvider.GetService<IOptionsMonitor<DatabaseOption>>().CurrentValue;

                options.UseSqlServer(databaseOptions.ConnectionString, options =>
                {
                    options.EnableRetryOnFailure(databaseOptions.EnableRetryOnFailure);
                    options.CommandTimeout(databaseOptions.CommandTimeout);
                });
                options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1,0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddApiVersioning().AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOptions>();

            services.AddSwaggerGen();

            return services;
        }
    }
}