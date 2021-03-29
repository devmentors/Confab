using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Saga.Messages
{
    internal record SignedUp(Guid UserId, string Email) : IEvent;
}