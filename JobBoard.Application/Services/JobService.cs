using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Application.Interfaces;
using JobBoard.Domain.Enums;
namespace JobBoard.Application.Services
{
    public class JobService
    {
        private readonly IJobRepository _jobRepository;
        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task CloseJobAsync(int jobId, int recruiterId)
        {
            var job = await _jobRepository.GetByIdAsync(jobId);
            if (job == null)
            {
                throw new KeyNotFoundException("job not found");
            }
            if (job.RecruiterId != recruiterId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this job.");
            }
            if (job.Status != JobStatus.Active)
            {
                throw new InvalidOperationException("Only active jobs can be closed.");
            }
            job.Close(recruiterId);
            await _jobRepository.SaveChangesAsync();
        } 
    }
}
