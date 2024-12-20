using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SigmaProject.Services.Candidate;
using SigmaProject.Services.Validators;

namespace SigmaProject.Services.DependencyResolution;

public static class DependencyExtensions
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        var assembly = typeof(DependencyExtensions).Assembly;
        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped<ICandidateService,CandidateService>();
        services.AddTransient(typeof(CreateOrUpdateCandidateModelValidator));
        services.AddMemoryCache();
        services.AddDistributedMemoryCache();

        return services;
    }
}