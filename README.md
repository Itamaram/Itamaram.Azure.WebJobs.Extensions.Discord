# Itamaram.Azure.WebJobs.Extensions.Discord
A custom Azure WebJob input binding trigger for Discord

# Sample Usage:

Ensure your configuration scope includes a variable named `token` containing your bot's token, and then use the following code to create your webjob:

```csharp
  public class Program
  {
      public static async Task Main()
      {
          var host = new HostBuilder()
              .ConfigureWebJobs(b => b.AddDiscord())
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
```

(Full code sample is included in this repo)
