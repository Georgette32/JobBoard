using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JobBoard.Application.Interfaces;
using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly JobDbContext _context;

    public UserRepository(JobDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
