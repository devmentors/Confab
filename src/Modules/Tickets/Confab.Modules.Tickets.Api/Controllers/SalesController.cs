using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.DTO;
using Confab.Modules.Tickets.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Tickets.Api.Controllers
{
    internal class SalesController : BaseController
    {
        private readonly ITicketSaleService _ticketSaleService;
        private const string Policy = "tickets";

        public SalesController(ITicketSaleService ticketSaleService)
        {
            _ticketSaleService = ticketSaleService;
        }
        
        [HttpGet("conferences/{conferenceId}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<TicketSaleInfoDto>>> GetAll(Guid conferenceId)
            => OkOrNotFound(await _ticketSaleService.GetAllAsync(conferenceId));

        [HttpGet("conferences/{conferenceId}/current")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<TicketSaleInfoDto>> GetCurrent(Guid conferenceId)
            => OkOrNotFound(await _ticketSaleService.GetCurrentAsync(conferenceId));

        [Authorize(Policy)]
        [HttpPost("conferences/{conferenceId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult> Post(Guid conferenceId, TicketSaleDto dto)
        {
            dto.ConferenceId = conferenceId;
            await _ticketSaleService.AddAsync(dto);
            return NoContent();
        }
    }
}