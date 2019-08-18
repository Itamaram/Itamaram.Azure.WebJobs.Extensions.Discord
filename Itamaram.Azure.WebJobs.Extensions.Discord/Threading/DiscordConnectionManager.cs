using System;
using System.Collections.Generic;
using System.Threading;
using DSharpPlus;

namespace Microsoft.Azure.WebJobs.Extensions.Discord.Threading
{
    public class DiscordConnectionManager : IDisposable
    {
        private readonly Dictionary<string, DiscordConnectionWrapper> connections = new Dictionary<string, DiscordConnectionWrapper>();
        private readonly SemaphoreSlim mutex = new SemaphoreSlim(1, 1);

        private DiscordConnectionManager() { }

        public static DiscordConnectionManager Instance { get; } = new DiscordConnectionManager();

        public DiscordConnectionWrapper GetConnectionForToken(string token)
        {
            using (mutex.Enter())
            {
                if (connections.TryGetValue(token, out var connection))
                    return connection;

                return connections[token] = new DiscordConnectionWrapper(new DiscordClient(new DiscordConfiguration
                {
                    Token = token,
                    TokenType = TokenType.Bot,
                }));
            }
        }

        public void Dispose()
        {
            mutex?.Dispose();
        }
    }
}