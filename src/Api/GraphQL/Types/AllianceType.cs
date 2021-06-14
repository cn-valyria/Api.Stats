using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using Repository;
using System.Security.Cryptography.X509Certificates;
using Valyria.Models;

namespace Api.GraphQL.Types
{
    public class AllianceType : ObjectGraphType<Alliance>
    {
        private readonly ICnDbRepository _cnDbRepository;

        public AllianceType(ICnDbRepository cnDbRepository)
        {
            _cnDbRepository = cnDbRepository;

            Name = "Alliance";
            Description = "A collective of nations under the same banner.";

            Field(a => a.Id).Description("The Alliance ID");
            Field(a => a.Name).Description("The name of the alliance");
            Field<DateTimeGraphType>(nameof(Alliance.Updated), "The last date that the alliance was updated by an alliance manager");
            Field(a => a.TotalNations).Description("The number of nations that are fully-approved members of the alliance");
            Field(a => a.ActiveNations).Description("The number of fully-approved members that have been active recently");
            Field(a => a.PercentActive).Description("The percentage of fully-approved members that have been active recently");
            Field(a => a.TotalStrength).Description("The total nation strength of all fully-approved members in the alliance");
            Field(a => a.AverageStrength).Description("The average nation strength of all fully-approved members in the alliance");
            Field<DecimalGraphType>(nameof(Alliance.Score), "Alliance Score, a score reflecting the size and strength of the alliance as a whole.");
            Field(a => a.TotalLand).Description("The total amount of land held by the alliance");
            Field(a => a.TotalInfrastructure).Description("The total amount of infrastructure held by the alliance");
            Field(a => a.TotalTechnology).Description("The total amount of technology held by the alliance");
            Field(a => a.NationsAtWar).Description("The number of fully-approved members currently set to War Mode");
            Field(a => a.NationsAtPeace).Description("The number of fully-approved members currently set to Peace Mode");
            Field(a => a.TotalSoldiers).Description("The total amount of soldiers held by the alliance");
            Field(a => a.TotalTanks).Description("The total amount of tanks held by the alliance");
            Field(a => a.TotalCruiseMissiles).Description("The total amount of cruise missiles held by the alliance");
            Field(a => a.TotalNuclearWeapons).Description("The total amount of nukes held by the alliance");
            Field(a => a.TotalAircraft).Description("The total amount of aircraft held by the alliance");
            Field(a => a.TotalNavy).Description("The total amount of navy vessels held by the alliance");
            Field(a => a.TotalNationsInAnarchy).Description("The number of fully-approved members currently in anarchy");
            Field<ListGraphType<AllianceType>>(
                "auditHistory",
                "All recorded historical audit entries for the alliance. Entries are only recorded if an actual change in the data occurred",
                new QueryArguments(
                    new QueryArgument<DateTimeGraphType> { Name = "entryRecordedBefore", Description = "A \"lower bound\" of timestamps after which audits must have been recorded. Defaults to the beginning of time" },
                    new QueryArgument<DateTimeGraphType> { Name = "entryRecordedAfter", Description = "An \"upper bound\" of timestamps before which audits must have been recorded. Defaults to right now" },
                    new QueryArgument<IntGraphType> { Name = "limit", Description = "The number of audit records to return, at most. Defaults to 100" },
                    new QueryArgument<IntGraphType> { Name = "offset", Description = "The number of audit records to skip before beginning to return results. Defaults to 0" }),
                GetAllianceAuditHistory);
        }

        private object GetAllianceAuditHistory(IResolveFieldContext<Alliance> context) => _cnDbRepository.Alliances.GetAuditHistory(context.Source.Id);
    }
}
