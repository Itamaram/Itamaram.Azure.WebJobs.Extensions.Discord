using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.Discord.Threading;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;

namespace Microsoft.Azure.WebJobs.Extensions.Discord.IncomingMessage
{
    internal class DiscordMessageListener : IListener
    {
        private readonly CancellationTokenSource source = new CancellationTokenSource();
        private readonly DiscordConnectionWrapper connection;

        public DiscordMessageListener(string token, ITriggeredFunctionExecutor exeggutor)
        {
            connection = DiscordConnectionManager.Instance.GetConnectionForToken(token);

            connection.Client.MessageCreated += e => exeggutor.TryExecuteAsync(new TriggeredFunctionData
            {
                TriggerValue = e,
                TriggerDetails = new Dictionary<string, string>
                {
                    ["MessageTimestamp"] = e.Message.Timestamp.ToString("O"),
                    ["Guild"] = e.Guild.Name,
                    ["Channel"] = e.Channel.Name,
                    ["Author"] = e.Author.Username,
                }
            }, source.Token);
        }

        public Task StartAsync(CancellationToken cancellationToken) => connection.Connect();

        public Task StopAsync(CancellationToken cancellationToken) => connection.Disconnect();

        public void Cancel() => source.Cancel();

        public void Dispose() => connection.Dispose();
    }
}