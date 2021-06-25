using GraphQL.Types;
using Repository.Models;

namespace Api.GraphQL.Types.Input
{
    public class NationFilterInputType : InputObjectGraphType<NationSearchFilter>
    {
        public NationFilterInputType()
        {
            Name = "NationFilter";

            Field(x => x.NationIds, nullable: true);
            Field(x => x.NationName, nullable: true);
            Field(x => x.RulerName, nullable: true);
            Field(x => x.AllianceName, nullable: true);
            Field<DecimalGraphType>(nameof(NationSearchFilter.NationStrengthUpperBound));
            Field<DecimalGraphType>(nameof(NationSearchFilter.NationStrengthLowerBound));
            Field<MatchTypeEnum>(nameof(NationSearchFilter.Match));
        }
    }
}
