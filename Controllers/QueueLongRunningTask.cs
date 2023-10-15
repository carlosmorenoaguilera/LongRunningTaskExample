using LongRunningTaskExample.BackgroundServices;
using Microsoft.AspNetCore.Mvc;

namespace LongRunningTaskExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueLongRunningTask: ControllerBase
    {
        private readonly IBackgroundTaskQueue _queue;
        private readonly ILogger<QueueLongRunningTask> _logger;
        public QueueLongRunningTask(IBackgroundTaskQueue queue, ILogger<QueueLongRunningTask> logger)
        {
            _queue = queue;
            _logger = logger;
        }



        [HttpGet]
        [Route("queuework")]
        public async Task<IActionResult> QueueWork(string work)
        {
            
            await _queue.QueueBackgroundWorkItemAsync(async (token) =>
            {

                await Task.Delay(10 *1000);
                _logger.LogInformation($"Finish the job");
                
            });

            _logger.LogInformation("reponse send ");
            return Ok(Guid.NewGuid());
        }
    }
}
