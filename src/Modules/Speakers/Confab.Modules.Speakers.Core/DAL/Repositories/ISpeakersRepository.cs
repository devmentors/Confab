using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.Entities;

namespace Confab.Modules.Speakers.Core.DAL.Repositories
{
    public interface ISpeakersRepository
    {
        Task<IReadOnlyList<Speaker>> BrowseAsync();
        Task<Speaker> GetAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task AddAsync(Speaker speaker);
        Task UpdateAsync(Speaker speaker);
    }
}