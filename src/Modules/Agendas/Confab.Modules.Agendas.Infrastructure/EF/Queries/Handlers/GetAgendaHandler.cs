using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.DTO;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Infrastructure.EF.Mappings;
using Confab.Shared.Abstractions.Queries;
using Confab.Shared.Abstractions.Storage;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class GetAgendaHandler : IQueryHandler<GetAgenda, IEnumerable<AgendaTrackDto>>
    {
        private readonly DbSet<AgendaTrack> _agendaTracks;
        private readonly IRequestStorage _requestStorage;

        public GetAgendaHandler(AgendasDbContext context, IRequestStorage requestStorage)
        {
            _agendaTracks = context.AgendaTracks;
            _requestStorage = requestStorage;
        }

        public async Task<IEnumerable<AgendaTrackDto>> HandleAsync(GetAgenda query)
        {
            var dtos = _requestStorage.Get<IEnumerable<AgendaTrackDto>>(GetStorageKey(query.ConferenceId));
            if (dtos is not null)
            {
                return dtos;
            }

            var agendaTracks = await _agendaTracks
                .Include(at => at.Slots)
                .ThenInclude(at => (at as RegularAgendaSlot).AgendaItem)
                .ThenInclude(ai => ai.Speakers)
                .Where(at => at.ConferenceId == query.ConferenceId)
                .ToListAsync();

            dtos = agendaTracks?.Select(at => at.AsDto());
            _requestStorage.Set(GetStorageKey(query.ConferenceId), dtos, TimeSpan.FromMinutes(5));

            return dtos;
        }

        private static string GetStorageKey(Guid conferenceId) => $"agenda/{conferenceId}";
    }
}