using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WorkloadCalculator.API.Controllers;
using WorkloadCalculator.Business.Interfaces;
using WorkloadCalculator.Domain.Requests;
using WorkloadCalculator.Domain.Responses;
using Xunit;

namespace WorkloadCalculator.Controller.Tests
{
    public class WorkloadControllerTests
    {
        private readonly Mock<IWorkloadService> _mockWorkloadService;

        public WorkloadControllerTests()
        {
            this._mockWorkloadService = new Mock<IWorkloadService>();
        }

        private WorkloadController CreateWorkloadController()
        {
            return new WorkloadController(_mockWorkloadService.Object);
        }

        [Fact]
        public async Task GenerateWorkloadAsync_ShouldBeTrue_WhenDataIsValid()
        {
            // Arrange
            var workloadController = this.CreateWorkloadController();
            _mockWorkloadService.Setup(y => y.GenerateWorkloadAsync(It.IsAny<WorkloadRequest>()))
                .ReturnsAsync(new WorkloadResponse());
           
            // Act
            var result = await workloadController.GenerateWorkloadAsync(new WorkloadRequest());

            // Assert
            Assert.IsAssignableFrom<ActionResult<WorkloadResponse>>(result);
            var objectResult = (OkObjectResult)result.Result;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

    }
}
