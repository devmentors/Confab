using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Saga.Messages
{
    internal record SignedIn(Guid UserId) : IEvent;
}