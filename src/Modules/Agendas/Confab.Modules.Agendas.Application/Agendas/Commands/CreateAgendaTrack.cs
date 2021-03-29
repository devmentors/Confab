using System;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Agendas.Commands
{
    public record CreateAgendaTrack(Guid ConferenceId, string Name) : ICommand
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}