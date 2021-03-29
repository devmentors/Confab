using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Entities;

namespace Confab.Modules.Speakers.Core.Mappings
{
    internal static class Extensions
    {
        public static SpeakerDto AsDto(this Speaker entity)
            => new()
            {
                Id = entity.Id,
                Email = entity.Email,
                FullName = entity.FullName,
                Bio = entity.Bio,
                AvatarUrl = entity.AvatarUrl
            };
        
        public static Speaker AsEntity(this SpeakerDto dto)
            => new()
            {
                Id = dto.Id,
                Email = dto.Email,
                FullName = dto.FullName,
                Bio = dto.Bio,
                AvatarUrl = dto.AvatarUrl
            };
    }
}