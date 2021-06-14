using AutoMapper;
using Repository.DataAccessLayer;
using DbAlliance = Repository.DataAccessLayer.DTO.Alliance;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valyria.Models;
using Repository.DataAccessLayer.QueryHelpers;
using System.Linq;
using Repository.Models;

namespace Repository
{
    public class AllianceDataHandler : IAllianceDataHelper
    {
        private readonly IMapper _mapper;
        private readonly IQueryHandler<DbAlliance> _allianceQueryHandler;
        private readonly IAuditQueryHandler<DbAlliance> _allianceAuditQueryHandler;

        public AllianceDataHandler(
            IMapper mapper, 
            IQueryHandler<DbAlliance> allianceQueryHandler,
            IAuditQueryHandler<DbAlliance> auditQueryHandler)
        {
            _mapper = mapper;
            _allianceQueryHandler = allianceQueryHandler;
            _allianceAuditQueryHandler = auditQueryHandler;
        }

        public async Task<Alliance?> Get(int id)
        {
            var alliance = await _allianceQueryHandler.Query(id);
            return alliance is null ? null : _mapper.Map<Alliance>(alliance);
        }

        public async Task<List<Alliance>> Get(IEnumerable<int> ids)
        {
            var allAlliances = await _allianceQueryHandler.Query(ids);
            return allAlliances.Select(_mapper.Map<Alliance>).ToList();
        }

        public async Task<List<Alliance>> GetAuditHistory(int id)
        {
            var auditData = await _allianceAuditQueryHandler.QueryAuditData(id);
            return auditData.Select(_mapper.Map<Alliance>).ToList();
        }

        public async Task<SearchResult<Alliance>> Search(SearchFilter filter, Dictionary<string, object> orderBy, int? limit = null, int? offset = null)
        {
            var dataOrderBy = orderBy?.Keys.Select(key => new OrderByClause(key, orderBy[key])).ToList() ?? new List<OrderByClause>();
            if (!dataOrderBy.Any())
                dataOrderBy.Add(OrderByClause.DefaultAllianceOrderBy);

            var (totalCount, searchResults) = await _allianceQueryHandler.Query(filter, dataOrderBy, limit ?? 100, offset ?? 0);
            return new SearchResult<Alliance>
            {
                TotalCount = totalCount,
                Results = searchResults.Select(_mapper.Map<Alliance>).ToList()
            };
        }
    }
}
