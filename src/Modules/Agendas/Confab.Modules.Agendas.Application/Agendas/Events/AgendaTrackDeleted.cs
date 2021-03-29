using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Agendas.Events
{
    public record AgendaTrackDeleted(Guid Id) : IEvent;
}