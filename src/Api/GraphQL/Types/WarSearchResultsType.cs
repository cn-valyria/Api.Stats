using GraphQL.Types;
using Repository.Models;
using Valyria.Models;

namespace Api.GraphQL.Types
{
    public class WarSearchResultsType : ObjectGraphType<SearchResult<War>>
    {
        public WarSearchResultsType()
        {
            Name = "WarSearchResults";

            Field<IntGraphType>(nameof(SearchResult<Alliance>.TotalCount), "The total number of results that could be returned by the current query");
            Field<ListGraphType<WarType>>(
                nameof(SearchResult<War>.Results),
                "The search results, limited only to the set requested",
                resolve: context => context.Source.Results);
        }
    }
}
