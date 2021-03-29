using System;

namespace Confab.Shared.Abstractions.Time
{
    public interface IClock
    {
        DateTime CurrentDate();
    }
}