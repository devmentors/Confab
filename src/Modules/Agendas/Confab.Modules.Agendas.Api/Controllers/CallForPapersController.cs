using System;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.CallForPapers.Commands;
using Confab.Modules.Agendas.Application.CallForPapers.DTO;
using Confab.Modules.Agendas.Application.CallForPapers.Queries;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Queries;
using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    [Route(AgendasModule.BasePath + "/conferences/{conferenceId:guid}/cfp")]
    [Authorize(Policy)]
    internal class CallForPapersController : BaseController
    {
        private const string Policy = "cfp";
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public CallForPapersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<CallForPapersDto>> GetAsync(Guid conferenceId) 
            => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetCallForPapers {ConferenceId = conferenceId}));

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Guid conferenceId, CreateCallForPapers command)
        {
            await _commandDispatcher.SendAsync(command.Bind(x => x.ConferenceId, conferenceId));
            return CreatedAtAction("Get", new {conferenceId = command.ConferenceId}, null);
        }

        [HttpPut("open")]
        public async Task<ActionResult> OpenAsync(Guid conferenceId)
        {
            await _commandDispatcher.SendAsync(new OpenCallForPapers(conferenceId));
            return NoContent();
        }
        
        [HttpPut("close")]
        public async Task<ActionResult> CloseAsync(Guid conferenceId)
        {
            await _commandDispatcher.SendAsync(new OpenCallForPapers(conferenceId));
            return NoContent();
        }
    }
}