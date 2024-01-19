using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreUseQueueBackgroundWorkItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController(IBackgroundTaskQueue taskQueue)
        {
            TaskQueue = taskQueue;
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            await TaskQueue.QueueBackgroundWorkItemAsync(async (token) =>
            {
                await Console.Out.WriteLineAsync("Hello World!");
            });

            return new string[] { "value1", "value2" };
        }
    }
}
