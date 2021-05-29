using AutoMapper;
using Valyria.Models;
using DbNation = Repository.DataAccessLayer.Entities.Nation;

namespace Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DbNation, Nation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.NationId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NationName));
        }
    }
}
