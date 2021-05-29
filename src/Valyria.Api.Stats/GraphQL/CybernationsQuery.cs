using Api.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using Repository;
using System;
using System.Threading.Tasks;
using Valyria.Models;
using Valyria.Models.Enums;

namespace Api.GraphQL
{
    public class CybernationsQuery : ObjectGraphType<object>
    {
        private readonly ICnDbRepository _cnDbRepository;

        public CybernationsQuery(ICnDbRepository cnDbRepository)
        {
            _cnDbRepository = cnDbRepository;

            Name = "Query";

            Field<NationType>("nation", arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }), resolve: QueryNationById);
        }

        public object QueryNationById(IResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("id");
            return id.HasValue ? _cnDbRepository.Nations.Get(id.Value) : Task.FromResult<Nation>(null);
        }
    }
}
