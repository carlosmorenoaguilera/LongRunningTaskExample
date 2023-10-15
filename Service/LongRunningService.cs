namespace LongRunningTaskExample.Service
{
    public class LongRunningService : BackgroundService
    {
        private readonly BackgroundWorkerQueue _queue;


        public LongRunningService(BackgroundWorkerQueue queue)
        {
            _queue = queue; 
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _queue.DequeDequeueAsync(stoppingToken);
                await workItem(stoppingToken);
            }
        }
    }
}
