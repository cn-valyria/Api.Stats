using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDataHandler<T> where T : class
    {
        /// <summary>
        /// Get a single instance of the entity by ID. If the entity cannot be found then null will be returned.
        /// </summary>
        Task<T?> Get(int id);

        /// <summary>
        /// Search for any instances of the entity by fuzzy-matching against a filter string. Includes limit and offset for pagination.
        /// </summary>
        Task<List<T>> Search(string filter, int? limit = null, int? offset = null);

        /// <summary>
        /// Returns the audit history stored for the entity by its ID.
        /// </summary>
        /// <returns>
        /// A representation of the entity at the time the audit change was recorded, along with the timestamp that the change was recorded.
        /// </returns>
        Task<List<T>> GetAuditHistory(int id);
    }
}
