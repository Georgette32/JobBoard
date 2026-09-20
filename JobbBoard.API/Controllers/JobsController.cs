using JobBoard.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace JobbBoard.API.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobsController : ControllerBase
{
    private readonly JobService _jobService;
    private readonly ApplicationService _applicationService;

    public JobsController(
        JobService jobService,
        ApplicationService applicationService)
    {
        _jobService = jobService;
        _applicationService = applicationService;
    }
    [Authorize(Roles = "Recruiter")]
    [HttpPost("{jobId}/close")]
    public async Task<IActionResult> CloseJob(int jobId)
    {
        try
        {
            var recruiterIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(recruiterIdClaim, out var recruiterId))
            {
                return Unauthorized();
            }

            await _jobService.CloseJobAsync(jobId, recruiterId);

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

