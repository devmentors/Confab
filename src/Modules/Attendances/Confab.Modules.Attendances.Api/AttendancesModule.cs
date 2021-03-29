using Confab.Modules.Attendances.Application;
using Confab.Modules.Attendances.Domain;
using Confab.Modules.Attendances.Infrastructure;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Attendances.Api
{
    internal class AttendancesModule : IModule
    {
        public const string BasePath = "attendances-module";        
        public string Name { get; } = "Attendances";
        public string Path => BasePath;

        public void Register(IServiceCollection services)
        {
            services.AddDomain()
                .AddApplication()
                .AddInfrastructure();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}