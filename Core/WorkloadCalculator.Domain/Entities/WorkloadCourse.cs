using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkloadCalculator.Domain.Entities
{
    [Table("WorkloadCourse")]
    public class WorkloadCourse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int WorkloadId { get; set; }

        public virtual Workload Workload { get; set; }

        [Required]
        public int CourseId { get; set; }

        [NotMapped]
        public virtual Course Course { get; set; }


    }
}
