using FluentValidation;
using SigmaProject.Models.Candidate;

namespace SigmaProject.Services.Validators
{
    public class CreateOrUpdateCandidateModelValidator : AbstractValidator<CreateOrUpdateCandidateModel>
    {
        public CreateOrUpdateCandidateModelValidator()
        {
            RuleFor(model => model.FirstName)
                .NotNull().WithMessage("FirstName is required.")
                .NotEmpty().WithMessage("FirstName cannot be empty.")
                .MaximumLength(100).WithMessage("FirstName cannot exceed 100 characters.");

            RuleFor(model => model.LastName)
                .NotNull().WithMessage("LastName is required.")
                .NotEmpty().WithMessage("LastName cannot be empty.")
                .MaximumLength(100).WithMessage("LastName cannot exceed 100 characters.");

            RuleFor(model => model.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not in a valid format.")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.")
                .Matches(@"^(?!.*\.\.)[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Email must be in a valid format.");

            RuleFor(model => model.PhoneNumber)
                .MaximumLength(17).WithMessage("PhoneNumber cannot exceed 17 characters.")
                .Matches(@"^\+?[1-9]\d{0,2}(-?\d{1,4}){0,1}(-?\d{1,4}){0,1}(-?\d{1,4}){0,1}$")
                .WithMessage("PhoneNumber must be in a valid format.")
                    .When(model => !string.IsNullOrEmpty(model.PhoneNumber));

            RuleFor(model => model.CallTimeInterval)
                .MaximumLength(50).WithMessage("CallTimeInterval cannot exceed 50 characters.");

            RuleFor(model => model.LinkedInProfileUrl)
                .MaximumLength(500).WithMessage("LinkedInProfileUrl cannot exceed 500 characters.");

            RuleFor(model => model.GitHubProfileUrl)
                .MaximumLength(500).WithMessage("GitHubProfileUrl cannot exceed 500 characters.");

            RuleFor(model => model.Comment)
                .NotNull().WithMessage("Comment is required.")
                .NotEmpty().WithMessage("Comment cannot be empty.")
                .MaximumLength(1000).WithMessage("Comment cannot exceed 1000 characters.");
        }
    }
}

