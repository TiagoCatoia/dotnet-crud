using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonApi.Api.Helpers;
using PersonApi.Application.DTOs.Person;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Repositories;

namespace PersonApi.Api.Controllers;

[ApiController]
[Route("api/person")]
[Authorize]
public class PersonController(IPersonRepository repository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PersonResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Person>>> GetAll()
    {
        var persons = await repository.GetAllAsync();
        var result = mapper.Map<IEnumerable<PersonResponseDto>>(persons);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PersonResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var person = await repository.GetByIdAsync(id);
        if (person == null)
            return ErrorResponseHelper.NotFound($"Person with ID {id} not found.", HttpContext);

        var result = mapper.Map<PersonResponseDto>(person);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PersonResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreatePersonDto dto)
    {
        var person = mapper.Map<Person>(dto);
        person.SetPassword(dto.Password);
        await repository.AddAsync(person);
        
        var result = mapper.Map<PersonResponseDto>(person);
        return CreatedAtAction(nameof(GetById), new { id = person.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, UpdatePersonDto dto)
    {
        var person = await repository.GetByIdAsync(id);
        if (person == null)
            return ErrorResponseHelper.NotFound($"Person with ID {id} not found.", HttpContext);
        
        person.Name = dto.Name ?? person.Name;
        person.Age = dto.Age ?? person.Age;
        person.Email = dto.Email ?? person.Email;
        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            person.SetPassword(dto.Password);
        }
        
        await repository.UpdateAsync(person);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existing = await repository.GetByIdAsync(id);
        if (existing == null)
            return ErrorResponseHelper.NotFound($"Person with ID {id} not found.", HttpContext);
        
        await repository.DeleteAsync(id);
        return NoContent();
    }
    
    [AllowAnonymous]
    [HttpGet("test-error")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult ThrowError()
    {
        throw new Exception("Forced error");
    }
}