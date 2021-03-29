using System;
using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [ApiController]
    [ProducesDefaultContentType]
    [Route(ConferencesModule.BasePath + "/[controller]")]
    internal class BaseController : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is null)
            {
                return NotFound();
            }

            return Ok(model);
        }
        
        protected void AddResourceIdHeader(Guid id) => Response.Headers.Add("Resource-ID", id.ToString());
    }
}