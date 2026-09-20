using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Domain.Entities
{
    public class Application
    {
        public int ApplicationId { get; set; }

        public int JobId { get; set; }

        public int CandidateId { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    }
}
