using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using System.Threading.Tasks;
using WorkloadCalculator.Business.Interfaces;
using WorkloadCalculator.Domain.Entities;
using WorkloadCalculator.Domain.Responses;
using WorkloadCalculator.Persistence.Interfaces;

namespace WorkloadCalculator.Business.Services
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public CourseService(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all courses
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseResponse>> GetCoursesAsync()
        {
           var courses = await _genericRepository.GetAllAsync<Course>();
           return _mapper.Map<IEnumerable<CourseResponse>>(courses);
        }

        /// <summary>
        /// Gets filtered data for courses
        /// </summary>
        /// <param name="match">Filter Expression for course</param>
        /// <returns></returns>
        public async Task<IEnumerable<CourseResponse>> FindCoursesAsync(Expression<Func<Course, bool>> match)
        {
            var filteredCourses = await _genericRepository.FindAllAsync(match);
            return _mapper.Map<IEnumerable<CourseResponse>>(filteredCourses);
        }
    }
}
