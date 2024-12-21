using Asp.Versioning.ApiExplorer;

namespace SigmaProject.API.Middleware;
public static class SwaggerMiddleware
{
    public static IApplicationBuilder UseApplicationSwagger(this IApplicationBuilder app, IConfiguration configuration, IApiVersionDescriptionProvider versionDescriptionProvider)
    {
        
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "swagger/{documentName}/swagger.json";
            options.SerializeAsV2 = true;
        });

        app.UseSwaggerUI(c =>
        {
            foreach(var desc in versionDescriptionProvider.ApiVersionDescriptions)
            {
                c.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", 
                    $"Sigma Candidate API - {desc.GroupName.ToUpper()}");
            }
        });

        return app;
    }
}