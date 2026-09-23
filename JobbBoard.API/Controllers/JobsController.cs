using JobBoard.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MediatR;
using JobBoard.Application.Features.Jobs.Commands;


namespace JobbBoard.API.Controllers;


[ApiController]
[Route("api/jobs")]

public class JobsController : ControllerBase
{
    private readonly JobService _jobService;
    private readonly ApplicationService _applicationService;
    private readonly IMediator _mediator;

    public JobsController(
        JobService jobService,
        ApplicationService applicationService,
        IMediator mediator)
    {
        _jobService = jobService;
        _applicationService = applicationService;
        _mediator = mediator;
    }
    /// <summary>
    /// Closes an active job owned by the authenticated recruiter.
    /// </summary>
    /// <param name="jobId">The ID of the job to close.</param>
    [Authorize(Roles = "Recruiter")]
    [HttpPut("{jobId}/close")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
   
    public async Task<IActionResult> CloseJob(int jobId)
    {
        try
        {
            var recruiterIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(recruiterIdClaim, out var recruiterId))
            {
                return Unauthorized();
            }

            await _mediator.Send(new CloseJobCommand(jobId, recruiterId));

            return Ok(new
            {
                message = "Job closed successfully."
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new
            {
                message = "Job not found."
            });
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        } }
        [HttpPost("{jobId}/applications")]
        public async Task<IActionResult> ApplyToJob(int jobId, int candidateId)
        {
            try
            {


            await _applicationService.ApplyAsync(jobId, candidateId);

            return Ok(new
                {
                    message = "Application submitted successfully."
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = "Job not found."
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }

