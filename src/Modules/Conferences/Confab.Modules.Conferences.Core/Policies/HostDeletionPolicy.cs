using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Policies
{
    internal class HostDeletionPolicy : IHostDeletionPolicy
    {
        private readonly IConferenceDeletionPolicy _conferenceDeletionPolicy;

        public HostDeletionPolicy(IConferenceDeletionPolicy conferenceDeletionPolicy)
        {
            _conferenceDeletionPolicy = conferenceDeletionPolicy;
        }
        
        public async Task<bool> CanDeleteAsync(Host host)
        {
            if (host.Conferences is null || !host.Conferences.Any())
            {
                return true;
            }

            foreach (var conference in host.Conferences)
            {
                if (await _conferenceDeletionPolicy.CanDeleteAsync(conference) is false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}