using System;

namespace Confab.Modules.Speakers.Core.Entities
{
    public class Speaker
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public string AvatarUrl { get; set; }
    }
}