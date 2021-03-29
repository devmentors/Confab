using Confab.Modules.Attendances.Domain.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Attendances.Domain
{
    public static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<ISlotPolicyFactory, SlotPolicyFactory>();
            return services;
        }
    }
}