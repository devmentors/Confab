using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Users.Core.Events
{
    internal record SignedUp(Guid UserId, string Email) : IEvent;
}