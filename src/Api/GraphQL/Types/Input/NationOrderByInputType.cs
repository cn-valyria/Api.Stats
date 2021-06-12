using GraphQL.Types;

namespace Api.GraphQL.Types.Input
{
    public class NationOrderByInputType : InputObjectGraphType
    {
        public NationOrderByInputType()
        {
            Name = "NationOrderBy";
            Description = "The field that the results will be ordered by.";

            Field<SortEnum>("nationId");
            Field<SortEnum>("nationName");
            Field<SortEnum>("rulerName");
            Field<SortEnum>("allianceName");
            Field<SortEnum>("strength");
            Field<SortEnum>("createdOn");
            Field<SortEnum>("updatedOn");
            Field<SortEnum>("infrastructure");
            Field<SortEnum>("technology");
        }
    }
}
