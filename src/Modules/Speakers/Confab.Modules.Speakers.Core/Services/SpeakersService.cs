using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DAL.Repositories;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Events;
using Confab.Modules.Speakers.Core.Exceptions;
using Confab.Modules.Speakers.Core.Mappings;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Speakers.Core.Services
{
    internal sealed class SpeakersService : ISpeakersService
    {
        private readonly ISpeakersRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public SpeakersService(ISpeakersRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        public async Task<IEnumerable<SpeakerDto>> BrowseAsync()
        {
            var entities = await _repository.BrowseAsync();
            return entities?.Select(e => e.AsDto());
        }

        public async Task<SpeakerDto> GetAsync(Guid speakerId)
        {
            var entity = await _repository.GetAsync(speakerId);
            return entity?.AsDto();
        }

        public async Task CreateAsync(SpeakerDto speaker)
        {
            var alreadyExists = await _repository.ExistsAsync(speaker.Id);
            if (alreadyExists)
            {
                throw new SpeakerAlreadyExistsException(speaker.Id);
            }

            await _repository.AddAsync(speaker.AsEntity());
            await _messageBroker.PublishAsync(new SpeakerCreated(speaker.Id, speaker.FullName));
        }

        public async Task UpdateAsync(SpeakerDto speaker)
        {
            var exists = await _repository.ExistsAsync(speaker.Id);

            if (!exists)
            {
                throw new SpeakerNotFoundException(speaker.Id);
            }
            
            await _repository.UpdateAsync(speaker.AsEntity());
        }
    }
}