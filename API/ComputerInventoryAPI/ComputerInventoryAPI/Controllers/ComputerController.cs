using ComputerInventoryAPI.Models;
using ComputerInventoryAPI.Models.Entities;
using ComputerInventoryAPI.Services.ComputerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerInventoryAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ComputerController : ControllerBase
{
    private readonly IComputerService _service;

    public ComputerController(IComputerService service)
    {
        _service = service;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetComputers()
    {
        var result = await _service.GetAllComputers();
        return Ok(result); // 200: Ok
    }

    [HttpGet("{id:int}")]
    public IActionResult Get([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400: Bad Request: Invalid request format
        }

        var c = _service.GetComputer(id);
        if (c == null)
        {
            return NotFound($"No computer with Id {id} was found."); // 404: Not Found
        }

        return Ok(c);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ComputerDtoAdd computer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingComputer = _service.GetComputerByName(computer.Name);
        if (existingComputer != null)
        {
            return Conflict("A computer with the same name already exists."); // 409: Conflict
        }
        
        var newComputer = new Computer
        {
            Name = computer.Name,
            Processor = computer.Processor,
            Ram = computer.Ram,
            Storage = computer.Storage,
            BrandId = computer.BrandId,
            OperatingSystemId = computer.OperatingSystemId
        };

        await _service.Save(newComputer);
        return CreatedAtAction("Get", new { id = computer.Id }, newComputer); // 201: Created
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ComputerDtoAdd computer)
    {
        if (id != computer.Id)
            return BadRequest("Id from route does not match id from body.");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingComputer = _service.GetComputer(id);
        if (existingComputer is null)
            return NotFound($"No computer with id {id} found.");

        var updatedComputer = new Computer
        {
            Id = computer.Id,
            Name = computer.Name,
            Processor = computer.Processor,
            Ram = computer.Ram,
            Storage = computer.Storage,
            BrandId = computer.BrandId,
            OperatingSystemId = computer.OperatingSystemId
        };

        await _service.Save(updatedComputer);
        return Ok(new { Message = $"Computer with id {id} successfully updated!", Computer = updatedComputer });
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var computer = _service.GetComputer(id);
        if (computer is null)
            return NotFound($"No computer with id {id} found.");

        _service.Delete(id);

        return NoContent(); // 204: No Content
        //return Ok($"Computer with id {id} successfully deleted!");
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] ComputerDtoAdd computer)
    {
        return StatusCode(StatusCodes.Status501NotImplemented); // 501: Not Implemented
    }
}

// Summary:

// 200: Ok
// 201: Created
// 204: No Content

// 400: Bad Request
// 404: Not Found
// 409: Conflict

// 501: Not Implemented
