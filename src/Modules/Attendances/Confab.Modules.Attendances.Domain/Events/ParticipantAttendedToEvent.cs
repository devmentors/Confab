using Confab.Modules.Attendances.Domain.Entities;
using Confab.Shared.Abstractions.Kernel;

namespace Confab.Modules.Attendances.Domain.Events
{
    public record ParticipantAttendedToEvent(Participant Participant, Attendance Attendance) : IDomainEvent;
}