using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.DataAccessLayer
{
    public interface IAuditQueryHandler<T>
    {
        /// <summary>
        /// Query for all audit data recorded for the entity.
        /// </summary>
        /// <param name="id">The ID of the entity that audit data may be recorded for</param>
        /// <param name="updatedAfter">A "lower bound" of timestamps after which audits must have been recorded. Defaults to the beginning of time</param>
        /// <param name="updatedBefore">An "upper bound" of timestamps before which audits must have been recorded. Defaults to right now</param>
        /// <param name="limit">The number of audit records to return, at most. Defaults to 100</param>
        /// <param name="offset">The number of audit records to skip before beginning to return results. Defaults to 0</param>
        Task<IEnumerable<T>> QueryAuditData(
            int id,
            DateTime? updatedAfter = null,
            DateTime? updatedBefore = null,
            int limit = 100,
            int offset = 0);
    }
}
