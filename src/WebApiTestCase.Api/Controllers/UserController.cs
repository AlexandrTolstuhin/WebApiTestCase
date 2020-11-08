using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiTestCase.Api.Controllers.Common;
using WebApiTestCase.Application.Models;
using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Application.Models.User;
using WebApiTestCase.Application.Services.Task;
using WebApiTestCase.Application.Services.User;
using WebApiTestCase.Common.Collections;

namespace WebApiTestCase.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;

        public UserController(IUserService userService, ITaskService taskService)
        {
            _userService = userService;
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagedModel paged)
        {
            return Ok(ApiResult<IPagedList<UserResponseModel>>.Success200(
                await _userService.GetAllAsync(paged)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(ApiResult<UserResponseModel>.Success200(
                await _userService.GetByIdAsync(id)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserEditModel model)
        {
            return Ok(ApiResult<int>.Success200(
                await _userService.UpdateAsync(id, model)));
        }

        [HttpPost("AssignTask")]
        public async Task<IActionResult> Post([FromBody] TaskCreateModel model)
        {
            return Ok(ApiResult<int>.Success200(
                await _taskService.CreateAsync(model)));
        }
    }
}