using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Services.Tickets.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesDefaultContentType]
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is not null)
            {
                return Ok(model);
            }

            return NotFound();
        }
    }
}