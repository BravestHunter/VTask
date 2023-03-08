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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<UserTask> tasks = await _unitOfWork.UserTaskRepository.GetAll();

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
            _unitOfWork.UserTaskRepository.Add(task);

            await _unitOfWork.SaveChanges();

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

            var task = await _unitOfWork.UserTaskRepository.Get(id.Value);
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
            _unitOfWork.UserTaskRepository.Update(task);

            await _unitOfWork.SaveChanges();

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

            var task = await _unitOfWork.UserTaskRepository.Get(id.Value);
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

            var task = await _unitOfWork.UserTaskRepository.Get(requestDto.Id);
            if (task == null)
            {
                return NotFound();
            }

            _unitOfWork.UserTaskRepository.Remove(task);

            await _unitOfWork.SaveChanges();

            TempData["SuccessMessage"] = "Task was deleted successfully";

            return RedirectToAction(nameof(All));
        }
    }
}
