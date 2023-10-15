using System.Collections.Concurrent;

namespace LongRunningTaskExample.Service
{
    public class BackgroundWorkerQueue
    {
        private ConcurrentQueue<Func<CancellationToken, Task>> _WorkItems = new ConcurrentQueue<Func<CancellationToken, Task>>();
        private SemaphoreSlim _Signal = new SemaphoreSlim(0);



        public async Task<Func<CancellationToken, Task>> DequeDequeueAsync(CancellationToken cancellationToken)
        {

            await _Signal.WaitAsync(cancellationToken);
            _WorkItems.TryDequeue(out var WorkItem);
            return WorkItem;

        }



        public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
        { 
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }


            _WorkItems.Enqueue(workItem);
            _Signal.Release();
        
        
        }








    }
}
