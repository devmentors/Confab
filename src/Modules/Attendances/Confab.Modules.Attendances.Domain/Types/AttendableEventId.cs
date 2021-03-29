using System;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Attendances.Domain.Types
{
    public class AttendableEventId : TypeId
    {
        public AttendableEventId(Guid value) : base(value)
        {
        }
        
        public static implicit operator AttendableEventId(Guid id) => new(id);

        public static implicit operator Guid(AttendableEventId id) => id.Value;
    }
}