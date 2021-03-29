using System;

namespace Confab.Modules.Agendas.Application.CallForPapers.DTO
{
    public class CallForPapersDto
    {
        public Guid Id { get; set; }
        public Guid ConferenceId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsOpened { get; set; }
    }
}