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
    internal sealed class ApproveSubmissionHandler : ICommandHandler<ApproveSubmission>
    {
        private readonly ISubmissionRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;
        private readonly IDomainEventDispatcher _dispatcher;

        public ApproveSubmissionHandler(ISubmissionRepository repository, IMessageBroker messageBroker, 
            IEventMapper eventMapper, IDomainEventDispatcher dispatcher)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
            _dispatcher = dispatcher;
        }

        public async Task HandleAsync(ApproveSubmission command)
        {
            var submission = await _repository.GetAsync(command.Id);

            if (submission is null)
            {
                throw new SubmissionNotFoundException(command.Id);
            }
            
            submission.Approve();
            
            await _repository.UpdateAsync(submission);
            await _dispatcher.DispatchAsync(submission.Events.ToArray());
            
            var integrationEvents = _eventMapper.MapAll(submission.Events);
            await _messageBroker.PublishAsync(integrationEvents.ToArray());
        }
    }
}