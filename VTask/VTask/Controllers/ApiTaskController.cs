using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VTask.Model;
using VTask.Model.DTO;
using VTask.Model.DTO.Task;
using VTask.Services;

namespace VTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApiTaskController : ControllerBase
    {
        private readonly ITaskService _userTaskService;

        public ApiTaskController(ITaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskGetResponseDto>> Get(TaskGetRequestDto request)
        {
            var response = await _userTaskService.Get(request);

            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TaskGetResponseDto>>> GetAll()
        {
            var response = await _userTaskService.GetAll();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<TaskAddResponseDto>> Add(TaskAddRequestDto request)
        {
            var response = await _userTaskService.Add(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<TaskUpdateResponseDto>> Update(TaskUpdateRequestDto request)
        {
            var response = await _userTaskService.Update(request);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskRemoveResponseDto>> Remove(TaskRemoveTRequestDto request)
        {
            var response = await _userTaskService.Remove(request);

            return Ok(response);
        }
    }
}
