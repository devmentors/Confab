using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Tickets.Core.Events
{
    internal record TicketPurchased(Guid TicketId, Guid ConferenceId, Guid UserId) : IEvent;
}