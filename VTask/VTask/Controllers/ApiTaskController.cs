using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VTask.Model;
using VTask.Model.DTO;
using VTask.Services;

namespace VTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApiTaskController : ControllerBase
    {
        private readonly IUserTaskService _userTaskService;

        public ApiTaskController(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserTaskResponseDto>>> GetTask(int id)
        {
            var serviceResponse = await _userTaskService.Get(id);

            return Ok(serviceResponse);
        }

        [HttpGet("all")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetUserTaskResponseDto>>>> GetAllTasks()
        {
            var serviceResponse = await _userTaskService.GetAll();

            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AddUserTaskResponseDto>>> AddTask(AddUserTaskRequestDto addTaskRequestDto)
        {
            var serviceResponse = await _userTaskService.Add(addTaskRequestDto);

            return Ok(serviceResponse);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<AddUserTaskResponseDto>>> UpdateTask(UpdateUserTaskRequestDto updateTaskRequestDto)
        {
            var serviceResponse = await _userTaskService.Update(updateTaskRequestDto);

            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserTaskResponseDto>>> DeleteTask(int id)
        {
            var serviceResponse = await _userTaskService.Delete(id);

            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }
    }
}
