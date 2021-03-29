using Confab.Modules.Agendas.Domain.Agendas.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Agendas.Domain
{
    public static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
            => services.AddScoped<IAgendaTracksDomainService, AgendaTracksDomainService>();
    }
}