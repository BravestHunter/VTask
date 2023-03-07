using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTask.Model;
using VTask.Model.DTO;
using VTask.Repositories;
using VTask.Services;

namespace VTask.Controllers.MVC
{
    public class TaskController : Controller
    {
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly IMapper _mapper;

        public TaskController(IUserTaskRepository userTaskRepository, IMapper mapper)
        {
            _userTaskRepository = userTaskRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<UserTask> tasks = await _userTaskRepository.GetLast(10);

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserTaskRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return View(requestDto);
            }

            var task = _mapper.Map<UserTask>(requestDto);
            await _userTaskRepository.Add(task);

            TempData["SuccessMessage"] = "Task was created successfully";

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }

            var task = await _userTaskRepository.Get(id.Value);
            if (task == null)
            {
                return NotFound();
            }

            var requestDto = _mapper.Map<UpdateUserTaskRequestDto>(task);

            return View(requestDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateUserTaskRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return View(requestDto);
            }

            var task = _mapper.Map<UserTask>(requestDto);
            await _userTaskRepository.Update(task);

            TempData["SuccessMessage"] = "Task was updated successfully";

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var task = await _userTaskRepository.Get(id.Value);
            if (task == null)
            {
                return NotFound();
            }

            var requestDto = _mapper.Map<DeleteUserTaskRequestDto>(task);

            return View(requestDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteUserTaskRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return View(requestDto);
            }

            var task = await _userTaskRepository.Get(requestDto.Id);
            if (task == null)
            {
                return NotFound();
            }

            await _userTaskRepository.Delete(task);

            TempData["SuccessMessage"] = "Task was deleted successfully";

            return RedirectToAction(nameof(All));
        }
    }
}
