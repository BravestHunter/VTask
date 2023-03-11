using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTask.Model.DAO;
using VTask.Model.DTO;
using VTask.Model.DTO.Task;
using VTask.Model.MVC;
using VTask.Repositories;
using VTask.Services;

namespace VTask.Controllers.MVC
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var tasks = await _taskService.GetAll();

            var taskModels = tasks.Select(t => _mapper.Map<TaskModel>(t));

            return View(taskModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = _mapper.Map<TaskAddRequestDto>(model);
            var response = await _taskService.Add(request);

            TempData[Constants.Notification.SuccessMessageTempBagKey] = "Task was created successfully";

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var request = new TaskGetRequestDto() { Id = id.Value };
            var response = await _taskService.Get(request);

            var model = _mapper.Map<TaskModel>(response);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = _mapper.Map<TaskUpdateRequestDto>(model);
            var response = await _taskService.Update(request);

            TempData[Constants.Notification.SuccessMessageTempBagKey] = "Task was updated successfully";

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var request = new TaskGetRequestDto() { Id = id.Value };
            var response = await _taskService.Get(request);

            var model = _mapper.Map<DeleteTaskModel>(response);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteTaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = _mapper.Map<TaskRemoveTRequestDto>(model);
            var response = await _taskService.Remove(request);

            TempData[Constants.Notification.SuccessMessageTempBagKey] = "Task was deleted successfully";

            return RedirectToAction(nameof(All));
        }
    }
}
