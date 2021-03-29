using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Services;
using Confab.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Speakers.Api.Controllers
{
    internal class SpeakersController : BaseController
    {
        private const string Policy = "speakers";
        private readonly ISpeakersService _speakersService;
        private readonly IContext _context;

        public SpeakersController(ISpeakersService service, IContext context)
        {
            _speakersService = service;
            _context = context;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<SpeakerDto>> Get(Guid id) =>  OkOrNotFound(await _speakersService.GetAsync(id));

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SpeakerDto>>> Get() => Ok(await _speakersService.BrowseAsync());

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post(SpeakerDto speaker)
        {
            speaker.Id = _context.Identity.Id;
            await _speakersService.CreateAsync(speaker);
            return CreatedAtAction(nameof(Get), new {id = speaker.Id}, null);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(SpeakerDto speaker)
        {
            speaker.Id = _context.Identity.Id;
            await _speakersService.UpdateAsync(speaker);
            return NoContent();
        }
    }
}