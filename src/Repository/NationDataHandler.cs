using Repository.DataAccessLayer;
using DbNation = Repository.DataAccessLayer.DTO.Nation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valyria.Models;
using AutoMapper;

namespace Repository
{
    public class NationDataHandler : INationDataHandler
    {
        private readonly IMapper _mapper;
        private readonly IQueryHandler<DbNation> _nationQueryHandler;

        public NationDataHandler(
            IMapper mapper,
            IQueryHandler<DbNation> nationQueryHandler)
        {
            _mapper = mapper;
            _nationQueryHandler = nationQueryHandler;
        }

        public async Task<Nation?> Get(int id)
        {
            var nation = await _nationQueryHandler.Query(id);
            return nation is null ? null : _mapper.Map<Nation>(nation);
        }

        public Task<List<(Nation, DateTime)>> GetAuditHistory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Nation>> Search(string filter, int? limit = null, int? offset = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Nation>> GetByAlliance(int? allianceId, string allianceName)
        {
            throw new NotImplementedException();
        }
    }
}
