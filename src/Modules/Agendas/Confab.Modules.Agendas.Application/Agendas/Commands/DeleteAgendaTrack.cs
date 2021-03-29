using System;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Agendas.Commands
{
    public record DeleteAgendaTrack(Guid Id) : ICommand;
}