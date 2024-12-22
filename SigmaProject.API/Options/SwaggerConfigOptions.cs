using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SigmaProject.API.Options
{
    public class SwaggerConfigOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;
        public SwaggerConfigOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            options.EnableAnnotations();
            foreach (var desc in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(desc.GroupName, new OpenApiInfo()
                {
                    Title = "Sigma Project API",
                    Version = desc.ApiVersion.ToString()
                });
            }
        }
    }
}
