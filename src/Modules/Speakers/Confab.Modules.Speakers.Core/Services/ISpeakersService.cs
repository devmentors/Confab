using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DTO;

namespace Confab.Modules.Speakers.Core.Services
{
    public interface ISpeakersService
    {
        Task<IEnumerable<SpeakerDto>> BrowseAsync();
        Task<SpeakerDto> GetAsync(Guid speakerId);
        Task CreateAsync(SpeakerDto speaker);
        Task UpdateAsync(SpeakerDto speaker);
    }
}