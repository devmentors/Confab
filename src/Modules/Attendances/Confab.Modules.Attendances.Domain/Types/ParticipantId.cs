using System;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Attendances.Domain.Types
{
    public class ParticipantId : TypeId
    {
        public ParticipantId(Guid value) : base(value)
        {
        }

        public static implicit operator ParticipantId(Guid id) => new(id);
        
        public static implicit operator Guid(ParticipantId id) => id.Value;
    }
}