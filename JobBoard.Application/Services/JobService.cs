using System;
using System.Collections.Generic;
using System.Linq;

using JobBoard.Application.Interfaces;

namespace JobBoard.Application.Services
{
    public class JobService
    {
        private readonly IJobRepository _jobRepository;
        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

    }
}
