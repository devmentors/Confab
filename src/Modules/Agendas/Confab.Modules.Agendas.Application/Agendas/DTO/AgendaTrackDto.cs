using System;
using System.Collections.Generic;

namespace Confab.Modules.Agendas.Application.Agendas.DTO
{
    public class AgendaTrackDto
    {
        public Guid Id { get; set; }
        public Guid ConferenceId { get; set; }
        public string Name { get; set; }
        public IEnumerable<object> Slots { get; set; }
    }
}