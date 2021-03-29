using System;
using Confab.Shared.Abstractions.Time;

namespace Confab.Shared.Infrastructure.Time
{
    internal class UtcClock : IClock
    {
        public DateTime CurrentDate() => DateTime.UtcNow;
    }
}