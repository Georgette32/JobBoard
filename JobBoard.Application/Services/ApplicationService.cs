using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JobBoard.Application.Interfaces;
using JobBoard.Domain.Entities;
using JobBoard.Domain.Enums;

namespace JobBoard.Application.Services;

public class ApplicationService
{
    private readonly IJobRepository _jobRepository;
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationService(
        IJobRepository jobRepository,
        IApplicationRepository applicationRepository)
    {
        _jobRepository = jobRepository;
        _applicationRepository = applicationRepository;
    }

    public async Task ApplyAsync(int jobId, int candidateId)
    {
        var job = await _jobRepository.GetByIdAsync(jobId);

        if (job == null)
        {
            throw new KeyNotFoundException("Job not found.");
        }

        if (job.Status != JobStatus.Active)
        {
            throw new InvalidOperationException(
                "You cannot apply to a closed job.");
        }

        var application = new JobBoard.Domain.Entities.Application
        {
            JobId = jobId,
            CandidateId = candidateId,
            Status = "Pending",
            AppliedAt = DateTime.UtcNow
        };

        await _applicationRepository.AddAsync(application);
        await _applicationRepository.SaveChangesAsync();
    }
}