using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Services.Tickets.Core.Events
{
    internal record TicketPurchased(Guid TicketId, Guid ConferenceId, Guid UserId) : IEvent;
}