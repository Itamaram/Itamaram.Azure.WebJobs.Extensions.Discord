# Itamaram.Azure.WebJobs.Extensions.Discord
A custom Azure WebJob input binding trigger for Discord

# Usage:

* Create a new WebJob project
* Install the nuget package `Itamaram.Azure.WebJobs.Extensions.Discord`
* Ensure your configuration scope includes a variable named `token` containing your bot's token
* Call `AddDiscord` in your `ConfigureWebJobs`
* Get triggered

# Sample Code

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
