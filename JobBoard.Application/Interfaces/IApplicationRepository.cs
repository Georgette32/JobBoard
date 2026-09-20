using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Domain.Entities;

namespace JobBoard.Application.Interfaces
{
    public interface IApplicationRepository
    {
        Task AddAsync(JobBoard.Domain.Entities.Application application);

        Task SaveChangesAsync();
    }
}
