using System.Collections.Generic;
using System.Threading.Tasks;
using Valyria.Models;

namespace Repository
{
    public interface INationDataHandler : IDataHandler<Nation>
    {
        Task<IEnumerable<Nation>> GetByAlliance(int? allianceId, string allianceName);
    }
}
