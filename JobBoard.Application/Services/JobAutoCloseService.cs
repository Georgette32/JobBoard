using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Application.Interfaces;
namespace JobBoard.Application.Services
{
    public class JobAutoCloseService
    {
        private readonly IJobRepository _jobRepository;
        public JobAutoCloseService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task AutoCloseJobsAsync()
        {
            var cutoffDate = DateTime.UtcNow.AddDays(-30);
            var jobs = await _jobRepository.GetActiveJobsOlderThanAsync(cutoffDate);
            foreach (var job in jobs)
            {
                job.Close(job.RecruiterId);
            }
            await _jobRepository.SaveChangesAsync();
        }
    }
}
