using System;

namespace Confab.Modules.Attendances.Application.DTO
{
    public class AttendanceDto
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid ConferenceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}