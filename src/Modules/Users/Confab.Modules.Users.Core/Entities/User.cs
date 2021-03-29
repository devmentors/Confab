using System;
using System.Collections.Generic;

namespace Confab.Modules.Users.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public Dictionary<string, IEnumerable<string>> Claims { get; set; }
    }
}