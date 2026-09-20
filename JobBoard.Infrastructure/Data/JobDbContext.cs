using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Domain.Entities;
namespace JobBoard.Infrastructure.Data
{
    public class JobDbContext: DbContext
    {
        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobBoard.Domain.Entities.Application> Applications { get; set; }
        public DbSet<JobBoard.Domain.Entities.User> Users { get; set; }
    }
}
