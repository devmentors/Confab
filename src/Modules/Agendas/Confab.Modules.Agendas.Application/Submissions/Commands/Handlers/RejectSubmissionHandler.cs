using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Application.Submissions.Services;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    internal sealed class RejectSubmissionHandler : ICommandHandler<RejectSubmission>
    {
        private readonly ISubmissionRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IEventMapper _eventMapper;

        public RejectSubmissionHandler(ISubmissionRepository repository, IMessageBroker messageBroker, 
            IDomainEventDispatcher dispatcher, IEventMapper eventMapper)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _dispatcher = dispatcher;
            _eventMapper = eventMapper;
        }

        public async Task HandleAsync(RejectSubmission command)
        {
            var submission = await _repository.GetAsync(command.Id);

            if (submission is null)
            {
                throw new SubmissionNotFoundException(command.Id);
            }
            
            submission.Reject();
            
            await _repository.UpdateAsync(submission);
            await _dispatcher.DispatchAsync(submission.Events.ToArray());
            
            var integrationEvents = _eventMapper.MapAll(submission.Events);
            await _messageBroker.PublishAsync(integrationEvents.ToArray());
        }
    }
}