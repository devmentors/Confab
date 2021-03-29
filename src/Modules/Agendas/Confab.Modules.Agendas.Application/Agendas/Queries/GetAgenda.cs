using System;
using System.Collections.Generic;
using Confab.Modules.Agendas.Application.Agendas.DTO;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Agendas.Queries
{
    public class GetAgenda : IQuery<IEnumerable<AgendaTrackDto>>
    {
        public Guid ConferenceId { get; set; }
    }
}