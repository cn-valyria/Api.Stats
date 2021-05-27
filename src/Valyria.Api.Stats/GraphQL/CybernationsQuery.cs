using GraphQL.Types;

namespace Api.GraphQL
{
    public class CybernationsQuery : ObjectGraphType<object>
    {
        public CybernationsQuery()
        {
            Name = "Query";
        }
    }
}
