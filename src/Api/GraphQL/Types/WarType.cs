using GraphQL.Types;
using Repository;
using Valyria.Models;

namespace Api.GraphQL.Types
{
    public class WarType : ObjectGraphType<War>
    {
        private readonly ICnDbRepository _cnDbRepository;

        public WarType(ICnDbRepository cnDbRepository)
        {
            _cnDbRepository = cnDbRepository;

            Name = "War";
            Description = "A single war between two nations began at a specific point in time";

            Field(w => w.Id).Description("The unique identifier of this war");
            Field<DecimalGraphType>(nameof(War.AttackingDestruction), "The amount of destruction (aka damage) inflicted against the attacker");
            Field<DecimalGraphType>(nameof(War.DefendingDestruction), "The amount of destruction (aka damage) inflicted against the defender");
            Field<DateTimeGraphType>(nameof(War.BeginDate), "The exact date and time that the war was declared");
            Field<DateTimeGraphType>(nameof(War.EndDate), "The date that the war should expire after");
            Field(w => w.Reason).Description("The reason given by the player declaring the war for why they declared war");
        }
    }
}
