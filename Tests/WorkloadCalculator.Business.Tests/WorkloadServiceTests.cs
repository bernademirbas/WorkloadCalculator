using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using WorkloadCalculator.Business.Interfaces;
using WorkloadCalculator.Business.Services;
using WorkloadCalculator.Domain.Entities;
using WorkloadCalculator.Domain.Requests;
using WorkloadCalculator.Domain.Responses;
using WorkloadCalculator.Persistence.Interfaces;
using Xunit;

namespace WorkloadCalculator.Business.Tests
{
    public class WorkloadServiceTests
    {
        private readonly Mock<IGenericRepository> _mockGenericRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<WorkloadService>> _mockLogger;
        private readonly Mock<ICourseService> _mockCourseService;
        public WorkloadServiceTests()
        {
            _mockGenericRepository = new Mock<IGenericRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<WorkloadService>>();
            _mockCourseService = new Mock<ICourseService>();
        }

        private WorkloadService CreateWorkloadService()
        {
            return new WorkloadService(_mockGenericRepository.Object, _mockLogger.Object, _mockMapper.Object, _mockCourseService.Object);
        }

        [Fact]
        public async Task GenerateWorkloadAsync_ShouldThrowsException_WhenDateRangeIsNotValid()
        {
            //Arrange
            var workloadService = CreateWorkloadService();
            var workloadRequest = new WorkloadRequest()
            {
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now
            };
            //Act
            var exception = await Assert.ThrowsAsync<InvalidDataException>(async () => await workloadService.GenerateWorkloadAsync(workloadRequest));

            // Assert
            Assert.Equal("End date should be greater than start date!", exception.Message);
        }

        [Fact]
        public async Task GenerateWorkloadAsync_ShouldThrowsException_WhenDailyWorkHourGreaterThanLimit()
        {
            //Arrange
            var workloadService = CreateWorkloadService();
            var workloadRequest = new WorkloadRequest()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Courses = new List<CourseRequest>() { new CourseRequest() { Id = 1 } }
            };
            var courseResponse = new List<CourseResponse>() {new CourseResponse() {Id = 1, TotalHour = 10}};
            _mockCourseService.Setup(c => c.FindCoursesAsync(It.IsAny<Expression<Func<Course, bool>>>()))
                .ReturnsAsync(courseResponse);
            _mockMapper.Setup(x => x.Map<Workload>(workloadRequest)).Returns(new Workload());

            //Act
            var exception = await Assert.ThrowsAsync<InvalidDataException>(async () => await workloadService.GenerateWorkloadAsync(workloadRequest));

            // Assert
            Assert.Contains("Daily work hours should not be greater than daily limit", exception.Message);
        }

        [Fact]
        public async Task GenerateWorkloadAsync_ShouldThrowsException_WhenDailyWorkHourLessThanMinimum()
        {
            //Arrange
            var workloadService = CreateWorkloadService();
            var workloadRequest = new WorkloadRequest()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                Courses = new List<CourseRequest>() { new CourseRequest() { Id = 1 } }
            };
            var courseResponse = new List<CourseResponse>() { new CourseResponse() { Id = 1, TotalHour = 5 } };
            _mockCourseService.Setup(c => c.FindCoursesAsync(It.IsAny<Expression<Func<Course, bool>>>()))
                .ReturnsAsync(courseResponse);
            _mockMapper.Setup(x => x.Map<Workload>(workloadRequest)).Returns(new Workload());

            //Act
            var exception = await Assert.ThrowsAsync<InvalidDataException>(async () => await workloadService.GenerateWorkloadAsync(workloadRequest));

            // Assert
            Assert.Contains($"Daily work hours should not be less than 1. Please select valid date range", exception.Message);
        }

        [Fact]
        public async Task GenerateWorkloadAsync_ShouldBeTrue_WhenDataIsValid()
        {
            //Arrange
            var workloadService = CreateWorkloadService();
            var workloadRequest = new WorkloadRequest()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3),
                Courses = new List<CourseRequest>() { new CourseRequest() { Id = 1 } }
            };
            var courseResponse = new List<CourseResponse>() { new CourseResponse() { Id = 1, TotalHour = 15 } };
            _mockCourseService.Setup(c => c.FindCoursesAsync(It.IsAny<Expression<Func<Course, bool>>>()))
                .ReturnsAsync(courseResponse);
            _mockMapper.Setup(x => x.Map<Workload>(workloadRequest)).Returns(new Workload());
            _mockMapper.Setup(x => x.Map<WorkloadResponse>(It.IsAny<Workload>())).Returns(new WorkloadResponse());
            _mockGenericRepository.Setup(x => x.SaveAsync(It.IsAny<Workload>()));

            //Act
            var workloadResponse = await workloadService.GenerateWorkloadAsync(workloadRequest);

            // Assert
            _mockCourseService.VerifyAll();
            _mockMapper.VerifyAll();
            _mockGenericRepository.VerifyAll();
            Assert.IsAssignableFrom<WorkloadResponse>(workloadResponse);
        }

    }
}
