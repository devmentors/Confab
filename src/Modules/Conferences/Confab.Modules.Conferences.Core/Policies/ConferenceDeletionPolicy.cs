using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Shared.Abstractions.Time;

namespace Confab.Modules.Conferences.Core.Policies
{
    internal class ConferenceDeletionPolicy : IConferenceDeletionPolicy
    {
        private readonly IClock _clock;

        public ConferenceDeletionPolicy(IClock clock)
        {
            _clock = clock;
        }

        public Task<bool> CanDeleteAsync(Conference conference)
        {
            // TODO: Check if there are any participants?
            var canDelete = _clock.CurrentDate().Date.AddDays(7) < conference.From.Date;

            return Task.FromResult(canDelete);
        }
    }
}