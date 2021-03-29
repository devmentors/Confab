using System;

namespace Confab.Shared.Abstractions.Kernel.Types
{
    public class SpeakerId : TypeId
    {
        public SpeakerId(Guid value) : base(value)
        {
        }

        public static implicit operator SpeakerId(Guid id)
            => new(id);
    }
}