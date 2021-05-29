using Repository.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.DataAccessLayer
{
    public interface IQueryHandler<T>
    {
        /// <summary>
        /// Query for the entity by its ID. Will return nothing if the ID is not valid
        /// </summary>
        Task<T> Query(int id);

        /// <summary>
        /// Query for a list of entities by fuzzy-matching text fields. Includes limit and offset for pagination purposes.
        /// </summary>
        Task<IEnumerable<T>> Query(string filter, int limit, int offset);
    }
}
