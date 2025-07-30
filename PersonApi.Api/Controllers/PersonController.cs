using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonApi.Api.Helpers;
using PersonApi.Api.Services.Authentication;
using PersonApi.Application.DTOs.Person;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Repositories;

namespace PersonApi.Api.Controllers;

[ApiController]
[Route("api/person")]
[Authorize]
public class PersonController(IPersonRepository repository, IMapper mapper, IAuthService authService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PersonResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<Person>>> GetAll()
    {
        var persons = await repository.GetAllAsync();
        var result = mapper.Map<IEnumerable<PersonResponseDto>>(persons);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PersonResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var person = await repository.GetByIdAsync(id);
        if (person == null)
            return ErrorResponseHelper.NotFound($"Person with ID {id} not found.", HttpContext);

        var result = mapper.Map<PersonResponseDto>(person);
        return Ok(result);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PersonResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(CreatePersonDto dto)
    {
        var existing = await repository.GetByEmailAsync(dto.Email);
        if (existing != null)
            return Conflict(new { message = "Email already registered" });
        
        var person = mapper.Map<Person>(dto);
        person.SetPassword(dto.Password);
        await repository.AddAsync(person);
        
        var result = mapper.Map<PersonResponseDto>(person);
        return CreatedAtAction(nameof(GetById), new { id = person.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existing = await repository.GetByIdAsync(id);
        if (existing == null)
            return ErrorResponseHelper.NotFound($"Person with ID {id} not found.", HttpContext);
        
        await repository.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpGet("test-error")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult ThrowError()
    {
        throw new Exception("Forced error");
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(PersonLoginRequestDto requestDto)
    {
        var person = await repository.GetByEmailAsync(requestDto.Email);
        if (person is null || !person.CheckPassword(requestDto.Password))
            return Unauthorized(new { message = "Invalid credentials" });
        
        var token = authService.GenerateJwtToken(person.Id.ToString());
        var loginResponse = new PersonLoginResponseDto(token);
        return Ok(loginResponse);
    }
}