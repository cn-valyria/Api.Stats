using AutoMapper;
using Repository.DataAccessLayer;
using DbWar = Repository.DataAccessLayer.DTO.War;
using Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valyria.Models;
using System.Linq;
using Repository.DataAccessLayer.QueryHelpers;

namespace Repository
{
    public class WarDataHandler : IWarDataHandler
    {
        public IMapper _mapper;
        public IQueryHandler<DbWar> _warQueryHandler;

        public WarDataHandler(IMapper mapper, IQueryHandler<DbWar> queryHandler)
            => (_mapper, _warQueryHandler) = (mapper, queryHandler);

        public async Task<War?> Get(int id)
        {
            var war = await _warQueryHandler.Query(id);
            return war is null ? null : _mapper.Map<War>(war);
        }

        public async Task<List<War>> Get(IEnumerable<int> ids)
        {
            var results = await _warQueryHandler.Query(ids);
            return results.Select(_mapper.Map<War>).ToList();
        }

        public Task<List<War>> GetAuditHistory(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SearchResult<War>> Search(SearchFilter filter, Dictionary<string, object> orderBy, int? limit = null, int? offset = null)
        {
            // Map order by clause dictionary to actual clauses, and check for fields with different names than their respective DB column
            var dataOrderBy = orderBy?.Keys.Select(key => key.ToLower() switch
            {
                "totaldestruction" => new OrderByClause("Destruction", orderBy[key]),
                "declaredon" => new OrderByClause("BeginDate", orderBy[key]),
                "expireson" => new OrderByClause("EndDate", orderBy[key]),
                _ => new OrderByClause(key, orderBy[key])
            }).ToList() ?? new List<OrderByClause>();

            // Sanity check that at least some default ordering is used if not provided
            if (!dataOrderBy.Any())
                dataOrderBy.Add(OrderByClause.DefaultWarOrderBy);

            // Get the results (plus total possible query result count) and return
            var (totalCount, searchResults) = await _warQueryHandler.Query(filter, dataOrderBy, limit ?? 100, offset ?? 0);
            return new SearchResult<War>
            {
                TotalCount = totalCount,
                Results = searchResults.Select(_mapper.Map<War>).ToList()
            };
        }
    }
}
