using AutoMapper;
using Valyria.Models;
using DbNation = Repository.DataAccessLayer.DTO.Nation;
using DbAlliance = Repository.DataAccessLayer.DTO.Alliance;
using DbWar = Repository.DataAccessLayer.DTO.War;

namespace Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DbNation, Nation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.NationId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NationName))
                .ForMember(dest => dest.Alliance, opt => opt.MapFrom(src => src.AllianceId.HasValue ? new Alliance { Id = src.AllianceId.Value } : null));

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

            CreateMap<DbWar, War>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WarId))
                .ForMember(dest => dest.AttackingNation, opt => opt.MapFrom(src => new Nation { Id = src.AttackingNationId }))
                .ForMember(dest => dest.AttackingAlliance, opt => opt.MapFrom(src => src.AttackingAllianceId == 0 ? null : new Alliance { Id = src.AttackingAllianceId }))
                .ForMember(dest => dest.DefendingNation, opt => opt.MapFrom(src => new Nation { Id = src.DefendingNationId }))
                .ForMember(dest => dest.DefendingAlliance, opt => opt.MapFrom(src => src.DefendingAllianceId == 0 ? null : new Alliance { Id = src.DefendingAllianceId }))
                .ForMember(dest => dest.AttackingDestruction, opt => opt.MapFrom((src, dest) =>
                {
                    /*
                     * The stats file that admin downloads almost always gets percentages wrong. Both attacking and defending 
                     * percentages are always rounded down if they contain any decimal places, so any combination of numbers 
                     * that are not perfect integers will result in two percentages that equals 99 instead of 100, which will 
                     * cause calculations of attacking and defending destruction to result in a number lower than the real 
                     * total destruction.
                     * Since we have no way of knowing the real percentages - i.e. there is nothing that says whether a ratio 
                     * like 29/70 was actually supposed to be 29.9/70.1 or 29.1/70.9 - the current solution is just to reward 
                     * defenders by penalizing attackers with the missing percentage point of destruction. The decision is 
                     * completely arbitrary, but in absence of actual correct data, I like the idea of making it just a bit 
                     * harder to have stats go in your favor as an attacker.
                     */
                    var realAttackingPercent = src.AttackPercent + src.DefendPercent == 100 ? src.AttackPercent : src.AttackPercent + 1;
                    return src.Destruction * (realAttackingPercent / 100m);
                }))
                .ForMember(dest => dest.DefendingDestruction, opt => opt.MapFrom(src => src.Destruction * (src.DefendPercent / 100m)));
        }
    }
}
