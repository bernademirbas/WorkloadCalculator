using System.Threading.Tasks;
using WorkloadCalculator.Domain.Requests;
using WorkloadCalculator.Domain.Responses;

namespace WorkloadCalculator.Business.Interfaces
{
    public interface IWorkloadService
    {
        Task<WorkloadResponse> GenerateWorkloadAsync(WorkloadRequest workloadRequest);
    }
}
