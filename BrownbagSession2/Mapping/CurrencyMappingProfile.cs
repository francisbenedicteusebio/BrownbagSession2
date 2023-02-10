using AutoMapper;
using BrownbagSession2.Contracts;
using BrownbagSession2.DTO;

namespace BrownbagSession2.Mapping
{
    public class CurrencyMappingProfile : Profile
    {
        public CurrencyMappingProfile()
        {
            CreateMap<CurrencyCodes, CurrencyCodesDto>()
                .ForMember(dest => dest.Id, 
                    opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDateTime,
                    opt => opt.Ignore())
                .ForMember(dest => dest.CurrencyCode, 
                    opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.CurrencyRate, 
                    opt => opt.MapFrom(src => src.Rate));
        }
    }
}
