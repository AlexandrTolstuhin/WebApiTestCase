using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiTestCase.Api.Controllers.Common;
using WebApiTestCase.Application.Models;
using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Application.Services.Task;
using WebApiTestCase.Common.Collections;

namespace WebApiTestCase.Api.Controllers
{
    public class TaskController : ApiController
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(ApiResult<TaskResponseModel>.Success200(
                await _taskService.GetTaskByIdAsync(id)));
        }

        [HttpGet("Provided/{userId}")]
        public async Task<IActionResult> GetProvided(int userId, [FromQuery] PagedModel paged)
        {
            return Ok(ApiResult<IPagedList<TaskResponseModel>>.Success200(
                await _taskService.GetUserProvidedTasksAsync(userId, paged)));
        }

        [HttpGet("Performed/{userId}")]
        public async Task<IActionResult> GetPerformed(int userId, [FromQuery] PagedModel paged)
        {
            return Ok(ApiResult<IPagedList<TaskResponseModel>>.Success200(
                await _taskService.GetUserPerformedTasksAsync(userId, paged)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TaskEditModel model)
        {
            return Ok(ApiResult<int>.Success200(
                await _taskService.UpdateAsync(id, model)));
        }

        [HttpPut("Status/{id}")]
        public async Task<IActionResult> PutStatus(int id, [FromBody] TaskStatusEditModel model)
        {
            return Ok(ApiResult<int>.Success200(
                await _taskService.SetStatusAsync(id, model)));
        }

        [HttpPut("Provider/{id}")]
        public async Task<IActionResult> PutProvider(int id, [FromBody] TaskProviderEditModel model)
        {
            return Ok(ApiResult<int>.Success200(
                await _taskService.SetProviderAsync(id, model)));
        }
    }
}