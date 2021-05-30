using AutoMapper;
using Repository.DataAccessLayer;
using DbAlliance = Repository.DataAccessLayer.DTO.Alliance;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valyria.Models;
using Repository.DataAccessLayer.QueryHelpers;

namespace Repository
{
    public class AllianceDataHandler : IAllianceDataHelper
    {
        private readonly IMapper _mapper;
        private readonly IQueryHandler<DbAlliance> _allianceQueryHandler;
        private readonly IReferenceQueryHandler<DbAlliance> _allianceReferenceQueryHandler;

        public AllianceDataHandler(IMapper mapper, IQueryHandler<DbAlliance> allianceQueryHandler, IReferenceQueryHandler<DbAlliance> allianceReferenceHandler)
        {
            _mapper = mapper;
            _allianceQueryHandler = allianceQueryHandler;
            _allianceReferenceQueryHandler = allianceReferenceHandler;
        }

        public async Task<Alliance?> Get(int id)
        {
            var alliance = await _allianceQueryHandler.Query(id);
            return alliance is null ? null : _mapper.Map<Alliance>(alliance);
        }

        public async Task<Alliance?> GetByNation(int nationId)
        {
            var alliance = await _allianceReferenceQueryHandler.Query(ReferenceQueryRequest.ByNationId(nationId));
            return alliance is null ? null : _mapper.Map<Alliance>(alliance);
        }

        public Task<List<(Alliance, DateTime)>> GetAuditHistory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Alliance>> Search(string filter, int? limit = null, int? offset = null)
        {
            throw new NotImplementedException();
        }
    }
}
