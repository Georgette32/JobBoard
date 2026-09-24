using JobBoard.Domain.Entities;

namespace JobBoard.Application.Interfaces
{
    public interface IJobRepository
    {
        Task<Job?> GetByIdAsync(int jobId);
        Task<List<Job>> GetActiveJobsOlderThanAsync(DateTime date);
        Task SaveChangesAsync();
    }
}