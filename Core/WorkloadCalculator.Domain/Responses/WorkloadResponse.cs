using System;

namespace WorkloadCalculator.Domain.Responses
{
    public class WorkloadResponse
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int TotalHours { get; set; }
    }
}
