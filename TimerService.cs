namespace Wordle.NET.Models
{
    public class TimerService(NotifierService notifier,
        ILogger<TimerService> logger) : IDisposable
    {
        private int elapsedCount;
        private readonly static TimeSpan heartbeatTickRate = TimeSpan.FromSeconds(5);
        private readonly ILogger<TimerService> logger = logger;
        private readonly NotifierService notifier = notifier;
        private PeriodicTimer? timer;
        public void Stop()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
                logger.LogInformation("Stopped");
            }
        }

        public async Task Start(TimeSpan? tickInterval, bool oneTime = true)
        {
            if (timer is null)
            {
                timer = new(tickInterval ?? heartbeatTickRate);
                logger.LogInformation("Started");

                using (timer)
                {
                    while (await timer.WaitForNextTickAsync())
                    {
                        elapsedCount += 1;
                        await notifier.Update("elapsedCount", elapsedCount);
                        logger.LogInformation("ElapsedCount {Count}", elapsedCount);
                    }
                    logger.LogInformation("sjalsdjads");
                }
            }
        }

        public void Dispose()
        {
            timer?.Dispose();

            // The following prevents derived types that introduce a
            // finalizer from needing to re-implement IDisposable.
            GC.SuppressFinalize(this);
        }
    }
    public class NotifierService
    {
        public async Task Update(string key, int value)
        {
            if (Notify != null)
            {
                await Notify.Invoke(key, value);
            }
        }

        public event Func<string, int, Task>? Notify;
    }
}