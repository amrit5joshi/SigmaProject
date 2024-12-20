using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using SigmaProject.Models.Candidate;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SigmaProject.Models.OpenApi
{
    public class CreateorUpdateCandidateModelFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context) => schema.Example = new OpenApiObject
        {
            [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(CreateOrUpdateCandidateModel.FirstName))] = new OpenApiString("amrit"),
            [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(CreateOrUpdateCandidateModel.LastName))] = new OpenApiString("joshi"),
            [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(CreateOrUpdateCandidateModel.Email))] = new OpenApiString("amrit@gmail.com"),
            [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(CreateOrUpdateCandidateModel.PhoneNumber))] = new OpenApiString("13141413"),
            [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(CreateOrUpdateCandidateModel.CallTimeInterval))] = new OpenApiString("After 6PM"),
            [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(CreateOrUpdateCandidateModel.LinkedInProfileUrl))] = new OpenApiString("aj.linkedin.com"),
            [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(CreateOrUpdateCandidateModel.GitHubProfileUrl))] = new OpenApiString("aj.github.com"),
            [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(CreateOrUpdateCandidateModel.Comment))] = new OpenApiString("i am amrit@gmail.com"),
        };
    }
}
