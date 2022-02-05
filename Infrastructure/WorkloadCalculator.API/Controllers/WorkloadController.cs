using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkloadCalculator.Business.Interfaces;
using WorkloadCalculator.Domain.Requests;
using WorkloadCalculator.Domain.Responses;

namespace WorkloadCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkloadController : ControllerBase
    {
        private readonly IWorkloadService _workloadService;
        public WorkloadController(IWorkloadService workloadService)
        {
            _workloadService = workloadService;
        }

        /// <summary>
        /// Generates workload according to workload request content
        /// </summary>
        /// <param name="workloadRequest">Request data: start date, end date, courses</param>
        /// <returns>Generated workload response</returns>
        [HttpPost]
        [Route("generate")]
        public async Task<ActionResult<WorkloadResponse>> GenerateWorkloadAsync(WorkloadRequest workloadRequest)
        {
            var workloadResponse = await _workloadService.GenerateWorkloadAsync(workloadRequest);
            return this.Ok(workloadResponse);
        }
    }
}
