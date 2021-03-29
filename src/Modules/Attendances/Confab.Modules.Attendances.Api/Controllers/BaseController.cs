using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Attendances.Api.Controllers
{
    [ApiController]
    [Route(AttendancesModule.BasePath + "/[controller]")]
    [ProducesDefaultContentType]
    internal abstract class BaseController : ControllerBase
    {
    }
}