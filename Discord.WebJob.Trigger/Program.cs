using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using Microsoft.Azure.WebJobs.Extensions.Discord.IncomingMessage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Discord.WebJob.Trigger
{
    public class Program
    {
        public static async Task Main()
        {
            var host = new HostBuilder()
                .ConfigureWebJobs(b => b.AddDiscord())
                .ConfigureLogging(b => b.AddConsole())
                .Build();

            using (host)
                await host.RunAsync();
        }
    }

    public class Pingers
    {
        public async Task Ping([DiscordMessageTrigger("%token%")] MessageCreateEventArgs args)
        {
            if (args.Message.Content.ToLower().StartsWith("ping"))
                await args.Message.RespondAsync("pong!");
        }
        
        public async Task Hell([DiscordMessageTrigger("%token%")] MessageCreateEventArgs args)
        {
            if (args.Message.Content.ToLower().StartsWith("hell"))
                await args.Message.RespondAsync("yeah!");
        }
    }
}
