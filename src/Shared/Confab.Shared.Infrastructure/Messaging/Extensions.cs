using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Infrastructure.Messaging.Brokers;
using Confab.Shared.Infrastructure.Messaging.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Messaging
{
    internal static class Extensions
    {
        private const string SectionName = "messaging";
        
        internal static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBroker, MessageBroker>();
            services.AddSingleton<IMessageChannel, MessageChannel>();
            services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();

            var messagingOptions = services.GetOptions<MessagingOptions>(SectionName);
            services.AddSingleton(messagingOptions);

            if (messagingOptions.UseBackgroundDispatcher)
            {
                services.AddHostedService<BackgroundDispatcher>();
            }
            
            return services;
        }
    }
}