using System.Collections.Generic;
using Confab.Modules.Attendances.Domain.Entities;

namespace Confab.Modules.Attendances.Domain.Policies
{
    public interface ISlotPolicy
    {
        IEnumerable<Slot> Generate(int participantsLimit);
    }
}