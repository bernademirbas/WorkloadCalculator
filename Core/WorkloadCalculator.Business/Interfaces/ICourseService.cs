using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkloadCalculator.Domain.Entities;
using WorkloadCalculator.Domain.Responses;

namespace WorkloadCalculator.Business.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseResponse>> GetCoursesAsync();
        Task<IEnumerable<CourseResponse>> FindCoursesAsync(Expression<Func<Course, bool>> match);
    }
}
