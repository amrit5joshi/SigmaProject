using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SigmaProject.Models.Candidate;
using SigmaProject.Models.Common;
using SigmaProject.Services.Candidate;
using Swashbuckle.AspNetCore.Annotations;

namespace SigmaProject.API.Controllers
{
    [ApiVersion("1")]
    public class CandidateController : BaseApiController
    {
        private readonly ICandidateService _candidateService;
        private const string _swaggerOperationTag = "Candidate";

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost("create-or-update")]
        [SwaggerOperation(
        Summary = "Creates or updates candidates",
        Description = "Creates a new candidate or updates an existing one based on provided details.",
        OperationId = "candidate.create-or-update",
        Tags = new[] { _swaggerOperationTag })]
        [SwaggerResponse(StatusCodes.Status200OK, "Candidate created or updated successfully.", type: typeof(Result<ReturnMessageModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid. Please check the input data and try again.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "An internal server error occurred while processing the request.")]
        public async Task<IActionResult> CreateOrUpdateCandidate([FromBody, SwaggerParameter("Parameters for creating or updating a candidate.", Required = true)] CreateOrUpdateCandidateModel request, CancellationToken cancellationToken)
        {
            var data = await _candidateService.CreateOrUpdateCandidate(request, cancellationToken);
            return HandleResult(data);
        }

        [HttpGet()]
        [SwaggerOperation(
        Summary = "Retrieve all candidates",
        Description = "Fetches a list of all candidates from the system.",
        OperationId = "candidate",
        Tags = new[] { _swaggerOperationTag })]
        [SwaggerResponse(StatusCodes.Status200OK, "A list of candidates retrieved successfully.", type: typeof(Result<List<CandidateModel>>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "An internal server error occurred while processing the request.")]
        public async Task<IActionResult> GetAllCandidates(CancellationToken cancellationToken)
        {
            var candidates = await _candidateService.GetAllCandidates(cancellationToken);
            return HandleResult(candidates);
        }
    }
}
