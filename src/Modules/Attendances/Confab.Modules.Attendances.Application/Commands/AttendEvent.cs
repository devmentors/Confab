using System;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Attendances.Application.Commands
{
    public record AttendEvent(Guid Id, Guid ParticipantId) : ICommand;
}