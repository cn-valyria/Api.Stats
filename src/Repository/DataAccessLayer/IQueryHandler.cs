using Repository.DataAccessLayer.DTO;
using Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.DataAccessLayer
{
    public interface IQueryHandler<T> where T : class
    {
        /// <summary>
        /// Query for the entity by its ID. Will return nothing if the ID is not valid
        /// </summary>
        Task<T?> Query(int id);

        /// <summary>
        /// Query for a list of entities by their IDs. Any invalid IDs will simply not return a record in the results.
        /// </summary>
        Task<IEnumerable<T>> Query(IEnumerable<int> ids);

        /// <summary>
        /// Query for a list of entities by fuzzy-matching text fields. Includes limit and offset for pagination purposes.
        /// </summary>
        Task<IEnumerable<T>> Query(SearchFilter filter, int limit, int offset);
    }
}
