using GraphQL.Types;
using Repository.Models;

namespace Api.GraphQL.Types.Input
{
    public class WarFilterInputType : InputObjectGraphType<WarSearchFilter>
    {
        public WarFilterInputType()
        {
            Name = "WarFilter";

            Field(w => w.AttackingNation, nullable: true);
            Field(w => w.AttackingAlliance, nullable: true);
            Field(w => w.DefendingNation, nullable: true);
            Field(w => w.DefendingAlliance, nullable: true);
            Field<MatchTypeEnum>(nameof(WarSearchFilter.Match));
        }
    }
}
