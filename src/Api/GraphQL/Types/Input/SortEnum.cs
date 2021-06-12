using GraphQL.Types;

namespace Api.GraphQL.Types.Input
{
    public class SortEnum : EnumerationGraphType
    {
        public SortEnum()
        {
            Name = "Sort";

            AddValue("ASC", "Ascending", 1);
            AddValue("DESC", "Descending", 2);
        }
    }
}
