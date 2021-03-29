namespace Confab.Modules.Agendas.Application.Agendas.DTO
{
    public class RegularAgendaSlotDto : AgendaSlotDto
    {
        public int? ParticipantsLimit { get; set; }
        public AgendaItemDto AgendaItem { get; set; }
    }
}