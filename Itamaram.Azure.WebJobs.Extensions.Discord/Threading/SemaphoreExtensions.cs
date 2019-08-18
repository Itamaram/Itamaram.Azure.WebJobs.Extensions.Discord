using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Azure.WebJobs.Extensions.Discord.Threading
{
    internal static class SemaphoreExtensions
    {
        public static async Task<IDisposable> EnterAsync(this SemaphoreSlim semaphore)
        {
            await semaphore.WaitAsync();
            return new SemaphoreScope(semaphore);
        }

        public static IDisposable Enter(this SemaphoreSlim semaphore)
        {
            semaphore.Wait();
            return new SemaphoreScope(semaphore);
        }
    }
}