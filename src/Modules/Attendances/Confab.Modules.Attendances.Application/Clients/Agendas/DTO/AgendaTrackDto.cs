using System;
using System.Collections.Generic;

namespace Confab.Modules.Attendances.Application.Clients.Agendas.DTO
{
    public class AgendaTrackDto
    {
        public Guid Id { get; set; }
        public Guid ConferenceId { get; set; }
        public string Name { get; set; }
        public IEnumerable<AgendaSlotDto> Slots { get; set; }
    }
}