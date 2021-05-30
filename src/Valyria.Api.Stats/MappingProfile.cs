using AutoMapper;
using Valyria.Models;
using DbNation = Repository.DataAccessLayer.DTO.Nation;
using DbAlliance = Repository.DataAccessLayer.DTO.Alliance;

namespace Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DbNation, Nation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.NationId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NationName));

            CreateMap<DbAlliance, Alliance>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AllianceId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AllianceName))
                .ForMember(dest => dest.TotalStrength, opt => opt.MapFrom(src => src.Strength))
                .ForMember(dest => dest.TotalLand, opt => opt.MapFrom(src => src.Land))
                .ForMember(dest => dest.TotalInfrastructure, opt => opt.MapFrom(src => src.Infrastructure))
                .ForMember(dest => dest.TotalTechnology, opt => opt.MapFrom(src => src.Technology))
                .ForMember(dest => dest.NationsAtWar, opt => opt.MapFrom(src => src.War))
                .ForMember(dest => dest.NationsAtPeace, opt => opt.MapFrom(src => src.Peace))
                .ForMember(dest => dest.TotalSoldiers, opt => opt.MapFrom(src => src.Soldiers))
                .ForMember(dest => dest.TotalTanks, opt => opt.MapFrom(src => src.Tanks))
                .ForMember(dest => dest.TotalCruiseMissiles, opt => opt.MapFrom(src => src.Cruise))
                .ForMember(dest => dest.TotalNuclearWeapons, opt => opt.MapFrom(src => src.Nukes))
                .ForMember(dest => dest.TotalAircraft, opt => opt.MapFrom(src => src.Aircraft))
                .ForMember(dest => dest.TotalNavy, opt => opt.MapFrom(src => src.Navy))
                .ForMember(dest => dest.TotalNationsInAnarchy, opt => opt.MapFrom(src => src.Anarchy));
        }
    }
}
