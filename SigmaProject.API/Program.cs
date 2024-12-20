using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SigmaProject.API.Extensions;
using SigmaProject.API.Middleware;
using SigmaProject.API.Middlewares;
using SigmaProject.Data;
using SigmaProject.Services.DependencyResolution;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServiceExtension(builder.Configuration);
            
            builder.Services.AddServiceLayer();
            
            builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

            using var app = builder.Build();
            
            using var scope = app.Services.CreateScope();
            var servies = scope.ServiceProvider;
            var dataContext = servies.GetRequiredService<DataContext>();
            dataContext.Database.Migrate();

            if (app.Environment.IsDevelopment())
            {
                app.UseApplicationSwagger(builder.Configuration, builder.Environment);
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseCors("CorsPolicy");
            app.UseSerilogRequestLogging();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
}