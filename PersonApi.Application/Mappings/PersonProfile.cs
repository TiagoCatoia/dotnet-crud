using AutoMapper;
using PersonApi.Application.DTOs.Person;
using PersonApi.Domain.Entities;

namespace PersonApi.Application.Mappings;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePersonDto, Person>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        CreateMap<Person, PersonResponseDto>();
    }
}