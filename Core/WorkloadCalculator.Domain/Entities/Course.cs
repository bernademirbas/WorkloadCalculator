using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkloadCalculator.Domain.Entities
{
    [Table("Course")]
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int TotalHour { get; set; }

        public virtual IList<WorkloadCourse> WorkloadCourses { get; set; } = new List<WorkloadCourse>();
    }
}
