using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Modules.Agendas.Domain.Submissions.Consts;
using Confab.Modules.Agendas.Domain.Submissions.Events;                                                   
using Confab.Shared.Abstractions.Kernel;

namespace Confab.Modules.Agendas.Domain.Agendas.Events.Handlers
{
    internal sealed class SubmissionApprovedHandler : IDomainEventHandler<SubmissionStatusChanged>
    {
        private readonly IAgendaItemsRepository _agendaItemsRepository;

        public SubmissionApprovedHandler(IAgendaItemsRepository agendaItemsRepository)
            => _agendaItemsRepository = agendaItemsRepository;

        public async Task HandleAsync(SubmissionStatusChanged domainEvent)
        {
            if (domainEvent.Status is SubmissionStatus.Rejected)
            {
                return;
            }

            var submission = domainEvent.Submission;
            var agendaItem = await _agendaItemsRepository.GetAsync(submission.Id);
            if (agendaItem is not null)
            {
                return;
            }

            agendaItem = AgendaItem.Create(submission.Id, submission.ConferenceId, submission.Title,
                submission.Description, submission.Level, submission.Tags, submission.Speakers.ToList());

            await _agendaItemsRepository.AddAsync(agendaItem);
        }
    }
}