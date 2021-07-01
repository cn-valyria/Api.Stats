using GraphQL.DataLoader;
using Repository;
using System.Linq;
using Valyria.Models;

namespace Api.GraphQL.Helpers
{
    public static class DataLoaderContextAccessorExtensions
    {
        public static object GetNationById(
            this IDataLoaderContextAccessor dataLoaderContextAccessor,
            int nationId,
            string loaderKey,
            ICnDbRepository cnDbRepository)
        {
            var dataLoader = dataLoaderContextAccessor.Context.GetOrAddBatchLoader<int, Nation>(loaderKey, async nationIds =>
            {
                var allNations = await cnDbRepository.Nations.Get(nationIds);
                return allNations.ToDictionary(nation => nation.Id, nation => nation);
            });

            return dataLoader.LoadAsync(nationId);
        }

        public static object GetAllianceById(
            this IDataLoaderContextAccessor dataLoaderContextAccessor,
            int allianceId,
            string loaderKey,
            ICnDbRepository cnDbRepository)
        {
            var dataLoader = dataLoaderContextAccessor.Context.GetOrAddBatchLoader<int, Alliance>(loaderKey, async allianceIds =>
            {
                var allAlliances = await cnDbRepository.Alliances.Get(allianceIds);
                return allAlliances.ToDictionary(alliance => alliance.Id, alliance => alliance);
            });

            return dataLoader.LoadAsync(allianceId);
        }
    }
}
