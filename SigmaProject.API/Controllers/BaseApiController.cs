using Microsoft.AspNetCore.Mvc;
using SigmaProject.Models.Common;

namespace SigmaProject.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null)
            {
                return NotFound();
            }
            if (result.IsSuccess && result.Value != null)
            {
                return Ok(result);
            }

            if (result.IsSuccess && result.Value == null)
            {
                return NotFound();
            }

            if (!result.IsSuccess && result.ValidationErrors != null)
            {
                return BadRequest(result);
            }
            return BadRequest(result);
        }
    }
}