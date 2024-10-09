using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile 
    {
        public AutoMapperProfiles()
        {
            CreateMap<Usluga, UslugaDto>();
            CreateMap<Zaposlenik, ZaposlenikDto>();
            CreateMap<Termin, TerminDto>();
            CreateMap<UslugaUpdateDto, Usluga>();
            CreateMap<UslugaInsertDto, Usluga>();
            CreateMap<TerminInsertDto, Termin>();
            CreateMap<TerminUpdateDto, Termin>();
            CreateMap<ZaposlenikDto, Zaposlenik>();
            CreateMap<ZaposlenikUpdateDto, Zaposlenik>();
            CreateMap<ZaposlenikInsertDto, Zaposlenik>();
            CreateMap<UslugaDto, Usluga>();
        }
    }
}
