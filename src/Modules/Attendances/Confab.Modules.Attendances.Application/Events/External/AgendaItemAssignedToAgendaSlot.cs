using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Attendances.Application.Events.External
{
    internal record AgendaItemAssignedToAgendaSlot(Guid Id, Guid AgendaItemId) : IEvent;
}