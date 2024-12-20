using SigmaProject.Models.Candidate;
using SigmaProject.Models.Common;

namespace SigmaProject.Services.Candidate
{
    public interface ICandidateService
    {
        Task<Result<ReturnMessageModel>> CreateOrUpdateCandidate(CreateOrUpdateCandidateModel request, CancellationToken cancellationToken);
        Task<Result<List<CandidateModel>>> GetAllCandidates(CancellationToken cancellationToken);
    }
}
