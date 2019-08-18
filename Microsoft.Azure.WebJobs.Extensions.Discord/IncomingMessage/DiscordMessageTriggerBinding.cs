using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;

namespace Microsoft.Azure.WebJobs.Extensions.Discord.IncomingMessage
{
    internal class DiscordMessageTriggerBinding : ITriggerBinding
    {
        private readonly string parameterName;
        private readonly string token;

        public DiscordMessageTriggerBinding(string parameterName, string token)
        {
            this.parameterName = parameterName;
            this.token = token;
        }

        public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
        {
            var trigger = new TriggerData(new MessageValueProvider(value), new Dictionary<string, object>());
            return Task.FromResult<ITriggerData>(trigger);
        }

        public Task<IListener> CreateListenerAsync(ListenerFactoryContext context)
        {
            IListener listener = new DiscordMessageListener(token, context.Executor);
            return Task.FromResult(listener);
        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            return new DiscordMessageTriggerParameterDescriptor
            {
                Name = parameterName
            };
        }

        public Type TriggerValueType { get; } = typeof(MessageCreateEventArgs);

        public IReadOnlyDictionary<string, Type> BindingDataContract { get; } = new Dictionary<string, Type>();
    }
}