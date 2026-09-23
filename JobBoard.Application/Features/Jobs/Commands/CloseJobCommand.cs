using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Jobs.Commands
{
    public record CloseJobCommand(int jobId , int recruiterId) :
        IRequest;
}
