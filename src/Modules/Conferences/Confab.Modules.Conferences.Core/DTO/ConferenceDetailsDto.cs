using System.ComponentModel.DataAnnotations;

namespace Confab.Modules.Conferences.Core.DTO
{
    internal class ConferenceDetailsDto : ConferenceDto
    {
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Description { get; set; }
    }
}