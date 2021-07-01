using AutoMapper;
using Repository.DataAccessLayer;
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

        public Task<SearchResult<Aid>> Search(SearchFilter filter, Dictionary<string, object> orderBy, int? limit = null, int? offset = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
