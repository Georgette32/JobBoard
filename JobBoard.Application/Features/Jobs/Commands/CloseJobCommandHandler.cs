using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Application.Interfaces;
using JobBoard.Domain.Enums;

namespace JobBoard.Application.Features.Jobs.Commands
{
    public class CloseJobCommandHandler : IRequestHandler<CloseJobCommand>
    {
private readonly IJobRepository _jobRepository;
        public CloseJobCommandHandler(IJobRepository jobRepository) {
            _jobRepository = jobRepository;
    }
        public async Task<Unit> Handle(
     CloseJobCommand request,
     CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.jobId);

            if (job == null)
            {
                throw new KeyNotFoundException("job not found");
            }

            if (job.RecruiterId != request.recruiterId)
            {
                throw new UnauthorizedAccessException(
                    "You are not the owner of this job.");
            }

            if (job.Status != JobStatus.Active)
            {
                throw new InvalidOperationException(
                    "Only active jobs can be closed.");
            }

            job.Close(request.recruiterId);

            await _jobRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
