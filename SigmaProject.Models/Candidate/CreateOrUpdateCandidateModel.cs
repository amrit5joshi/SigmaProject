using SigmaProject.Models.OpenApi;
using Swashbuckle.AspNetCore.Annotations;

namespace SigmaProject.Models.Candidate
{
    [SwaggerSchemaFilter(typeof(CreateorUpdateCandidateModelFilter))]
    public class CreateOrUpdateCandidateModel
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CallTimeInterval { get; set; }
        public string? LinkedInProfileUrl { get; set; }
        public string? GitHubProfileUrl { get; set; }
        public string Comment { get; set; }
    }
}