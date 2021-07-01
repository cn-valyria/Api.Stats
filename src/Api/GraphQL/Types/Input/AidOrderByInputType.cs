using GraphQL.Types;

namespace Api.GraphQL.Types.Input
{
    public class AidOrderByInputType : InputObjectGraphType
    {
        public AidOrderByInputType()
        {
            Name = "AidOrderBy";
            Description = "The field that the results will be ordered by";

            Field<SortEnum>("sentOn");
        }
    }
}
