using System;

namespace Confab.Modules.Attendances.Infrastructure.Clients.Requests
{
    internal class GetAgenda
    {
        public Guid ConferenceId { get; set; }
    }
}