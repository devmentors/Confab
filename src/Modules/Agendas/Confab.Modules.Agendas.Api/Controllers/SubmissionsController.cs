using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Commands;
using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    internal class SubmissionsController : BaseController
    {
        private const string Policy = "submissions";
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public SubmissionsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [Authorize(Policy)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SubmissionDto>> GetAsync(Guid id)
            => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetSubmission {Id = id}));
        
        [Authorize(Policy)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubmissionDto>>> BrowseAsync([FromQuery] BrowseSubmissions query) 
            => Ok(await _queryDispatcher.QueryAsync(query));

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateAsync(CreateSubmission command)
        {
            await _commandDispatcher.SendAsync(command);
            AddResourceIdHeader(command.Id);
            return CreatedAtAction("Get", new {id = command.Id}, null);
        }
        
        [Authorize(Policy)]
        [HttpPut("{id:guid}/approve")]
        public async Task<ActionResult> ApproveAsync(Guid id)
        {
            await _commandDispatcher.SendAsync(new ApproveSubmission(id));
            return NoContent();
        }
        
        [Authorize(Policy)]
        [HttpPut("{id:guid}/reject")]
        public async Task<ActionResult> RejectAsync(Guid id)
        {
            await _commandDispatcher.SendAsync(new RejectSubmission(id));
            return NoContent();
        }
    }
}