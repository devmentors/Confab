namespace Confab.Modules.Attendances.Application.Clients.Agendas.DTO
{
    public class RegularAgendaSlotDto : AgendaSlotDto
    {
        public int? ParticipantsLimit { get; set; }
        public AgendaItemDto AgendaItem { get; set; }
    }
}