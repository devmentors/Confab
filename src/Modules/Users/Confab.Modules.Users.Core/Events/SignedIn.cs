using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Users.Core.Events
{
    internal record SignedIn(Guid UserId) : IEvent;
}