using System;
using Confab.Modules.Agendas.Application.Agendas.DTO;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Agendas.Queries
{
    public class GetAgendaItem : IQuery<AgendaItemDto>
    {
        public Guid Id { get; set; }
    }
}