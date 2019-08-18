using System;
using Microsoft.Azure.WebJobs.Description;

namespace Microsoft.Azure.WebJobs.Extensions.Discord.IncomingMessage
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class DiscordMessageTriggerAttribute : Attribute
    {
        public string Token { get; }

        public DiscordMessageTriggerAttribute(string token)
        {
            Token = token;
        }
    }
}