using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkloadCalculator.Business.Interfaces;
using WorkloadCalculator.Domain.Responses;

namespace WorkloadCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// Gets all courses
        /// </summary>
        /// <returns>Course response data</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseResponse>>> GetCourses()
        {
            return Ok(await _courseService.GetCoursesAsync());
        }
    }
}
