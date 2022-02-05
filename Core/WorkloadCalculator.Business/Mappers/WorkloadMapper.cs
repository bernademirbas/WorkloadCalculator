using AutoMapper;
using WorkloadCalculator.Domain.Entities;
using WorkloadCalculator.Domain.Requests;
using WorkloadCalculator.Domain.Responses;

namespace WorkloadCalculator.Business.Mappers
{
    public class WorkloadMapper : Profile
    {
        public WorkloadMapper()
        {
            CreateMap<WorkloadRequest, Workload>()
                .ForSourceMember(d => d.Courses, opt => opt.DoNotValidate());
            CreateMap<Workload, WorkloadResponse>()
                .ForSourceMember(d => d.CreationDate, opt => opt.DoNotValidate())
                .ForSourceMember(d => d.WorkloadCourses, opt => opt.DoNotValidate())
                .ForSourceMember(d => d.Id, opt => opt.DoNotValidate());
        }
    }
}
