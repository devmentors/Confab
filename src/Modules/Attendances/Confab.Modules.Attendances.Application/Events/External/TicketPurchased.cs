using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Attendances.Application.Events.External
{
    internal record TicketPurchased(Guid TicketId, Guid ConferenceId, Guid UserId) : IEvent;
}