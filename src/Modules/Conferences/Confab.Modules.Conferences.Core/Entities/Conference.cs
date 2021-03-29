using System;

namespace Confab.Modules.Conferences.Core.Entities
{
    public class Conference
    {
        public Guid Id { get; set; }
        public Guid HostId { get; set; }
        public Host Host { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string LogoUrl { get; set; }
        public int? ParticipantsLimit { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}