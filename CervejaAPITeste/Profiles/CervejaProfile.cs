using AutoMapper;
using CervejaAPITeste.Data.Dtos;
using CervejaAPITeste.Models;

namespace CervejaAPITeste.Profiles;

public class CervejaProfile : Profile
{
    public CervejaProfile()
    {
        CreateMap<CreatCervejaDto, Cerveja>();
        CreateMap<UpdateCervejaDto, Cerveja>();
        CreateMap<Cerveja, UpdateCervejaDto>();
        CreateMap<Cerveja, ReadCervejaDto>();
    }
}
