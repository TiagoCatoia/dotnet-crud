using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<Person>> GetById(int id)
    {
        var person = await repository.GetByIdAsync(id);
        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<Person>> Create(Person person)
    {
        await repository.AddAsync(person);
        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }

    [HttpPut]
    public async Task<ActionResult<Person>> Update(Person person)
    {
        await repository.UpdateAsync(person);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Person>> Delete(int id)
    {
        await repository.DeleteAsync(id);
        return NoContent();
    }
}