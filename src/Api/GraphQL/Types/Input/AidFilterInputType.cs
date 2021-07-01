using GraphQL.Types;
using Repository.Models;

namespace Api.GraphQL.Types.Input
{
    public class AidFilterInputType : InputObjectGraphType<AidSearchFilter>
    {
        public AidFilterInputType()
        {
            Name = "AidFilter";

            Field(x => x.SendingNation, nullable: true);
            Field(x => x.SendingRuler, nullable: true);
            Field(x => x.SendingAlliance, nullable: true);
            Field(x => x.ReceivingNation, nullable: true);
            Field(x => x.ReceivingRuler, nullable: true);
            Field(x => x.ReceivingAlliance, nullable: true);
            Field<DateTimeGraphType>(nameof(AidSearchFilter.SentEarlierThan));
            Field<DateTimeGraphType>(nameof(AidSearchFilter.SentLaterThan));
            Field<MatchTypeEnum>(nameof(AidSearchFilter.Match));
        }
    }
}
