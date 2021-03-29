using System;
using System.Collections.Generic;

namespace Confab.Modules.Attendances.Application.Clients.Agendas.DTO
{
    public class AgendaItemDto
    {
        public Guid Id { get; set; }
        public Guid ConferenceId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}