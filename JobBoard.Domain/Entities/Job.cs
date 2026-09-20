using JobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Domain.Entities
{
    public class Job
    {
        public int JobId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int RecruiterId { get; set; }
        public JobStatus Status { get; set; } = JobStatus.Active;
        public DateTime? ClosedAt {  get; set; }
        public int? ClosedBy { get; set; }
        public ICollection<Application> Applications { get; set; } = new List<Application>();

        public void Close(int recruiterId)
        {
            if (Status != JobStatus.Active)
            {
                throw new InvalidOperationException("Only active jobs can be closed.");
            }

            Status = JobStatus.Closed;
            ClosedAt = DateTime.UtcNow;
            ClosedBy = recruiterId;
        }
    }
}
