using Microsoft.Azure.WebJobs.Host.Config;

namespace Microsoft.Azure.WebJobs.Extensions.Discord.IncomingMessage
{
    internal class DiscordMessageExtensionConfigProvider : IExtensionConfigProvider
    {
        private readonly INameResolver resolver;

        public DiscordMessageExtensionConfigProvider(INameResolver resolver)
        {
            this.resolver = resolver;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            var trigger = new DiscordMessageTriggerAttributeBindingProvider(resolver);
            context.AddBindingRule<DiscordMessageTriggerAttribute>().BindToTrigger(trigger);
        }
    }
}