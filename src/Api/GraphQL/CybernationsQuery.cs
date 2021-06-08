using Api.GraphQL.Types;
using Api.GraphQL.Types.Input;
using GraphQL;
using GraphQL.Types;
using Repository;
using Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valyria.Models;

namespace Api.GraphQL
{
    public class CybernationsQuery : ObjectGraphType<object>
    {
        private readonly ICnDbRepository _cnDbRepository;

        public CybernationsQuery(ICnDbRepository cnDbRepository)
        {
            _cnDbRepository = cnDbRepository;

            Name = "Query";

            Field<NationType>("getNation", arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "nationId" }), resolve: QueryNationById);
            Field<ListGraphType<NationType>>(
                "searchNations",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<FilterInputType>> { Name = "filter" },
                    new QueryArgument<IntGraphType> { Name = "limit" },
                    new QueryArgument<IntGraphType> { Name = "offset" }),
                resolve: QueryNationsByFilter);
        }

        public object QueryNationById(IResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("nationId");
            return id.HasValue ? _cnDbRepository.Nations.Get(id.Value) : Task.FromResult<Nation>(null);
        }

        public object QueryNationsByFilter(IResolveFieldContext<object> context)
        {
            var filter = context.GetArgument<SearchFilter>("filter");
            var limit = context.GetArgument<int?>("limit");
            var offset = context.GetArgument<int?>("offset");
            return _cnDbRepository.Nations.Search(filter, limit, offset);
        }
    }
}
