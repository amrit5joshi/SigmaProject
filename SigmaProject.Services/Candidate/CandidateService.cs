using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SigmaProject.Data;
using SigmaProject.Models.Candidate;
using SigmaProject.Models.Common;
using SigmaProject.Services.Validators;

namespace SigmaProject.Services.Candidate;

public class CandidateService : ICandidateService
{
    private readonly DataContext _context;
    private readonly ILogger<CandidateService> _logger;
    private readonly IMemoryCache _cache;
    private ILogger<CandidateService> object1;
    private IMemoryCache object2;
    private const string candidateEntity = "Candidate";
    public CandidateService(DataContext context, ILogger<CandidateService> logger, IMemoryCache cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }

    public async Task<Result<ReturnMessageModel>> CreateOrUpdateCandidate(CreateOrUpdateCandidateModel request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateOrUpdateCandidateModelValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result<ReturnMessageModel>.ValidationError(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            var existingCandidate = await _context.Candidate
                .FirstOrDefaultAsync(c => c.Email == request.Email, cancellationToken);

            if (existingCandidate == null)
            {
                existingCandidate = new SigmaProject.Data.Entities.Candidate.Candidate
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    CallTimeInterval = request.CallTimeInterval,
                    GitHubProfileUrl = request.GitHubProfileUrl,
                    LinkedInProfileUrl = request.LinkedInProfileUrl,
                    Comment = request.Comment,
                };
                await _context.Candidate.AddAsync(existingCandidate, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _cache.Remove("AllCandidates");
                return Result<ReturnMessageModel>.Success(new ReturnMessageModel { ReturnMessage = ReturnMessageModel.SaveSuccess(candidateEntity) });
            }
            else
            {
                var newCandidate = new SigmaProject.Data.Entities.Candidate.Candidate
                {
                    Id = existingCandidate.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber ?? existingCandidate.PhoneNumber,
                    Email = request.Email,
                    CallTimeInterval = request.CallTimeInterval ?? existingCandidate.CallTimeInterval,
                    GitHubProfileUrl = request.GitHubProfileUrl ?? existingCandidate.GitHubProfileUrl,
                    LinkedInProfileUrl = request.LinkedInProfileUrl ?? existingCandidate.LinkedInProfileUrl,
                    Comment = request.Comment,
                };


                existingCandidate.FirstName = request.FirstName;
                existingCandidate.LastName = request.LastName;
                existingCandidate.PhoneNumber = request.PhoneNumber;
                existingCandidate.Email = request.Email;
                existingCandidate.CallTimeInterval = request.CallTimeInterval;
                existingCandidate.GitHubProfileUrl = request.GitHubProfileUrl;
                existingCandidate.LinkedInProfileUrl = request.LinkedInProfileUrl;
                existingCandidate.Comment = request.Comment;

                _context.Candidate.Update(existingCandidate);
                await _context.SaveChangesAsync(cancellationToken);
                _cache.Remove("AllCandidates");
                return Result<ReturnMessageModel>.Success(new ReturnMessageModel { ReturnMessage = ReturnMessageModel.UpdateSuccess(candidateEntity) });
            }
        }
        catch (DbUpdateException dbUpdateException)
        {
            _logger.LogError(dbUpdateException, "Database update error: {Message}", dbUpdateException.Message);
            return Result<ReturnMessageModel>.Error("An error occurred while updating the database. Please try again.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            return Result<ReturnMessageModel>.Error("An unexpected error occurred. Please try again.");
        }
    }

    public async Task<Result<List<CandidateModel>>> GetAllCandidates(CancellationToken cancellationToken)
    {
        const string cacheKey = "AllCandidates";

        try
        {
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<SigmaProject.Data.Entities.Candidate.Candidate> candidates))
            {
                candidates = await _context.Candidate.ToListAsync(cancellationToken);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(cacheKey, candidates, cacheOptions);
            }

            var candidateModels = candidates.Select(c => new CandidateModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                CallTimeInterval = c.CallTimeInterval,
                GitHubProfileUrl = c.GitHubProfileUrl,
                LinkedInProfileUrl = c.LinkedInProfileUrl,
                Comment = c.Comment
            }).ToList();

            return Result<List<CandidateModel>>.Success(candidateModels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Result<List<CandidateModel>>.Error(ex.Message);
        }
    }
}