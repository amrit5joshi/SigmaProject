using Microsoft.AspNetCore.Mvc;
using Moq;
using SigmaProject.API.Controllers;
using SigmaProject.Models.Candidate;
using SigmaProject.Models.Common;
using SigmaProject.Services.Candidate;

namespace SigmaProject.Test.ApiLayer
{
    public class CandidateControllerTests
    {
        private readonly Mock<ICandidateService> _mockCandidateService;
        private readonly CandidateController _controller;

        public CandidateControllerTests()
        {
            _mockCandidateService = new Mock<ICandidateService>();
            _controller = new CandidateController(_mockCandidateService.Object);
        }

        [Fact]
        public async Task GetAllCandidates_ReturnsOkResult_WithCandidates()
        {
            var candidates = new List<CandidateModel>
            {
                 new CandidateModel { Id = 1, FirstName = "Amrit", LastName = "Joshi", Email = "amrit.joshi@example.com" },
                new CandidateModel { Id = 2, FirstName = "Ben", LastName = "Smith", Email = "ben.smith@example.com" },
                new CandidateModel { Id = 3, FirstName = "Hari", LastName = "Kumar", Email = "hari.kumar@example.com" },
                new CandidateModel { Id = 4, FirstName = "Ram", LastName = "Chandra", Email = "ram.chandra@example.com" },
                new CandidateModel { Id = 5, FirstName = "Pravesh", LastName = "Yadav", Email = "pravesh.yadav@example.com" }
            };

            var expectedResult = Result<List<CandidateModel>>.Success(candidates);

            _mockCandidateService.Setup(service => service.GetAllCandidates(It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetAllCandidates(CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult.Value, okResult.Value);
        }

        [Fact]
        public async Task CreateOrUpdateCandidate_ReturnsOkResult_WhenSuccessful()
        {
            var candidateRequest = new CreateOrUpdateCandidateModel
            {
                FirstName = "Ben",
                LastName = "Smith",
                Email = "ben.smith@example.com",
                PhoneNumber = "9876543210",
                CallTimeInterval = "Afternoon",
                LinkedInProfileUrl = "https://www.linkedin.com/in/bensmith/",
                GitHubProfileUrl = "https://github.com/bensmith",
                Comment = "Looking for opportunities in tech."
            };

            var resultMessage = new ReturnMessageModel { ReturnMessage = "Candidate created successfully." };
            var expectedResult = Result<ReturnMessageModel>.Success(resultMessage);

            _mockCandidateService.Setup(service => service.CreateOrUpdateCandidate(candidateRequest, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.CreateOrUpdateCandidate(candidateRequest, CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult.Value.ReturnMessage, ((ReturnMessageModel)okResult.Value).ReturnMessage);
        }


        [Fact]
        public async Task CreateOrUpdateCandidate_ReturnsBadRequest_WhenValidationFails()
        {
            var candidateRequest = new CreateOrUpdateCandidateModel
            {
                FirstName = "", 
                LastName = "JOSHI",
                Email = "JOSHI@gmail.com",
                Comment = "random comment"
            };

            var expectedResult = Result<ReturnMessageModel>.ValidationError(new List<string> { "First name is required." });

            _mockCandidateService.Setup(service => service.CreateOrUpdateCandidate(candidateRequest, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.CreateOrUpdateCandidate(candidateRequest, CancellationToken.None);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expectedResult.ValidationErrors, badRequestResult.Value);
        }

        [Fact]
        public async Task GetAllCandidates_ReturnsOkResult_WithNoCandidates()
        {
            var expectedResult = Result<List<CandidateModel>>.Success(new List<CandidateModel>());

            _mockCandidateService.Setup(service => service.GetAllCandidates(It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetAllCandidates(CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Empty(((List<CandidateModel>)okResult.Value));
        }

        [Fact]
        public async Task CreateOrUpdateCandidate_ReturnsBadRequest_ForInvalidCandidate()
        {
            var candidateRequest = new CreateOrUpdateCandidateModel
            {
                FirstName = "",
                LastName = "Kumar",
                Email = "invalid.email",
                Comment = "random comment"
            };

            var expectedResult = Result<ReturnMessageModel>.ValidationError(new List<string> { "First name is required.", "Email is not valid." });

            _mockCandidateService.Setup(service => service.CreateOrUpdateCandidate(candidateRequest, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.CreateOrUpdateCandidate(candidateRequest, CancellationToken.None);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expectedResult.ValidationErrors, badRequestResult.Value);
        }
    }
}