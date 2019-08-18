using System;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace Microsoft.Azure.WebJobs.Extensions.Discord.IncomingMessage
{
    internal class MessageValueProvider : IValueProvider
    {
        private readonly MessageCreateEventArgs value;

        public MessageValueProvider(object value)
        {
            this.value = value as MessageCreateEventArgs;
        }

        public Task<object> GetValueAsync() => Task.FromResult<object>(value);

        public string ToInvokeString() => value.ToString();

        public Type Type { get; } = typeof(MessageCreateEventArgs);
    }
}