using System;
using System.ComponentModel.DataAnnotations;

namespace Confab.Modules.Conferences.Core.DTO
{
    internal class HostDto
    {
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        
        [StringLength(1000, MinimumLength = 3)]
        public string Description { get; set; }
    }
}