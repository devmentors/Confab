using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.CallForPapers.Events
{
    internal record CallForPapersCreated(Guid ConferenceId) : IEvent;
}