using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task_Manager.Core.Interfaces;
using Task_Manager.Models.DTO;
using TaskModel = Task_Manager.Models.Task;

namespace Task_Manager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TasksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _unitOfWork.Tasks.GetAll();
            if (!tasks.Any()) return BadRequest("There is no Tasks");
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var targetOne = await _unitOfWork.Tasks.GetById(id);
            if (targetOne is null) return BadRequest("Invalid Selected Task");
            return Ok(targetOne);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TaskDTO taskDTO)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid Data");
            TaskModel task = new()
            {
                CategoryId = taskDTO.CategoryId,
                CreatedAt = DateTime.Now,
                Description = taskDTO.Description,
                DueDate = taskDTO.DueDate,
                ProjectId = taskDTO.ProjectId,
                StartDate = taskDTO.StartDate,
                PriorityLevel = taskDTO.PriorityLevel,
                Status = taskDTO.Status,
                Title = taskDTO.Title,
                 //TODO: 
            };
            var isCreated = _unitOfWork.Tasks.Add(task);
            if (isCreated is true)
            {
                await _unitOfWork.CompleteAsync();
                return Created("/Tasks", task);
            }
            else
            {
                return BadRequest("Something WentWrong, Please try again later");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ModifiedTaskDTO taskDTO)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid Data");
            var targetOne = await _unitOfWork.Tasks.GetById(id);
            if (targetOne is null) return BadRequest("Invalid Data");
            targetOne.Title = taskDTO.Title;
            targetOne.Description = taskDTO.Description;
            targetOne.DueDate = taskDTO.DueDate;
            targetOne.StartDate = taskDTO.StartDate;
            targetOne.PriorityLevel = taskDTO.PriorityLevel;
            targetOne.Status = taskDTO.Status;

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var targetOne = await _unitOfWork.Tasks.GetById(id);
            if (targetOne is null) return BadRequest("Not Found this Task");
            var isDeleted = _unitOfWork.Tasks.Delete(targetOne);
            if (isDeleted is true)
            {
                await _unitOfWork.CompleteAsync();
                return Ok("Deleted Successfully");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}