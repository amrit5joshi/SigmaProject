using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SigmaProject.API.Options;
using SigmaProject.Data;
using SigmaProject.Models.Common;
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

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorMessage = "Invalid Request Body";
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e =>
                            e.Value.Errors.Select(err => err.ErrorMessage).ToList()[0]
                        ).ToList();

                    var returnMessageModel = new ReturnMessageModel
                    {
                        ReturnMessage = errorMessage,
                        ValidationErrors = errors
                    };

                    var response = new Result<ReturnMessageModel>
                    {
                        IsSuccess = false,
                        Value = returnMessageModel,
                        ErrorMessage = errorMessage,
                        ValidationErrors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
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