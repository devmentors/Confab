using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.CallForPapers.Events;
using Confab.Modules.Agendas.Application.CallForPapers.Exceptions;
using Confab.Modules.Agendas.Domain.CallForPapers.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.CallForPapers.Commands.Handlers
{
    public sealed class OpenCallForPapersHandler : ICommandHandler<OpenCallForPapers>
    {
        private readonly ICallForPapersRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public OpenCallForPapersHandler(ICallForPapersRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        
        public async Task HandleAsync(OpenCallForPapers command)
        {
            var callForPapers = await _repository.GetAsync(command.ConferenceId);
            
            if (callForPapers is null)
            {
                throw new CallForPapersNotFoundException(command.ConferenceId);
            }
            
            callForPapers.Open();
            await _repository.UpdateAsync(callForPapers);
            await _messageBroker.PublishAsync(new CallForPapersOpened(callForPapers.ConferenceId, 
                callForPapers.From, callForPapers.To));
        }
    }
}