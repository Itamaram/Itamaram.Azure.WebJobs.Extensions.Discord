using System;
using System.Threading;

namespace Microsoft.Azure.WebJobs.Extensions.Discord.Threading
{
    internal class SemaphoreScope : IDisposable
    {
        private readonly SemaphoreSlim semaphore;

        public SemaphoreScope(SemaphoreSlim semaphore)
        {
            this.semaphore = semaphore;
        }

        public void Dispose() => semaphore.Release();
    }
}