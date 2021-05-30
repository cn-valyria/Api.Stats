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

            Field<NationType>("getNation", arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "nationId" }), resolve: QueryNationById);
        }

        public object QueryNationById(IResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("nationId");
            return id.HasValue ? _cnDbRepository.Nations.Get(id.Value) : Task.FromResult<Nation>(null);
        }
    }
}
