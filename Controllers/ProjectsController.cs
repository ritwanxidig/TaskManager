using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task_Manager.Core.Interfaces;
using Task_Manager.Models;
using Task_Manager.Models.DTO;

namespace Task_Manager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _unitOfWork.Projects.GetAll();
            if (!projects.Any()) return BadRequest("There is no Projects");
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var targetOne = await _unitOfWork.Projects.GetById(id);
            if (targetOne is null) return BadRequest("Not Found this Project");
            return Ok(targetOne);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProjectDTO projectDTO)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid Data");
            Project project = new()
            {
                Completed = false,
                CreatedAt = DateTime.Now,
                Description = projectDTO.Description,
                EndDate = projectDTO.EndDate,
                Name = projectDTO.Name,
                StartDate = projectDTO.StartDate,
                 // TODO: Logged Usser
            };

            _unitOfWork.Projects.Add(project);

            await _unitOfWork.CompleteAsync();
            return Created("/Projects", project);




        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] ProjectDTO projectDTO)
        {
            var targetOne = await _unitOfWork.Projects.GetById(id);
            if (targetOne is null) return BadRequest("Invalid Project");
            if (!ModelState.IsValid) return BadRequest("Invalid Data");
            targetOne.Description = projectDTO.Description;
            targetOne.EndDate = projectDTO.EndDate;
            targetOne.StartDate = projectDTO.StartDate;
            targetOne.Name = projectDTO.Name;

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var targetOne = await _unitOfWork.Projects.GetById(id);
            if (targetOne is null) return BadRequest("Invalid Project");

            _unitOfWork.Projects.Delete(targetOne);
            await _unitOfWork.CompleteAsync();

            return Ok("Successfully Deleted");
        }
    }
}