using GraphQL.Types;
using Repository.Models;
using Valyria.Models;

namespace Api.GraphQL.Types
{
    public class NationSearchResultsType : ObjectGraphType<SearchResult<Nation>>
    {
        public NationSearchResultsType()
        {
            Name = "NationSearchResults";

            Field<IntGraphType>(nameof(SearchResult<Nation>.TotalCount), "The total number of results that could be returned by the current query");
            Field<ListGraphType<NationType>>(
                nameof(SearchResult<Nation>.Results), 
                "The search results, limited only to the set requested",
                resolve: context => context.Source.Results);
        }
    }
}
