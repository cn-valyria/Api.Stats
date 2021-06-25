using GraphQL.Types;

namespace Api.GraphQL.Types.Input
{
    public class WarOrderByInputType : InputObjectGraphType
    {
        public WarOrderByInputType()
        {
            Name = "WarOrderBy";
            Description = "The field that the results will be ordered by";

            Field<SortEnum>("totalDestruction");
            Field<SortEnum>("declaredOn");
            Field<SortEnum>("expiresOn");
        }
    }
}
