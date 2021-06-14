using Repository.DataAccessLayer;
using DbNation = Repository.DataAccessLayer.DTO.Nation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valyria.Models;
using AutoMapper;
using System.Linq;
using Repository.Models;
using Repository.DataAccessLayer.QueryHelpers;

namespace Repository
{
    public class NationDataHandler : INationDataHandler
    {
        private readonly IMapper _mapper;
        private readonly IQueryHandler<DbNation> _nationQueryHandler;
        private readonly IAuditQueryHandler<DbNation> _nationAuditQueryHandler;

        public NationDataHandler(
            IMapper mapper,
            IQueryHandler<DbNation> nationQueryHandler,
            IAuditQueryHandler<DbNation> auditQueryHandler)
        {
            _mapper = mapper;
            _nationQueryHandler = nationQueryHandler;
            _nationAuditQueryHandler = auditQueryHandler;
        }

        public async Task<Nation?> Get(int id)
        {
            var nation = await _nationQueryHandler.Query(id);
            return nation is null ? null : _mapper.Map<Nation>(nation);
        }

        public async Task<List<Nation>> Get(IEnumerable<int> ids)
        {
            var allNations = await _nationQueryHandler.Query(ids);
            return allNations.Select(_mapper.Map<Nation>).ToList();
        }

        public async Task<List<Nation>> GetAuditHistory(int id)
        {
            var auditData = await _nationAuditQueryHandler.QueryAuditData(id);
            return auditData.Select(_mapper.Map<Nation>).ToList();
        }

        public async Task<SearchResult<Nation>> Search(SearchFilter filter, Dictionary<string, object> orderBy, int? limit = null, int? offset = null)
        {
            var dataOrderBy = orderBy?.Keys.Select(key => new OrderByClause(key, orderBy[key])).ToList() ?? new List<OrderByClause>();
            if (!dataOrderBy.Any())
                dataOrderBy.Add(OrderByClause.DefaultNationOrderBy);

            var (totalCount, searchResults) = await _nationQueryHandler.Query(filter, dataOrderBy, limit ?? 100, offset ?? 0);
            return new SearchResult<Nation>
            {
                TotalCount = totalCount,
                Results = searchResults.Select(_mapper.Map<Nation>).ToList()
            };
        }
    }
}
