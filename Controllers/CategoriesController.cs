using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_Manager.Core;
using Task_Manager.Core.Interfaces;
using Task_Manager.Models;
using Task_Manager.Models.DTO;

namespace Task_Manager.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _unitOfWork.Categories.GetAll();
        if (!categories.Any()) return BadRequest("No Categories Found");
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var target = await _unitOfWork.Categories.GetById(id);
        if (target is null) return BadRequest("Invalid Category Selected");
        return Ok(target);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CategoryDTO categoryDTO)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid Data");
        Category category = new()
        {
            Name = categoryDTO.Name,
            ColorCode = categoryDTO.ColorCode,
            CreatedAt = DateTime.Now,

        };
        var isCreated = _unitOfWork.Categories.Add(category);
        if (isCreated)
        {
            await _unitOfWork.CompleteAsync();
            return Created("/Categories", category);
        }
        else
        {
            return BadRequest("Something Is Wrong");
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO categoryDTO)
    {
        var targetOne = await _unitOfWork.Categories.GetById(id);
        if (targetOne is null) return BadRequest("Invalid Category");
        targetOne.Name = categoryDTO.Name;
        targetOne.ColorCode = categoryDTO.ColorCode;
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var targetOne = await _unitOfWork.Categories.GetById(id);
        if (targetOne is null) return BadRequest("Invalid Category");
        var isDeleted = _unitOfWork.Categories.Delete(targetOne);
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