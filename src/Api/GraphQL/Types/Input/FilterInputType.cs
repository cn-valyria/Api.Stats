using GraphQL.Types;
using Repository.Models;

namespace Api.GraphQL.Types.Input
{
    public class FilterInputType : InputObjectGraphType<SearchFilter>
    {
        public FilterInputType()
        {
            Name = "Filter";

            Field(x => x.NationIds, nullable: true);
            Field(x => x.NationName, nullable: true);
            Field(x => x.RulerName, nullable: true);
            Field(x => x.AllianceName, nullable: true);
            Field<DecimalGraphType>(nameof(SearchFilter.NationStrengthUpperBound));
            Field<DecimalGraphType>(nameof(SearchFilter.NationStrengthLowerBound));
            Field<MatchTypeEnum>(nameof(SearchFilter.Match));
        }
    }
}
