using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.CallForPapers.Exceptions;
using Confab.Modules.Agendas.Application.Submissions.Services;
using Confab.Modules.Agendas.Domain.CallForPapers.Repositories;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel;
using Confab.Shared.Abstractions.Kernel.Types;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    internal sealed class CreateSubmissionHandler : ICommandHandler<CreateSubmission>
    {
        private readonly ISpeakerRepository _speakerRepository;
        private readonly ISubmissionRepository _submissionRepository;
        private readonly ICallForPapersRepository _callForPapersRepository;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;

        public CreateSubmissionHandler(ISpeakerRepository speakerRepository, ISubmissionRepository submissionRepository,
            ICallForPapersRepository callForPapersRepository, IDomainEventDispatcher dispatcher,
            IMessageBroker messageBroker, IEventMapper eventMapper)
        {
            _speakerRepository = speakerRepository;
            _submissionRepository = submissionRepository;
            _callForPapersRepository = callForPapersRepository;
            _dispatcher = dispatcher;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
        }

        public async Task HandleAsync(CreateSubmission command)
        {
            var callForPapers = await _callForPapersRepository.GetAsync(command.ConferenceId);
            if (callForPapers is null)
            {
                throw new CallForPapersNotFoundException(command.ConferenceId);
            }

            if (!callForPapers.IsOpened)
            {
                throw new CallForPapersClosedException(command.ConferenceId);
            }
            
            var speakerIds = command.SpeakerIds.Select(id => new AggregateId(id));
            var speakers = await _speakerRepository.BrowseAsync(speakerIds);

            if (speakers.Count() != command.SpeakerIds.Count())
            {
                throw new MissingSubmissionSpeakersException(command.Id);
            }
            
            var submission = Submission.Create(command.Id, command.ConferenceId, command.Title, command.Description, 
                command.Level, command.Tags, speakers);
            
            await _submissionRepository.AddAsync(submission);
            await _dispatcher.DispatchAsync(submission.Events.ToArray());
            
            var integrationEvents = _eventMapper.MapAll(submission.Events);
            await _messageBroker.PublishAsync(integrationEvents.ToArray());
        }
    }
}