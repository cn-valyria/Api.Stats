using GraphQL.Types;

namespace Api.GraphQL.Types.Input
{
    public class AllianceOrderByInput : InputObjectGraphType
    {
        public AllianceOrderByInput()
        {
            Name = "AllianceOrderBy";
            Description = "The field that the results will be ordered by";

            Field<SortEnum>("AllianceName");
            Field<SortEnum>("TotalNations");
            Field<SortEnum>("ActiveNations");
            Field<SortEnum>("TotalStrength");
            Field<SortEnum>("Score");
        }
    }
}
