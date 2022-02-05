using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkloadCalculator.Domain.Requests
{
    public class WorkloadRequest
    {
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Courses is required")]
        public IEnumerable<CourseRequest> Courses { get; set; }
    }
}
