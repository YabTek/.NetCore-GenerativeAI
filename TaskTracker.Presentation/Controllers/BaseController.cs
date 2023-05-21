using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Responses;

namespace TaskTracker.Presentation.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BaseController: ControllerBase
{
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result == null) 
            return NotFound(result);
        else if (result.Success && result.Value != null)
            return Ok(result);
        else if (result.Success && result.Value == null)
            return NotFound(result);
        else 
            return BadRequest(result);
    }
}