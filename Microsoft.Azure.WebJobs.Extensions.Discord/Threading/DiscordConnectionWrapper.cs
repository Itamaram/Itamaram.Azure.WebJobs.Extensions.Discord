using System;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;

namespace Microsoft.Azure.WebJobs.Extensions.Discord.Threading
{
    public class DiscordConnectionWrapper : IDisposable
    {
        public DiscordClient Client { get; }
        public bool IsConnected { get; set; }

        public DiscordConnectionWrapper(DiscordClient client)
        {
            Client = client;
        }

        private readonly SemaphoreSlim mutex = new SemaphoreSlim(1, 1);

        public async Task Connect()
        {
            using (await mutex.EnterAsync())
            {
                if (IsConnected)
                    return;

                await Client.ConnectAsync();
                IsConnected = true;
            }
        }

        public async Task Disconnect()
        {
            using (await mutex.EnterAsync())
            {
                if (!IsConnected)
                    return;

                await Client.DisconnectAsync();
                IsConnected = false;
            }
        }

        public void Dispose()
        {
            mutex?.Dispose();
            Client?.Dispose();
        }
    }
}