using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Attendances.Api.Controllers
{
    [Route(AttendancesModule.BasePath)]
    internal class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get() => "Attendances API";
    }
}