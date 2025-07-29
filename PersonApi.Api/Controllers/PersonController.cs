using Microsoft.AspNetCore.Mvc;
using PersonApi.Api.Helpers;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Repositories;

namespace PersonApi.Api.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController(IPersonRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetAll()
    {
        var persons = await repository.GetAllAsync();
        return Ok(persons);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var person = await repository.GetByIdAsync(id);
        if (person == null)
            return ErrorResponseHelper.NotFound($"Person with ID {{person.Id}} not found.", HttpContext);

        return Ok(person);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Person person)
    {
        var existing = await repository.GetByIdAsync(person.Id);
        if (existing != null)
            return ErrorResponseHelper.Conflict("There is already a person with this ID.", HttpContext);
        
        await repository.AddAsync(person);
        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Person person)
    {
        var existing = await repository.GetByIdAsync(person.Id);
        if (existing == null)
            return ErrorResponseHelper.NotFound($"Person with ID {person.Id} not found.", HttpContext);
        
        await repository.UpdateAsync(person);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await repository.GetByIdAsync(id);
        if (existing == null)
            return ErrorResponseHelper.NotFound($"Person with ID {id} not found.", HttpContext);
        
        await repository.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpGet("test-error")]
    public IActionResult ThrowError()
    {
        throw new Exception("Forced error");
    }
}