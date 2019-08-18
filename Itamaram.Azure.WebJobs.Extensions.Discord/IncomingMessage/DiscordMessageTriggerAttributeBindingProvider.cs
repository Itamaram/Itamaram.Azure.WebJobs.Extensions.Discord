using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Host.Triggers;

namespace Microsoft.Azure.WebJobs.Extensions.Discord.IncomingMessage
{
    internal class DiscordMessageTriggerAttributeBindingProvider : ITriggerBindingProvider
    {
        private readonly INameResolver resolver;

        public DiscordMessageTriggerAttributeBindingProvider(INameResolver resolver)
        {
            this.resolver = resolver;
        }

        public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
        {
            var attribute = context.Parameter.GetCustomAttribute<DiscordMessageTriggerAttribute>();

            if (attribute == null)
                return Task.FromResult<ITriggerBinding>(null);

            var binding = new DiscordMessageTriggerBinding(context.Parameter.Name, resolver.ResolveWholeString(attribute.Token));

            return Task.FromResult<ITriggerBinding>(binding);
        }
    }
}