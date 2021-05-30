using Repository.DataAccessLayer.QueryHelpers;
using System.Threading.Tasks;

namespace Repository.DataAccessLayer
{
    public interface IReferenceQueryHandler<T> where T : class
    {
        /// <summary>
        /// Query for the entity by its reference to a nation.
        /// </summary>
        Task<T?> Query(ReferenceQueryRequest referenceQueryRequest);
    }
}
