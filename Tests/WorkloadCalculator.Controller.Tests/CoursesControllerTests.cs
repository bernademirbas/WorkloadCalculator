using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WorkloadCalculator.API.Controllers;
using WorkloadCalculator.Business.Interfaces;
using WorkloadCalculator.Domain.Responses;
using Xunit;

namespace WorkloadCalculator.Controller.Tests
{
    public class CoursesControllerTests
    {
        private readonly Mock<ICourseService> _mockCourseService;

        public CoursesControllerTests()
        {
            this._mockCourseService = new Mock<ICourseService>();
        }

        private CoursesController CreateCoursesController()
        {
            return new CoursesController(_mockCourseService.Object);
        }

        [Fact]
        public async Task GetCoursesAsync_ShouldBeTrue_WhenDataIsValid()
        {
            // Arrange
            var coursesController = this.CreateCoursesController();
            _mockCourseService.Setup(y => y.GetCoursesAsync()).ReturnsAsync(new List<CourseResponse>());

            // Act
            var result = await coursesController.GetCourses();

            // Assert
            Assert.IsAssignableFrom<ActionResult<IEnumerable<CourseResponse>>>(result);
            var objectResult = (OkObjectResult)result.Result;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

    }
}
