namespace SigmaProject.API.Middleware;
public static class SwaggerMiddleware
{
    public static IApplicationBuilder UseApplicationSwagger(this IApplicationBuilder app, IConfiguration configuration, IHostEnvironment env)
    {
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "swagger/{documentName}/swagger.json";
            options.SerializeAsV2 = true;
        });

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sigma Candidate Project API");
        });

        return app;
    }
}