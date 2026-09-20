using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Application.Interfaces;
using JobBoard.Infrastructure.Data;

namespace JobBoard.Infrastructure.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly JobDbContext _context;

    public ApplicationRepository(JobDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(JobBoard.Domain.Entities.Application application)
    {
        await _context.Applications.AddAsync(application);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}