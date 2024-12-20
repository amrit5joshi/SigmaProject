using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using SigmaProject.Data;
using SigmaProject.Models.Candidate;
using SigmaProject.Models.Common;
using SigmaProject.Services.Candidate;

namespace SigmaProject.Test.Services
{
    public class CandidateServiceTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly Mock<ILogger<CandidateService>> _mockLogger;
        private readonly Mock<IMemoryCache> _mockCache;
        private readonly CandidateService _service;
        private readonly DbContextOptions<DataContext> _options;

        public CandidateServiceTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "InMemoryDb")
                    .Options;

            _mockLogger = new Mock<ILogger<CandidateService>>();
            _mockCache = new Mock<IMemoryCache>();
            _service = new CandidateService(new DataContext(_options), _mockLogger.Object, _mockCache.Object);
        }

        [Fact]
        public async Task CreateOrUpdateCandidate_ReturnsValidationError_WhenValidationFails()
        {
            var candidateRequest = new CreateOrUpdateCandidateModel
            {
                FirstName = "",
                LastName = "Smith",
                Email = "ben.smith@example.com"
            };

            var result = await _service.CreateOrUpdateCandidate(candidateRequest, CancellationToken.None);

            var validationResult = Assert.IsType<Result<ReturnMessageModel>>(result);
            Assert.Contains("FirstName cannot be empty.", validationResult.ValidationErrors);
            Assert.Contains("Comment is required.", validationResult.ValidationErrors);
            Assert.Contains("Comment cannot be empty.", validationResult.ValidationErrors);
        }

        [Fact]
        public async Task CreateOrUpdateCandidate_ReturnsSuccess_WhenCandidateIsCreated()
        {
            var candidateRequest = new CreateOrUpdateCandidateModel
            {
                FirstName = "Ben",
                LastName = "Smith",
                Email = "ben.smith@example.com",
                PhoneNumber = "9876543210",
                Comment = "New candidate"
            };

            var result = await _service.CreateOrUpdateCandidate(candidateRequest, CancellationToken.None);

            var successResult = Assert.IsType<Result<ReturnMessageModel>>(result);
            Assert.Equal("Candidate Saved Successfully", successResult.Value.ReturnMessage);

            using (var context = new DataContext(_options))
            {
                var candidates = await context.Candidate.ToListAsync();
                Assert.Single(candidates);
                Assert.Equal("Ben", candidates.First().FirstName);
            }
        }

        [Fact]
        public async Task CreateOrUpdateCandidate_ReturnsSuccess_WhenCandidateIsUpdated()
        {
            var existingCandidate = new SigmaProject.Data.Entities.Candidate.Candidate
            {
                FirstName = "Amrit",
                LastName = "Joshi",
                Email = "john.doe@example.com",
                Comment = "old comment candidate"
            };

            using (var context = new DataContext(_options))
            {
                context.Candidate.Add(existingCandidate);
                await context.SaveChangesAsync();
            }

            var candidateRequest = new CreateOrUpdateCandidateModel
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Comment = "Updated candidate"
            };

            var result = await _service.CreateOrUpdateCandidate(candidateRequest, CancellationToken.None);

            var successResult = Assert.IsType<Result<ReturnMessageModel>>(result);
            Assert.Equal("Candidate Updated Successfully", successResult.Value.ReturnMessage);

            using (var context = new DataContext(_options))
            {
                var updatedCandidate = await context.Candidate.FindAsync(existingCandidate.Id);
                Assert.Equal("Jane", updatedCandidate.FirstName);
            }
        }
    }
}