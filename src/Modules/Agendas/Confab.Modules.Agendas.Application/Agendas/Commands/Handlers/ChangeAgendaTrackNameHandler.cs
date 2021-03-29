using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Events;
using Confab.Modules.Agendas.Application.Agendas.Exceptions;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Agendas.Commands.Handlers
{
    internal sealed class ChangeAgendaTrackNameHandler : ICommandHandler<ChangeAgendaTrackName>
    {
        private readonly IAgendaTracksRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public ChangeAgendaTrackNameHandler(IAgendaTracksRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        
        public async Task HandleAsync(ChangeAgendaTrackName command)
        {
            var agendaTrack = await _repository.GetAsync(command.Id);

            if (agendaTrack is null)
            {
                throw new AgendaTrackNotFoundException(command.Id);
            }
            
            agendaTrack.ChangeName(command.Name);

            await _repository.UpdateAsync(agendaTrack);
            await _messageBroker.PublishAsync(new AgendaTrackUpdated(agendaTrack.Id));
        }
    }
}