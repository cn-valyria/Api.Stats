using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.DataAccessLayer.QueryHelpers
{
    public class ReferenceQueryRequest
    {
        public int? NationId { get; }
        public int? AllianceId { get; }

        private ReferenceQueryRequest(int? nationId = null, int? allianceId = null)
            => (NationId, AllianceId) = (nationId, allianceId);

        public static ReferenceQueryRequest ByNationId(int nationId) => new ReferenceQueryRequest(nationId);
    }
}
