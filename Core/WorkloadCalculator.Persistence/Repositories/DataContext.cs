using Microsoft.EntityFrameworkCore;
using WorkloadCalculator.Domain.Entities;

namespace WorkloadCalculator.Persistence.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Workload> Workloads { get; set; }
        public DbSet<WorkloadCourse> WorkloadCourses { get; set; }
    }
}
