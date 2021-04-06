using Microsoft.AspNetCore.Mvc;

namespace Confab.Services.Tickets.Api.Controllers
{
    [Route("")]
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get() => "Tickets API";
    }
}