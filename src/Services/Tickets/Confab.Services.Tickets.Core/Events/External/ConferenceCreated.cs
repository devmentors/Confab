using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Confab.Services.Tickets.Core.Events.External
{
    [Message("modular-monolith")]
    public record ConferenceCreated(Guid Id, string Name, int? ParticipantsLimit, DateTime From, DateTime To) : IEvent;
}