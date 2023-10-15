using LongRunningTaskExample.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LongRunningTaskExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LongRunningTaskController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly BackgroundWorkerQueue _backgroundWorkerQueue;

        public LongRunningTaskController(ILogger<LongRunningTaskController> logger, BackgroundWorkerQueue backgroundWorkerQueue)
        {
            _logger = logger;
            _backgroundWorkerQueue = backgroundWorkerQueue; 
        }
        

        [HttpGet]
        [Route("RunTask")]
        public async Task<IActionResult> DoTask()
        {
            _logger.LogInformation("Initialize Call...");
            await CallTask();
            return Ok("Done");
        }


        [HttpGet]
        [Route("No")]
        public async Task<IActionResult> NotToDo()
        {
            _logger.LogInformation("Initialize Call...");
            await CallTask();
            return Ok("Done");
        }




        [NonAction]
        public async Task CallTask()
        {
            _logger.LogInformation($"Starting at {DateTime.UtcNow.TimeOfDay}");

            _backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
            {
                await Task.Delay(30000);
                _logger.LogInformation($"Done at {DateTime.UtcNow.TimeOfDay}");
            });
  
        }
    }


 
}
