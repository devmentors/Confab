using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Application.Agendas.DTO;
using Confab.Modules.Agendas.Application.Agendas.Types;
using Confab.Modules.Agendas.Domain.Agendas.Entities;

namespace Confab.Modules.Agendas.Infrastructure.EF.Mappings
{
    internal static class AgendasExtensions
    {
        public static AgendaItemDto AsDto(this AgendaItem agendaItem)
            => new()
            {
                Id = agendaItem.Id,
                ConferenceId = agendaItem.ConferenceId,
                Title = agendaItem.Title,
                Description = agendaItem.Description,
                Level = agendaItem.Level,
                Tags = agendaItem.Tags,
                Speakers = agendaItem.Speakers.Select(s => new SpeakerDto
                {
                    Id = s.Id,
                    FullName = s.FullName
                })
            };

        public static AgendaTrackDto AsDto(this AgendaTrack agendaTrack)
        {
            var agendaTrackDto = new AgendaTrackDto
            {
                Id = agendaTrack.Id,
                ConferenceId = agendaTrack.ConferenceId,
                Name = agendaTrack.Name
            };

            var regularSlots = agendaTrack.Slots.OfType<RegularAgendaSlot>()
                .Select(rs => (object) rs.AsDto());
            
            var placeholderSlots = agendaTrack.Slots.OfType<PlaceholderAgendaSlot>()
                .Select(ps => (object) new PlaceholderAgendaSlotDto
                {
                    Id = ps.Id,
                    From = ps.From,
                    To = ps.To,
                    Type = AgendaSlotType.Placeholder,
                    Placeholder = ps.Placeholder
                });

            var slots = new List<object>();
            slots.AddRange(regularSlots);
            slots.AddRange(placeholderSlots);

            agendaTrackDto.Slots = slots;

            return agendaTrackDto;
        }

        public static RegularAgendaSlotDto AsDto(this RegularAgendaSlot slot)
            => new()
            {
                Id = slot.Id,
                From = slot.To,
                To = slot.To,
                Type = AgendaSlotType.Regular,
                ParticipantsLimit = slot.ParticipantsLimit,
                AgendaItem = new AgendaItemDto
                {
                    Id = slot.AgendaItem.Id,
                    ConferenceId = slot.AgendaItem.ConferenceId,
                    Title = slot.AgendaItem.Title,
                    Description = slot.AgendaItem.Description,
                    Level = slot.AgendaItem.Level,
                    Tags = slot.AgendaItem.Tags,
                    Speakers = slot.AgendaItem.Speakers.Select(s => new SpeakerDto
                    {
                        Id = s.Id,
                        FullName = s.FullName
                    })
                }
            };
    }
}