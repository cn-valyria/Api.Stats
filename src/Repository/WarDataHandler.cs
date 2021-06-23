using AutoMapper;
using Repository.DataAccessLayer;
using DbWar = Repository.DataAccessLayer.DTO.War;
using Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valyria.Models;

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
            var result = war is null ? null : _mapper.Map<War>(war);
            return result;
        }

        public Task<List<War>> Get(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<War>> GetAuditHistory(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<SearchResult<War>> Search(SearchFilter filter, Dictionary<string, object> orderBy, int? limit = null, int? offset = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
