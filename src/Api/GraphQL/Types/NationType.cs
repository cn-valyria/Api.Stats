using Api.GraphQL.Helpers;
using AutoMapper;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using Repository;
using System.Linq;
using System.Threading.Tasks;
using Valyria.Models;

namespace Api.GraphQL.Types
{
    public class NationType : ObjectGraphType<Nation>
    {
        private readonly IDataLoaderContextAccessor _dataLoaderContextAccessor;
        private readonly ICnDbRepository _cnDbRepository;

        public NationType(ICnDbRepository cnDbRepository, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            _cnDbRepository = cnDbRepository;
            _dataLoaderContextAccessor = dataLoaderContextAccessor;

            Name = "Nation";
            Description = "An individual nation. The core entity in Cybernations, and the thing that someone plays as.";

            Field(n => n.Id).Description("The Nation ID");
            Field(n => n.Name).Description("The name of the nation");
            Field(n => n.RulerName).Description("The name of the nation's ruler");
            Field<AllianceType>(nameof(Nation.Alliance), "The alliance that the nation is affiliated with", resolve: GetAllianceByNation);
            Field<DateTimeGraphType>(nameof(Nation.AllianceDate), "The date that the nation last changed its alliance affiliation");
            Field(n => n.AllianceStatus).Description("The status of the nation within its alliance");
            Field<GovernmentTypeEnum>(nameof(Nation.GovernmentType), "The nation's selected government type");
            Field<ReligionEnum>(nameof(Nation.Religion), "The nation's selected religion");
            Field<TeamEnum>(nameof(Nation.Team), "The nation's selected team, aka color");
            Field<DateTimeGraphType>(nameof(Nation.Created), "The date that the nation was created");
            Field<DecimalGraphType>(nameof(Nation.Technology), "The amount of technology that the nation currently owns");
            Field<DecimalGraphType>(nameof(Nation.Infrastructure), "The amount of infrastructure that the nation currently owns");
            Field<DecimalGraphType>(nameof(Nation.BaseLand), "The base amount of land that the nation currently owns, not including game-driven modifiers");
            Field<NationalWarStatusEnum>(nameof(Nation.WarStatus), "The current war/peace mode selection of the nation");
            Field(n => n.Votes).Description("The number of senate votes that the nation has received");
            Field<DecimalGraphType>(nameof(Nation.Strength), "Nation Strength, a score reflecting the overall strength of the current nation");
            Field(n => n.Defcon).Description("The DEFCON level of the nation");
            Field(n => n.BaseSoldiers).Description("The amount of soldiers that the nation is currently holding, not including effective soldiers");
            Field(n => n.Tanks).Description("The amount of tanks that the nation is currently holding");
            Field(n => n.CruiseMissiles).Description("The amount of cruise missiles that the nation is currently holding");
            Field(n => n.Nukes).Description("The amount of nuclear missiles that the nation is currently holding");
            Field<RecentActivityEnum>(nameof(Nation.RecentActivity), "A rough measure of how recently the player logged into their nation");
            Field<DateTimeGraphType>(nameof(Nation.UpdatedOn), "The date and time that this state of the nation was recorded");
            Field<ListGraphType<NationType>>(
                "auditHistory",
                "All recorded historical audit entries for the nation. Entries are only recorded if an actual change in the data occurred",
                new QueryArguments(
                    new QueryArgument<DateTimeGraphType> { Name = "entryRecordedBefore", Description = "A \"lower bound\" of timestamps after which audits must have been recorded. Defaults to the beginning of time" },
                    new QueryArgument<DateTimeGraphType> { Name = "entryRecordedAfter", Description = "An \"upper bound\" of timestamps before which audits must have been recorded. Defaults to right now" },
                    new QueryArgument<IntGraphType> { Name = "limit", Description = "The number of audit records to return, at most. Defaults to 100" },
                    new QueryArgument<IntGraphType> { Name = "offset", Description = "The number of audit records to skip before beginning to return results. Defaults to 0" }),
                GetNationAuditHistory);
        }

        private object GetAllianceByNation(IResolveFieldContext<Nation> context)
            => _dataLoaderContextAccessor.GetAllianceById(context.Source.Alliance?.Id ?? 0, "GetAlliancesForNationByIds", _cnDbRepository);

        private object GetNationAuditHistory(IResolveFieldContext<Nation> context) => _cnDbRepository.Nations.GetAuditHistory(context.Source.Id);
    }
}
