using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Application.Interfaces;
using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly JobDbContext _context;

        public JobRepository(JobDbContext context)
        {
            _context = context;
        }

        public async Task<Job?> GetByIdAsync(int jobId)
        {
            return await _context.Jobs.FindAsync(jobId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

