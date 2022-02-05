using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkloadCalculator.Domain.Entities
{
    [Table("Workload")]
    public class Workload
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public int TotalHours { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public virtual IList<WorkloadCourse> WorkloadCourses { get; set; } = new List<WorkloadCourse>();
    }
}
