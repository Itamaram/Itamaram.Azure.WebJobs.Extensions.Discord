using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Discord.IncomingMessage;

// ReSharper disable once CheckNamespace - It's okay, all the cool kids do this
namespace Microsoft.Extensions.Hosting
{
    public static class DiscordWebJobsBuilderExtensions
    {
        public static IWebJobsBuilder AddDiscord(this IWebJobsBuilder builder)
        {
            builder.AddExtension<DiscordMessageExtensionConfigProvider>();
            return builder;
        }
    }
}