using GraphQL.Types;
using Repository.Models;
using Valyria.Models;

namespace Api.GraphQL.Types
{
    public class AllianceSearchResultsType : ObjectGraphType<SearchResult<Alliance>>
    {
        public AllianceSearchResultsType()
        {
            Name = "AllianceSearchResults";

            Field<IntGraphType>(nameof(SearchResult<Alliance>.TotalCount), "The total number of results that could be returned by the current query");
            Field<ListGraphType<AllianceType>>(
                nameof(SearchResult<Alliance>.Results),
                "The search results, limited only to the set requested",
                resolve: context => context.Source.Results);
        }
    }
}
