using GraphQL.Types;
using Repository.Models;
using Valyria.Models;

namespace Api.GraphQL.Types
{
    public class AidSearchResultsType : ObjectGraphType<SearchResult<Aid>>
    {
        public AidSearchResultsType()
        {
            Name = "AidSearchResults";

            Field<IntGraphType>(nameof(SearchResult<Aid>.TotalCount), "The total number of results that could be returned by the current query");
            Field<ListGraphType<AidType>>(nameof(SearchResult<Aid>.Results), "The search results, limited only to the set requested");
        }
    }
}
