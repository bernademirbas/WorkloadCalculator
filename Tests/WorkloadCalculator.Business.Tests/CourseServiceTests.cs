using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using WorkloadCalculator.Business.Services;
using WorkloadCalculator.Domain.Entities;
using WorkloadCalculator.Domain.Responses;
using WorkloadCalculator.Persistence.Interfaces;
using Xunit;

namespace WorkloadCalculator.Business.Tests
{
    public class CourseServiceTests
    {
        private readonly Mock<IGenericRepository> _mockGenericRepository;
        private readonly Mock<IMapper> _mockMapper;
        public CourseServiceTests()
        {
            _mockGenericRepository = new Mock<IGenericRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        private CourseService CreateCourseService()
        {
            return new CourseService(_mockGenericRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetCoursesAsync_ShouldBeTrue_WhenDataIsValid()
        {
            //Arrange
            var courseService = CreateCourseService();
            var mockCourses = new List<Course>()
            {
                new Course()
                {
                    Id = 1,
                    Name = "Blockchain and HR"
                }
            };
            var mockCourseResponses = new List<CourseResponse>()
            {
                new CourseResponse()
                {
                    Id = 1,
                    Name = "Blockchain and HR"
                }
            };
            _mockGenericRepository.Setup(repository => repository.GetAllAsync<Course>()).ReturnsAsync(mockCourses);
            _mockMapper.Setup(x => x.Map<IEnumerable<CourseResponse>>(mockCourses)).Returns(mockCourseResponses);
           
            //Act
            var courses = await courseService.GetCoursesAsync();

            // Assert
            _mockGenericRepository.VerifyAll();
            Assert.IsAssignableFrom<List<CourseResponse>>(courses);
        }
        
        [Fact]
        public async Task FindCoursesAsync_ShouldBeTrue_WhenDataIsValid()
        {
            //Arrange
            var courseService = CreateCourseService();
            var mockCourses = new List<Course>()
            {
                new Course()
                {
                    Id = 1,
                    Name = "Blockchain and HR"
                }
            };
            var mockCourseResponses = new List<CourseResponse>()
            {
                new CourseResponse()
                {
                    Id = 1,
                    Name = "Blockchain and HR"
                }
            };
            _mockGenericRepository.Setup(repository => repository.FindAllAsync(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(mockCourses);
            _mockMapper.Setup(x => x.Map<IEnumerable<CourseResponse>>(mockCourses)).Returns(mockCourseResponses);

            //Act
            var courses = await courseService.FindCoursesAsync(It.IsAny<Expression<Func<Course, bool>>>());

            // Assert
            _mockGenericRepository.VerifyAll();
            Assert.IsAssignableFrom<List<CourseResponse>>(courses);
        }
    }
}
