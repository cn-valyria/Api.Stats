using Api.GraphQL.Helpers;
using GraphQL.DataLoader;
using GraphQL.Types;
using Repository;
using Valyria.Models;

namespace Api.GraphQL.Types
{
    public class AidType : ObjectGraphType<Aid>
    {
        private readonly ICnDbRepository _cnDbRepository;
        private readonly IDataLoaderContextAccessor _dataLoaderContextAccessor;

        public AidType(ICnDbRepository cnDbRepository, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            _cnDbRepository = cnDbRepository;
            _dataLoaderContextAccessor = dataLoaderContextAccessor;

            Name = "Aid";
            Description = "A package of money, technology, and/or soldiers sent from one nation to another";

            Field(a => a.Id).Description("The unique identifier of the aid package");
            Field<NationType>(
                nameof(Aid.SendingNation), 
                "The nation that sent the aid",
                resolve: context => GetNationById(context.Source.SendingNation.Id));
            Field<AllianceType>(
                nameof(Aid.SendingAlliance), 
                "The alliance that the nation who sent the aid was in at the time the aid was sent",
                resolve: context => GetAllianceById(context.Source.SendingAlliance?.Id ?? 0));
            Field<TeamEnum>(nameof(Aid.SendingTeam), "The team that the nation who sent the aid was in when at the time the aid was sent");

            Field<NationType>(
                nameof(Aid.ReceivingNation), 
                "The nation that the nation who received the aid was in when at the time the aid was sent",
                resolve: context => GetNationById(context.Source.ReceivingNation.Id));
            Field<AllianceType>(
                nameof(Aid.ReceivingAlliance), 
                "The alliance that the nation who received the aid was in when at the time the aid was sent",
                resolve: context => GetAllianceById(context.Source.ReceivingAlliance?.Id ?? 0));
            Field<TeamEnum>(nameof(Aid.ReceivingTeam), "The team that the nation who received the aid was in when at the time the aid was sent");

            Field<AidStatusEnum>(nameof(Aid.Status), "The current status of the aid package");
            Field(a => a.Money).Description("The amount of money, if any, that was included in the aid package");
            Field(a => a.Technology).Description("The amount of technology, if any, that was included in the aid package");
            Field(a => a.Soldiers).Description("The number of soldiers, if any, that were included in the aid package");
            Field<DateTimeGraphType>(nameof(Aid.SentOn), "The exact time that the aid was sent");
            Field(a => a.Reason).Description("The reason given for the aid being sent");
        }

        private object GetNationById(int nationId)
            => _dataLoaderContextAccessor.GetNationById(nationId, "GetNationsForAidByIds", _cnDbRepository);

        private object GetAllianceById(int allianceId)
            => _dataLoaderContextAccessor.GetAllianceById(allianceId, "GetAlliancesForAidByIds", _cnDbRepository);
    }
}
