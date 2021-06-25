using GraphQL.Types;
using Repository.Models;

namespace Api.GraphQL.Types.Input
{
    public class AllianceFilterInputType : InputObjectGraphType<AllianceSearchFilter>
    {
        public AllianceFilterInputType()
        {
            Name = "AllianceFilter";

            Field(x => x.AllianceName, nullable: true);
            Field<DecimalGraphType>(nameof(AllianceSearchFilter.AllianceScoreLowerBound));
            Field<DecimalGraphType>(nameof(AllianceSearchFilter.AllianceScoreUpperBound));
            Field<MatchTypeEnum>(nameof(NationSearchFilter.Match));
        }
    }
}
