using System;
using Confab.Modules.Attendances.Domain.Types;

namespace Confab.Modules.Attendances.Domain.Entities
{
    public record Attendance(Guid Id, AttendableEventId AttendableEventId, SlotId SlotId, ParticipantId ParticipantId,
        DateTime From, DateTime To);
}