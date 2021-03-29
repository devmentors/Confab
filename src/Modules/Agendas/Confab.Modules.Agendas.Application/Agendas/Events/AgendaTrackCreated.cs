using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Agendas.Events
{
    public record AgendaTrackCreated(Guid Id) : IEvent;
}