using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Application.Commands;
using Confab.Modules.Attendances.Application.DTO;
using Confab.Modules.Attendances.Application.Queries;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Contexts;
using Confab.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Attendances.Api.Controllers
{
    [Authorize]
    internal class AttendancesController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IContext _context;

        public AttendancesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher,
            IContext context)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _context = context;
        }
        
        [HttpGet("{conferenceId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IReadOnlyList<AttendanceDto>>> BrowseAttendancesAsync(Guid conferenceId)
        {
            var attendances = await _queryDispatcher.QueryAsync(new BrowseAttendances
            {
                ConferenceId = conferenceId,
                UserId = _context.Identity.Id
            });

            if (attendances is null)
            {
                return NotFound();
            }

            return Ok(attendances);
        }

        [HttpPost("events/{eventId:guid}/attend")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> AttendAsync(Guid eventId)
        {
            await _commandDispatcher.SendAsync(new AttendEvent(eventId, _context.Identity.Id));
            return NoContent();
        }
    }
}