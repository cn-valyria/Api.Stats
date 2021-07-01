using AutoMapper;
using Repository.DataAccessLayer;
using Repository.DataAccessLayer.QueryHelpers;
using Repository.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valyria.Models;
using DbAid = Repository.DataAccessLayer.DTO.Aid;

namespace Repository
{
    public class AidDataHandler : IAidDataHandler
    {
        private readonly IMapper _mapper;
        private readonly IQueryHandler<DbAid> _aidQueryHandler;

        public AidDataHandler(IMapper mapper, IQueryHandler<DbAid> queryHandler)
            => (_mapper, _aidQueryHandler) = (mapper, queryHandler);

        public async Task<Aid?> Get(int id)
        {
            var aid = await _aidQueryHandler.Query(id);
            return aid is null ? null : _mapper.Map<Aid>(aid);
        }

        public async Task<List<Aid>> Get(IEnumerable<int> ids)
        {
            var results = await _aidQueryHandler.Query(ids);
            return results.Select(_mapper.Map<Aid>).ToList();
        }

        public Task<List<Aid>> GetAuditHistory(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SearchResult<Aid>> Search(SearchFilter filter, Dictionary<string, object> orderBy, int? limit = null, int? offset = null)
        {
            // Map order by clause dictionary to actual clauses, and check for fields with different names than their respective DB column
            var dataOrderBy = orderBy?.Keys.Select(key => key.ToLower() switch
            {
                "sentOn" => new OrderByClause("Date", orderBy[key]),
                _ => new OrderByClause(key, orderBy[key])
            }).ToList() ?? new List<OrderByClause>();

            // Sanity check that at least some default ordering is used if not provided
            if (!dataOrderBy.Any())
                dataOrderBy.Add(OrderByClause.DefaultAidOrderBy);

            // Get the results (plus total possible query result count) and return
            var (totalCount, searchResults) = await _aidQueryHandler.Query(filter, dataOrderBy, limit ?? 100, offset ?? 0);
            return new SearchResult<Aid>
            {
                TotalCount = totalCount,
                Results = searchResults.Select(_mapper.Map<Aid>).ToList()
            };
        }
    }
}
