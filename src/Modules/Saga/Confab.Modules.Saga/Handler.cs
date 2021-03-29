using System.Threading.Tasks;
using Chronicle;
using Confab.Modules.Saga.Messages;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Saga
{
    internal class Handler : IEventHandler<SpeakerCreated>, IEventHandler<SignedUp>, IEventHandler<SignedIn>
    {
        private readonly ISagaCoordinator _coordinator;

        public Handler(ISagaCoordinator coordinator)
            => _coordinator = coordinator;

        public Task HandleAsync(SpeakerCreated @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);

        public Task HandleAsync(SignedUp @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);

        public Task HandleAsync(SignedIn @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);
    }
}