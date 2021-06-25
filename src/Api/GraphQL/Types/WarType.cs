using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using Repository;
using System.Linq;
using Valyria.Models;

namespace Api.GraphQL.Types
{
    public class WarType : ObjectGraphType<War>
    {
        private readonly IDataLoaderContextAccessor _dataLoaderContextAccessor;
        private readonly ICnDbRepository _cnDbRepository;

        public WarType(ICnDbRepository cnDbRepository, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            _cnDbRepository = cnDbRepository;
            _dataLoaderContextAccessor = dataLoaderContextAccessor;

            Name = "War";
            Description = "A single war between two nations began at a specific point in time";

            Field(w => w.Id).Description("The unique identifier of this war");
            Field<NationType>(
                nameof(War.AttackingNation), 
                "The nation that declared war", resolve: 
                context => GetNationById(context.Source.AttackingNation.Id));
            Field<AllianceType>(
                nameof(War.AttackingAlliance), 
                "The alliance that the nation was in when it declared war",
                resolve: context => GetAllianceById(context.Source.AttackingAlliance?.Id ?? 0));
            Field<TeamEnum>(nameof(War.AttackingTeam), "The team that the nation was in when it declared war");
            Field<DecimalGraphType>(nameof(War.AttackingDestruction), "The amount of destruction (aka damage) inflicted against the attacker");

            Field<NationType>(
                nameof(War.DefendingNation), 
                "The nation that is defending itself from the attacker",
                resolve: context => GetNationById(context.Source.DefendingNation.Id));
            Field<AllianceType>(
                nameof(War.DefendingAlliance), 
                "The alliance that the nation was in when it was attacked",
                resolve: context => GetAllianceById(context.Source.DefendingAlliance?.Id ?? 0));
            Field<TeamEnum>(nameof(War.DefendingTeam), "The team that the nation was in when it was attacked");
            Field<DecimalGraphType>(nameof(War.DefendingDestruction), "The amount of destruction (aka damage) inflicted against the defender");

            Field<WarStatusEnum>(nameof(War.WarStatus), "The current status of the war");
            Field<DateTimeGraphType>(nameof(War.DeclaredOn), "The exact date and time that the war was declared");
            Field<DateTimeGraphType>(nameof(War.ExpiresOn), "The date that the war should expire after");
            Field(w => w.Reason).Description("The reason given by the player declaring the war for why they declared war");
        }

        private object GetNationById(int nationId)
        {
            var dataLoader = _dataLoaderContextAccessor.Context.GetOrAddBatchLoader<int, Nation>("GetNationsForWarByIds", async nationIds =>
            {
                var allNations = await _cnDbRepository.Nations.Get(nationIds);
                return allNations.ToDictionary(nation => nation.Id, nation => nation);
            });

            return dataLoader.LoadAsync(nationId);
        }

        private object GetAllianceById(int allianceId)
        {
            var dataLoader = _dataLoaderContextAccessor.Context.GetOrAddBatchLoader<int, Alliance>("GetAlliancesForWarByIds", async allianceIds =>
            {
                var allAlliances = await _cnDbRepository.Alliances.Get(allianceIds);
                return allAlliances.ToDictionary(alliance => alliance.Id, alliance => alliance);
            });

            return dataLoader.LoadAsync(allianceId);
        }
    }
}
