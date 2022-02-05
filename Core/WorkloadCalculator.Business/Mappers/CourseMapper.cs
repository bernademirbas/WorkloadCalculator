using AutoMapper;
using WorkloadCalculator.Domain.Entities;
using WorkloadCalculator.Domain.Responses;

namespace WorkloadCalculator.Business.Mappers
{
    public class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<Course, CourseResponse>().ReverseMap();
        }
    }
}
