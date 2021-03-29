using Confab.Modules.Agendas.Application.CallForPapers.DTO;
using Confab.Modules.Agendas.Domain.CallForPapers.Entities;

namespace Confab.Modules.Agendas.Infrastructure.EF.Mappings
{
    internal static class CallForPapersExtensions
    {
        public static CallForPapersDto AsDto(this CallForPapers dbModel)
            => new()
            {
                Id = dbModel.Id,
                ConferenceId = dbModel.ConferenceId,
                From = dbModel.From,
                To = dbModel.To,
                IsOpened = dbModel.IsOpened,
            };
    }
}