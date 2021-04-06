using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Infrastructure.Messaging.Dispatchers;
using Convey.MessageBrokers;

namespace Confab.Shared.Infrastructure.Messaging.Brokers
{
    internal sealed class MessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleClient;
        private readonly IBusPublisher _busPublisher;
        private readonly IAsyncMessageDispatcher _asyncMessageDispatcher;
        private readonly MessagingOptions _messagingOptions;

        public MessageBroker(IModuleClient moduleClient, IBusPublisher busPublisher,
            IAsyncMessageDispatcher asyncMessageDispatcher, MessagingOptions messagingOptions)
        {
            _moduleClient = moduleClient;
            _busPublisher = busPublisher;
            _asyncMessageDispatcher = asyncMessageDispatcher;
            _messagingOptions = messagingOptions;
        }

        public async Task PublishAsync(params IMessage[] messages)
        {
            if (messages is null)
            {
                return;
            }

            messages = messages.Where(x => x is not null).ToArray();

            if (!messages.Any())
            {
                return;
            }

            var tasks = new List<Task>();
            
            foreach (var message in messages)
            {
                await _busPublisher.PublishAsync(message); // External RabbitMQ message broker
                if (_messagingOptions.UseBackgroundDispatcher)
                {
                    await _asyncMessageDispatcher.PublishAsync(message);
                    continue;
                }
                
                tasks.Add(_moduleClient.PublishAsync(message));
            }

            await Task.WhenAll(tasks);
        }
    }
}