using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WorkloadCalculator.Business.Interfaces;
using WorkloadCalculator.Domain.Entities;
using WorkloadCalculator.Domain.Requests;
using WorkloadCalculator.Domain.Responses;
using WorkloadCalculator.Persistence.Interfaces;

namespace WorkloadCalculator.Business.Services
{
    public class WorkloadService : IWorkloadService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ICourseService _courseService;
        private readonly ILogger<WorkloadService> _logger;
        private readonly IMapper _mapper;

        public WorkloadService(IGenericRepository genericRepository, ILogger<WorkloadService> logger, IMapper mapper, ICourseService courseService)
        {
            _genericRepository = genericRepository;
            _logger = logger;
            _mapper = mapper;
            _courseService = courseService;
        }

        /// <summary>
        /// Generates Workload according to workload request content
        /// </summary>
        /// <param name="workloadRequest">Request data: start date, end date, courses</param>
        /// <returns></returns>
        public async Task<WorkloadResponse> GenerateWorkloadAsync(WorkloadRequest workloadRequest)
        {
            ValidateWorkloadDates(workloadRequest.StartDate, workloadRequest.EndDate);
            var courseIds = workloadRequest.Courses.Select(cr => cr.Id).ToList();
            var timeSpan = (workloadRequest.EndDate ?? DateTime.Now) - (workloadRequest.StartDate ?? DateTime.Now);

            var workload = _mapper.Map<Workload>(workloadRequest);
            workload.TotalHours = await GetSelectedCourseHours(timeSpan, courseIds);
            workload.CreationDate = DateTime.Now;
            workload.WorkloadCourses = courseIds.Select(c => new WorkloadCourse()
            {
                CourseId = c,
            }).ToList();

            await _genericRepository.SaveAsync(workload);
            return _mapper.Map<WorkloadResponse>(workload);
        }

        /// <summary>
        /// Calculates Selected Courses Total hours
        /// </summary>
        /// <param name="workloadTimeSpan">total selected time span</param>
        /// <param name="courseIds">selected course Id list</param>
        /// <returns></returns>
        private async Task<int> GetSelectedCourseHours(TimeSpan workloadTimeSpan, List<int> courseIds)
        {
            var selectedCourses = await _courseService.FindCoursesAsync(c => courseIds.Contains(c.Id));
            var selectedCourseHours = selectedCourses.Sum(c => c.TotalHour);
            ValidatesDailyWorkHourLimit(workloadTimeSpan, selectedCourseHours);
            return selectedCourseHours;
        }

        /// <summary>
        /// Validates daily work hour limit according selected courses total hours
        /// </summary>
        /// <param name="workloadTimeSpan">total selected time span</param>
        /// <param name="selectedCourseHours">Sum of selected courses total hours</param>
        private void ValidatesDailyWorkHourLimit(TimeSpan workloadTimeSpan, int selectedCourseHours)
        {
            var dailyWorkLimit = 6;
            if (selectedCourseHours / workloadTimeSpan.Days > dailyWorkLimit)
            {
                var exceptionMessage = $"Daily work hours should not be greater than daily limit {dailyWorkLimit}";
                _logger.LogError(exceptionMessage);
                throw new InvalidDataException(exceptionMessage);
            }

            if (selectedCourseHours / workloadTimeSpan.Days < 1)
            {
                var exceptionMessage = "Daily work hours should not be less than 1. Please select valid date range";
                _logger.LogError(exceptionMessage);
                throw new InvalidDataException(exceptionMessage);
            }
        }

        /// <summary>
        /// Validates workload dates according to date values
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        private void ValidateWorkloadDates(DateTime? startDate, DateTime? endDate)
        {
            if (startDate >= endDate)
            {
                var exceptionMessage = "End date should be greater than start date!";
                _logger.LogError(exceptionMessage);
                throw new InvalidDataException(exceptionMessage);
            }
        }
    }
}
