using Api.GraphQL.Types;
using Api.GraphQL.Types.Input;
using GraphQL;
using GraphQL.Types;
using Newtonsoft.Json.Linq;
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
            Field<NationSearchResultsType>(
                "searchNations",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<FilterInputType>> { Name = "filter" },
                    new QueryArgument<NationOrderByInputType> { Name = "orderBy" },
                    new QueryArgument<IntGraphType> { Name = "limit" },
                    new QueryArgument<IntGraphType> { Name = "offset" }),
                resolve: QueryNationsByFilter);

            Field<AllianceType>("getAlliance", arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "allianceId" }), resolve: QueryAllianceById);
            Field<AllianceSearchResultsType>(
                "searchAlliances",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<FilterInputType>> { Name = "filter" },
                    new QueryArgument<AllianceOrderByInput> { Name = "orderBy" },
                    new QueryArgument<IntGraphType> { Name = "limit" },
                    new QueryArgument<IntGraphType> { Name = "offset" }),
                resolve: QueryAlliancesByFilter);

            Field<WarType>("getWar", arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "warId" }), resolve: QueryWarById);
        }

        #region Nation Resolvers

        public object QueryNationById(IResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("nationId");
            return id.HasValue ? _cnDbRepository.Nations.Get(id.Value) : Task.FromResult<Nation>(null);
        }

        public object QueryNationsByFilter(IResolveFieldContext<object> context)
        {
            var filter = context.GetArgument<SearchFilter>("filter");
            var orderBy = context.GetArgument<Dictionary<string, object>>("orderBy");
            var limit = context.GetArgument<int?>("limit");
            var offset = context.GetArgument<int?>("offset");
            return _cnDbRepository.Nations.Search(filter, orderBy, limit, offset);
        }

        #endregion

        #region Alliance Resolvers

        public object QueryAllianceById(IResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("allianceId");
            return id.HasValue ? _cnDbRepository.Alliances.Get(id.Value) : Task.FromResult<Alliance>(null);
        }

        public object QueryAlliancesByFilter(IResolveFieldContext<object> context)
        {
            var filter = context.GetArgument<SearchFilter>("filter");
            var orderBy = context.GetArgument<Dictionary<string, object>>("orderBy");
            var limit = context.GetArgument<int?>("limit");
            var offset = context.GetArgument<int?>("offset");
            return _cnDbRepository.Alliances.Search(filter, orderBy, limit, offset);
        }

        #endregion

        #region War Resolvers

        public object QueryWarById(IResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("warId");
            return id.HasValue ? _cnDbRepository.Wars.Get(id.Value) : Task.FromResult<War>(null);
        }

        #endregion
    }
}
